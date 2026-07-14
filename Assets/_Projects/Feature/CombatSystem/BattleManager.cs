using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleComplete;

    private List<EffectContainer> effectStack = new();

    private void Awake()
    {
        Instance = this;
    }

    public void StartBattle()
    {
        effectStack.Clear();

        Field playerField = FieldManager.Instance.PlayerField;
        Field enemyField = FieldManager.Instance.EnemyField;

        for(int index = 0; index < playerField.Cards.Length; index++)
        {
            BattleCard(playerField.Cards[index], enemyField.Cards[index]);
        }

        effectStack.Sort((a, b) => a.OwnerCard.CardEffect.Priority.CompareTo(b.OwnerCard.CardEffect.Priority));

        StartCoroutine(ApplyEffect());
    }

    private void BattleCard(CardData playerCard, CardData enemyCard)
    {
        if(BattleCard(playerCard.CombatAttribute, enemyCard.CombatAttribute) == playerCard.EffectActivateCondition)
        {
            effectStack.Add(new EffectContainer(playerCard, enemyCard));
        }
        if (BattleCard(enemyCard.CombatAttribute, playerCard.CombatAttribute) == enemyCard.EffectActivateCondition)
        {
            effectStack.Add(new EffectContainer(enemyCard, enemyCard));
        }
    }

    // return EffectActivateCondition as left
    private EffectActivateCondition BattleCard(CombatAttribute left, CombatAttribute right)
    {
        if (left == right) return EffectActivateCondition.Draw;

        return (left, right) switch
        {
            (CombatAttribute.Fire, CombatAttribute.Grass) => EffectActivateCondition.Win,
            (CombatAttribute.Grass, CombatAttribute.Water) => EffectActivateCondition.Win,
            (CombatAttribute.Water, CombatAttribute.Fire) => EffectActivateCondition.Win,

            _ => EffectActivateCondition.Lose
        };
    }

    private IEnumerator ApplyEffect()
    {
        yield return new WaitForSeconds(1f);

        foreach (var effectContainer in effectStack)
        {
            effectContainer.ActivateEffect();

            yield return new WaitForSeconds(1f);
        }

        EndBattle();
    }

    private void EndBattle()
    {
        OnBattleComplete?.Invoke();
    }

    public class EffectContainer
    {
        public EffectContainer(CardData ownerCard, CardData opponentCard)
        {
            OwnerCard = ownerCard;
            OpponentCard = opponentCard;
        }

        public CardData OwnerCard {  get; set; }
        public CardData OpponentCard { get; set; }

        public void ActivateEffect()
        {
            if(OwnerCard.OwnerType == OwnerType.Player)
            {
                OwnerCard.CardEffect.ActivateEffect(PlayerStateBridge.GetPlayer(), EnemyStateBridge.GetEnemy());
            }
            else
            {
                OwnerCard.CardEffect.ActivateEffect(EnemyStateBridge.GetEnemy(), PlayerStateBridge.GetPlayer());
            }
        }
    }
}
