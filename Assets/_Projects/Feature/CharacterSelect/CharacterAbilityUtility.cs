using UnityEngine;

public static class CharacterAbilityUtility
{
    public static int ApplyJackpotChance(int baseValue)
    {
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
