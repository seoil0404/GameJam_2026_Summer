using UnityEngine;

public static class CharacterSelection
{
    public static CharacterData SelectedCharacter { get; private set; }

    public static void Select(CharacterData character)
    {
        SelectedCharacter = character;
    }
}
