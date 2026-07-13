using System;
using UnityEngine;

public class TestCardEffect1 : CardEffectBase
{
    public override string Name => "TestCard1";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        Debug.Log("TestCard1 Effect Activated");
    }
}