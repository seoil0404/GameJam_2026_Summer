using UnityEngine;

public class TestCardEffect3 : CardEffectBase
{
    public override string Name => "Energy_Drink";
    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        owner.Health += 2;
        Debug.Log($"{owner.name} used {Name}, restoring 5 health. {owner.name}'s health is now {owner.Health}.");
    }
}