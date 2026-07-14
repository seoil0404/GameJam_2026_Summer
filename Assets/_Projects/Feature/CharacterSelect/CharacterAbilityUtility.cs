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
            return baseValue * 2;
        }
        else
        {
            return baseValue;
        }
    }
   
}
