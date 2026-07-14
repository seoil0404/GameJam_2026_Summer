using UnityEngine;

public class TestCardEffect2 : CardEffectBase
{
    public override string Name => "Slash"; //ТќАн
    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        opponent.Health -= opponent.Health / 5;
        Debug.Log($"{opponent.name}used {Name}, {opponent.Health} dealing  5 damage");
    }
}