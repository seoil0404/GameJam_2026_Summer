using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Judge(CombatAttribute owner, CombatAttribute opponent)
    {
        if (owner == opponent)
            return 0;

        switch (owner)
        {
            case CombatAttribute.Rock:
                if (opponent == CombatAttribute.Scissor)
                    return 1;
                else
                    return -1;

            case CombatAttribute.Scissor:
                if (opponent == CombatAttribute.Paper)
                    return 1;
                else
                    return -1;


            case CombatAttribute.Paper:
                if (opponent == CombatAttribute.Rock)
                    return 1;
                else
                    return -1;
        }
        return 0;
    }
    public void ResolveCombat(CardData ownerCard, CardData opponentCard, Entity ownerEntity, Entity opponentEntity)
    {
        int result = Judge(ownerCard.CombatAttribute, opponentCard.CombatAttribute);

        EffectActivateCondition ownerOutcome = ConvertToCondition(result);
        EffectActivateCondition opponentOutcome = ConvertToCondition(-result);

        if (ownerCard.EffectActivateCondition == ownerOutcome)
            ownerCard.CardEffect.ActivateEffect(ownerEntity, opponentEntity);

        if (opponentCard.EffectActivateCondition == opponentOutcome)
            opponentCard.CardEffect.ActivateEffect(opponentEntity, ownerEntity);
    }

    private EffectActivateCondition ConvertToCondition(int result)
    {
        switch (result)
        {
            case 1:
                return EffectActivateCondition.Win;
            case 0:
                return EffectActivateCondition.Draw;
            case -1:
                return EffectActivateCondition.Lose;
        }
        return EffectActivateCondition.Draw;
    }

    public void ResolveTurn(CardData[] ownerField, CardData[] opponentField, Entity ownerEntity, Entity opponentEntity)
    {
        for (int i = 0; i < ownerField.Length; i++)
        {
            ResolveCombat(ownerField[i], opponentField[i], ownerEntity, opponentEntity);

        }
    }
}
