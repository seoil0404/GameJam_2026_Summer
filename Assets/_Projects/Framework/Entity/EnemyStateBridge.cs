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
        else if (UnityEngine.Random.value < 0.3f)
        {
            SmartAllocate(hands, field);
        }
        else
        {
            RandomAllocate(hands, field);
        }

        CardVisualSynchronyzer.Instance.SyncEnemy();
    }

    public static void OnEndSync()
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

    // 첫 라운드: 상대 필드를 보고 판단하지 않고, 손패에서 그냥 순서대로(랜덤하게 생성된 손패이므로 사실상 랜덤) 채운다.
    // 첫 라운드부터 상대 카드를 보고 맞춰 내면 무조건 이겨버려서 일부러 여길 상대 정보 없이 채우게 함.
    private static void PlaceInitialCards(List<CardData> hands, Field field)
    {
        for (int i = 0; i < field.Cards.Length; i++)
        {
            if (hands.Count == 0)
                break;

            field.Cards[i] = hands[0];
            hands.RemoveAt(0);
        }
    }

    // 2라운드부터 30% 확률로 실행됨: 상대 필드는 보지 않고(다음에 플레이어가 뭘 낼지 모르는 상태),
    // 내 손패만 보고 "이 카드면 이길 것 같다"(승리 조건 카드)를 찾아서 필드에서 제일 안좋은 자리와 교체한다.
    private static void SmartAllocate(List<CardData> hands, Field enemyField)
    {
        CardData winCard = hands.Find(card => card.EffectActivateCondition == EffectActivateCondition.Win);

        if (winCard == null)
        {
            // 손패에 승리 조건 카드가 없으면 그냥 랜덤 손패 교체로 대체
            RandomSwapWithHand(hands, enemyField);
            return;
        }

        int worstSlot = FindWorstSlot(enemyField);

        if (worstSlot == -1)
        {
            RandomSwapWithHand(hands, enemyField);
            return;
        }

        enemyField.Cards[worstSlot] = winCard;
        hands.Remove(winCard);
    }

    // 필드에서 가장 안좋다고 볼 수 있는 자리를 찾는다 (빈 자리 > 패배조건 카드 > 무승부조건 카드 > 승리조건 카드 순으로 우선 교체 대상)
    private static int FindWorstSlot(Field field)
    {
        for (int i = 0; i < field.Cards.Length; i++)
        {
            if (field.Cards[i] == null)
                return i;
        }

        int worstIndex = -1;
        int worstRank = int.MaxValue;

        for (int i = 0; i < field.Cards.Length; i++)
        {
            int rank = ConditionRank(field.Cards[i].EffectActivateCondition);
            if (rank < worstRank)
            {
                worstRank = rank;
                worstIndex = i;
            }
        }

        return worstIndex;
    }

    // 숫자가 낮을수록 먼저 교체 대상 (패배 조건 < 무승부 조건 < 승리 조건)
    private static int ConditionRank(EffectActivateCondition condition)
    {
        return condition switch
        {
            EffectActivateCondition.Lose => 0,
            EffectActivateCondition.Draw => 1,
            EffectActivateCondition.Win => 2,
            _ => 1
        };
    }

    // 70% 확률로 이쪽을 타면 분석 없이 필드 위치교환 / 손패교체 중 하나를 랜덤으로 실행한다.
    private static void RandomAllocate(List<CardData> hands, Field field)
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            RandomSwapFieldPositions(field);
        }
        else
        {
            RandomSwapWithHand(hands, field);
        }
    }

    //(미사용)이기는속성반환함수
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

    //(미사용) 매라운드랜덤배치방식
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

    // 필드 카드 두 자리를 완전 랜덤으로 위치교환 (분석 없음)
    private static void RandomSwapFieldPositions(Field field)
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

    // 완전 랜덤한 필드 슬롯에 손패 맨 앞 카드를 교체 (분석 없음)
    private static void RandomSwapWithHand(List<CardData> hands, Field field)
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
