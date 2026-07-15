using UnityEngine;

public static class CharacterAbilityUtility
{
    public static int ApplyJackpotChance(int baseValue, Entity owner)
    {
        // 잭팟은 플레이어가 선택했을 때 플레이어 카드 효과에만 적용되어야 함. 적 카드 효과에는 적용 안 함.
        if (!(owner is Player))
        {
            return baseValue;
        }

        CharacterData character = CharacterSelection.SelectedCharacter;

        // 잭팟 캐릭터(EffectDoubleChance > 0)가 아니면 발동 자체가 없는 것이므로 사운드도 재생하지 않음
        if(character == null || character.EffectDoubleChance <= 0)
        {
            return baseValue;
        }

        AudioManager.Instance.PlaySFX("5-2"); // 잭팟 효과 발동음

        if(UnityEngine.Random.value < character.EffectDoubleChance)
        {
            AudioManager.Instance.PlaySFX("5-3"); // 잭팟 성공음 (2배)
            Debug.Log($"{character.CharacterName} 정상작동 (효과 2배: {baseValue} -> {baseValue * 2})");
            return baseValue * 2;
        }
        else
        {
            return baseValue;
        }
    }
   
}
