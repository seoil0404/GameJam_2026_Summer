using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpriteRegistry", menuName = "Scriptable Objects/CardEffectSpriteRegistry")]
public class CardEffectSpriteRegistry : ScriptableObject
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }

    private Dictionary<string, Sprite> dictionary;

    private void OnEnable()
    {
        dictionary = null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        dictionary = null;
    }
#endif

    public Sprite GetSprite(string effectName)
    {
        if (string.IsNullOrEmpty(effectName))
            return null;

        if (dictionary == null)
            BuildCache();

        if (dictionary.TryGetValue(effectName, out var sprite))
            return sprite;

        return null;
    }

    private void BuildCache()
    {
        dictionary = new Dictionary<string, Sprite>(Sprites?.Length ?? 0);

        if (Sprites == null)
            return;

        foreach (var sprite in Sprites)
        {
            if (sprite == null)
                continue;

            if (dictionary.ContainsKey(sprite.name))

            {
                Debug.LogWarning($"[{name}] 이름이 중복된 스프라이트가 있습니다: {sprite.name}", this);
                continue;
            }

            dictionary.Add(sprite.name, sprite);
        }
    }
}