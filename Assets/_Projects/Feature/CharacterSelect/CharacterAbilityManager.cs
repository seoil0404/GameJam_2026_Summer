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
        Debug.Log($"[턴 카운트 확인용] OnBattleComplete 호출됨 -> turnCount = {turnCount}");
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
        int cappedTarget = Mathf.Min(player.Health + healAmount, maxHealth);
        player.Health = Mathf.Max(player.Health, cappedTarget); // 이미 20을 넘겨둔 상태면 힐로 깎이지 않게 유지

        AudioManager.Instance.PlaySFX("5-1");

        Debug.Log($"{character.CharacterName} 정상작동 (체력 {healAmount} 회복, 현재 체력 {player.Health})");
    }
}