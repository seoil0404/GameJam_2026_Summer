using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatAttributeIconRegistry", menuName = "Scriptable Objects/CombatAttributeIconRegistry")]
public class CombatAttributeIconRegistry : ScriptableObject
{
    [SerializeField] private Sprite fireIcon;
    [SerializeField] private Sprite waterIcon;
    [SerializeField] private Sprite grassIcon;

    private Dictionary<CombatAttribute, Sprite> dictionary;

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

    public Sprite GetIcon(CombatAttribute combatAttribute)
    {
        if (dictionary == null)
            BuildDictionary();

        if (dictionary.TryGetValue(combatAttribute, out var sprite))
        {
            return sprite;
        }

        return null;
    }

    private void BuildDictionary()
    {
        dictionary = new Dictionary<CombatAttribute, Sprite>
        {
            { CombatAttribute.Fire,     fireIcon    },
            { CombatAttribute.Water,    waterIcon },
            { CombatAttribute.Grass,    grassIcon   },
        };
    }
}