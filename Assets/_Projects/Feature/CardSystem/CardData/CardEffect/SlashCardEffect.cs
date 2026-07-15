using UnityEngine;

public class SlashCardEffect : CardEffectBase
{
    public override string Name => "Slash"; //����

    public override int Priority => 0;

    public override string Description => "ȿ�� : \n �ߵ� �� ��� ü���� 20%�� ���ظ� ���մϴ�";

    public override void ActivateEffect(Entity owner, Entity opponent)
    {
        int slashDamage = opponent.Health / 5;
        opponent.Health -= CharacterAbilityUtility.ApplyJackpotChance(slashDamage, owner);
        EffectToOpponent(owner, opponent);
        //Debug.Log($"{opponent.name}used {Name}, {opponent.Health} dealing  5 damage");
    }
}