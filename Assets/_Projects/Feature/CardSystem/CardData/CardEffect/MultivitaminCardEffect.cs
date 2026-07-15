using UnityEngine;

public class MultivitaminCardEffect : CardEffectBase
{
    public override string Name => "Multivitamin"; //Á¾ÇÕºñÅ¸¹Î

    public override int Priority => 0;

    public override string Description => "Description";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        int healthAmount = (20 - owner.Health) / 5;
        owner.Health += CharacterAbilityUtility.ApplyJackpotChance(healthAmount, owner);
        EffectToOwner(owner, opponent);
    }
}