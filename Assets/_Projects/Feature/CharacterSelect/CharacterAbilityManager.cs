using UnityEngine;

public class CharacterAbilityManager : MonoBehaviour
{
    private int turnCount = 0;

    private void Start()
    {
        BattleManager.Instance.OnBattleComplete += OnBattleComplete;
    }

    private void OnBattleComplete()
    {
        turnCount++;
        TryApplyPeriodicHeal();
    }

    private void TryApplyPeriodicHeal()
    {
        CharacterData character = CharacterSelection.SelectedCharacter;

        if (character == null || character.HealIntervalTurns <= 0)
            return;

        if (turnCount % character.HealIntervalTurns != 0)
            return;

        Entity player = Player.Instance;
        Entity enemy = Enemy.Instance;

        const int maxHealth = 20;
        int healAmount = Mathf.RoundToInt(enemy.Health * character.HealPercentOfEnemyHp);
        player.Health = Mathf.Min(player.Health + healAmount, maxHealth);
    }
}