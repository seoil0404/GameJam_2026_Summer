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

        if(character == null)
        {
            return baseValue;
        }
        if(UnityEngine.Random.value < character.EffectDoubleChance)
        {
            Debug.Log($"{character.CharacterName} 정상작동 (효과 2배: {baseValue} -> {baseValue * 2})");
            return baseValue * 2;
        }
        else
        {
            return baseValue;
        }
    }
   
}
