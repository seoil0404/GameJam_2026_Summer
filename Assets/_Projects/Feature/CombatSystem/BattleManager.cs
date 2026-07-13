using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleComplete;

    private void Awake()
    {
        Instance = this;
    }

    public void StartBattle()
    {
        Field playerField = FieldManager.Instance.PlayerField;
        Field enemyField = FieldManager.Instance.EnemyField;

        for(int index = 0; index < playerField.Cards.Length; index++)
        {
            BattleCard(playerField.Cards[index], enemyField.Cards[index]);
        }

        GenerateVisualEffect();
    }

    private void BattleCard(CardData playerCard, CardData enemyCard)
    {
        if(BattleCard(playerCard.CombatAttribute, enemyCard.CombatAttribute) == playerCard.EffectActivateCondition)
        {
            playerCard.CardEffect.ActivateEffect(PlayerStateBridge.GetPlayer(), EnemyStateBridge.GetEnemy());
        }
        if (BattleCard(enemyCard.CombatAttribute, playerCard.CombatAttribute) == enemyCard.EffectActivateCondition)
        {
            enemyCard.CardEffect.ActivateEffect(EnemyStateBridge.GetEnemy(), PlayerStateBridge.GetPlayer());
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

    private void GenerateVisualEffect()
    {
        EndBattle();
    }

    private void EndBattle()
    {
        OnBattleComplete?.Invoke();
    }
}
