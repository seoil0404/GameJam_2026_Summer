using UnityEngine;

public class EnergeDrinkCardEffect : CardEffectBase
{
    public override string Name => "EnergyDrink";

    public override int Priority => 0;

    public override string Description => "ȿ�� : \n�ߵ� �� ü���� 2 ȸ���մϴ�";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        owner.Health += CharacterAbilityUtility.ApplyJackpotChance(2, owner);
        EffectToOwner(owner, opponent);
        //Debug.Log($"{owner.name} used {Name}, restoring 5 health. {owner.name}'s health is now {owner.Health}.");
    }
}