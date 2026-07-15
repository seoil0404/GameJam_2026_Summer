using System;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyStateBridge
{
    public static event Action OnAllocateComplete;

    public static Entity GetEnemy()
    {
        return Enemy.Instance;
    }

    public static void StartAllocate()
    {
        List<CardData> hands = HandManager.Instance.EnemyHands;
        Field field = FieldManager.Instance.EnemyField;

        if (IsFirstRound(field))
        {
            PlaceInitialCards(hands, field);
        }
        else
        {
            AnalyzeAndAllocate(hands, field);
        }

        CardVisualSynchronyzer.Instance.SyncEnemy();
        CardVisualSynchronyzer.Instance.OnSyncEnemyComplete += OnSyncEnemyComplete;
    }

    private static void OnSyncEnemyComplete()
    {
        OnAllocateComplete?.Invoke();
    }

    private static bool IsFirstRound(Field field)
    {
        foreach (CardData card in field.Cards)
        {
            if (card != null)
                return false;
        }
        return true;
    }

    // 첫 라운드
    private static void PlaceInitialCards(List<CardData> hands, Field field)
    {
        Field playerField = FieldManager.Instance.PlayerField;

        for (int i = 0; i < field.Cards.Length; i++)
        {
            if (hands.Count == 0)
                break;

            CardData best = hands[0];
            int bestScore = ScoreSlot(best, playerField.Cards[i]);

            foreach (CardData candidate in hands)
            {
                int score = ScoreSlot(candidate, playerField.Cards[i]);
                if (score > bestScore)
                {
                    best = candidate;
                    bestScore = score;
                }
            }

            field.Cards[i] = best;
            hands.Remove(best);
        }
    }

    // 2라운드부터 가능한 행동(손패 1장 교체 / 필드 두 자리 위치 교환)
    private static void AnalyzeAndAllocate(List<CardData> hands, Field enemyField)
    {
        Field playerField = FieldManager.Instance.PlayerField;
        int slotCount = enemyField.Cards.Length;

        int[] currentScores = new int[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            currentScores[i] = ScoreSlot(enemyField.Cards[i], playerField.Cards[i]);
        }

        int bestDelta = 0;
        int bestActionType = 0; // 0: 없음, 1: 손패 교체, 2: 필드 위치 교환
        CardData bestHandCard = null;
        int bestHandSlot = -1;
        int bestSwapA = -1;
        int bestSwapB = -1;

        // 후보 1: 손패 카드로 특정 자리 교체
        foreach (CardData candidate in hands)
        {
            for (int i = 0; i < slotCount; i++)
            {
                if (playerField.Cards[i] == null)
                    continue;

                int newScore = ScoreSlot(candidate, playerField.Cards[i]);
                int delta = newScore - currentScores[i];

                if (delta > bestDelta)
                {
                    bestDelta = delta;
                    bestActionType = 1;
                    bestHandCard = candidate;
                    bestHandSlot = i;
                }
            }
        }

        // 후보 2: 필드 카드 두 자리 위치 교환
        for (int i = 0; i < slotCount; i++)
        {
            for (int j = i + 1; j < slotCount; j++)
            {
                if (enemyField.Cards[i] == null || enemyField.Cards[j] == null)
                    continue;
                if (playerField.Cards[i] == null || playerField.Cards[j] == null)
                    continue;

                int newScoreI = ScoreSlot(enemyField.Cards[j], playerField.Cards[i]);
                int newScoreJ = ScoreSlot(enemyField.Cards[i], playerField.Cards[j]);
                int delta = (newScoreI + newScoreJ) - (currentScores[i] + currentScores[j]);

                if (delta > bestDelta)
                {
                    bestDelta = delta;
                    bestActionType = 2;
                    bestSwapA = i;
                    bestSwapB = j;
                }
            }
        }

        if (bestActionType == 1)
        {
            enemyField.Cards[bestHandSlot] = bestHandCard;
            hands.Remove(bestHandCard);
        }
        else if (bestActionType == 2)
        {
            (enemyField.Cards[bestSwapA], enemyField.Cards[bestSwapB]) = (enemyField.Cards[bestSwapB], enemyField.Cards[bestSwapA]);
        }

    }


    private static int ScoreSlot(CardData enemyCard, CardData playerCard)
    {
        if (enemyCard == null || playerCard == null)
            return 0;

        EffectActivateCondition enemyOutcome = GetOutcome(enemyCard.CombatAttribute, playerCard.CombatAttribute);
        EffectActivateCondition playerOutcome = Mirror(enemyOutcome);

        int score = 0;

        if (enemyCard.EffectActivateCondition == enemyOutcome)
            score += 1;

        if (playerCard.EffectActivateCondition == playerOutcome)
            score -= 1;

        return score;
    }

    // self 입장에서 이 매치업 결과가 Win / Lose / Draw 중 뭔지 (BattleManager 판정 규칙과 동일)
    private static EffectActivateCondition GetOutcome(CombatAttribute self, CombatAttribute opponent)
    {
        if (self == opponent)
            return EffectActivateCondition.Draw;

        return Beats(self, opponent) ? EffectActivateCondition.Win : EffectActivateCondition.Lose;
    }

    // outcome을 상대방 입장에서 보면 어떻게 되는지 (Win <-> Lose, Draw는 그대로)
    private static EffectActivateCondition Mirror(EffectActivateCondition outcome)
    {
        return outcome switch
        {
            EffectActivateCondition.Win => EffectActivateCondition.Lose,
            EffectActivateCondition.Lose => EffectActivateCondition.Win,
            _ => EffectActivateCondition.Draw
        };
    }

    // attacker가 defender를 이기는지 (BattleManager의 승패 판정과 동일한 규칙)
    private static bool Beats(CombatAttribute attacker, CombatAttribute defender)
    {
        return (attacker, defender) switch
        {
            (CombatAttribute.Fire, CombatAttribute.Grass) => true,
            (CombatAttribute.Grass, CombatAttribute.Water) => true,
            (CombatAttribute.Water, CombatAttribute.Fire) => true,
            _ => false
        };
    }

    //(미사용) 이 속성을 이기는 속성이 뭔지 반환. 속성 상성만 보던 이전 버전에서 쓰던 함수.
    private static CombatAttribute CounterOf_Unused(CombatAttribute attribute)
    {
        return attribute switch
        {
            CombatAttribute.Fire => CombatAttribute.Water,
            CombatAttribute.Grass => CombatAttribute.Fire,
            CombatAttribute.Water => CombatAttribute.Grass,
            _ => attribute
        };
    }

    //(미사용) 예전에 쓰던 "매 라운드 필드를 통째로 랜덤 재생성" 방식.
    private static void StartAllocate_FullRandom_Unused()
    {
        Field field = FieldManager.Instance.EnemyField;

        var randomCards = CardManager.Instance.GenerateCards(field.Cards.Length, OwnerType.Enemy);

        for (int i = 0; i < field.Cards.Length; i++)
        {
            field.Cards[i] = randomCards[i];
        }

        CardVisualSynchronyzer.Instance.SyncEnemy();

        OnAllocateComplete?.Invoke();
    }

    //(미사용) 예전에 쓰던 "50% 확률로 필드 위치교환 / 손패교체" 방식.
    private static void SwapFieldPositions_Unused(Field field)
    {
        if (field.Cards.Length < 2)
            return;

        int indexA = UnityEngine.Random.Range(0, field.Cards.Length);
        int indexB;
        do
        {
            indexB = UnityEngine.Random.Range(0, field.Cards.Length);
        } while (indexB == indexA);

        (field.Cards[indexA], field.Cards[indexB]) = (field.Cards[indexB], field.Cards[indexA]);
    }

    //(미사용) 예전에 쓰던 "50% 확률로 필드 위치교환 / 손패교체" 방식.
    private static void SwapWithHand_Unused(List<CardData> hands, Field field)
    {
        if (hands.Count == 0)
            return;

        int slotIndex = UnityEngine.Random.Range(0, field.Cards.Length);

        field.Cards[slotIndex] = hands[0];
        hands.RemoveAt(0);
    }

    /*private static void Example()
    {
        // 카드 배치가 끝났을때는 아래처럼
        OnAllocateComplete.Invoke();

        // StartAllocate가 호출되면 아래 객체들을 참고해서 카드 배치하기

        // *손패 접근 (List<CardData>로 반환)
        var hands = HandManager.Instance.EnemyHands;

        // *필드 접근
        var field = FieldManager.Instance.EnemyField;


        손패에서 카드를 꺼내서 필드에 카드를 넣는 로직을 구현해야하는데,
        처음 라운드에는 EnemyField에서 필드가 다 null이니까 그걸 감지해서
        처음 라운드일때는 손패에서 4개 꺼내서 배치하면 되고
        처음 라운드가 아닐 때에는 손패에서 새로운걸 꺼내서 필드에 넣을지,
        아니면 필드 카드끼리 교체할지를 너의 AI가 선택해야 함
        손패나 필드에서 따로 교환 로직은 없고 너가 바꾸는대로 다 바뀌는거라 너가 그런 로직까지 만들면 될듯

    }*/


}
