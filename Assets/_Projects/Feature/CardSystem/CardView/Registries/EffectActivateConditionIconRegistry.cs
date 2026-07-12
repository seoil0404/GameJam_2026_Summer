using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectActivateConditionIconRegistry", menuName = "Scriptable Objects/EffectActivateConditionIconRegistry")]
public class EffectActivateConditionIconRegistry : ScriptableObject
{
    [SerializeField] private Sprite winIcon;
    [SerializeField] private Sprite drawIcon;
    [SerializeField] private Sprite loseIcon;

    private Dictionary<EffectActivateCondition, Sprite> dictionary;

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

    public Sprite GetIcon(EffectActivateCondition condition)
    {
        if (dictionary == null)
            BuildDictionary();

        if (dictionary.TryGetValue(condition, out var sprite))
        {
            return sprite;
        }

        return null;
    }

    private void BuildDictionary()
    {
        dictionary = new Dictionary<EffectActivateCondition, Sprite>
        {
            { EffectActivateCondition.Win,  winIcon  },
            { EffectActivateCondition.Draw, drawIcon },
            { EffectActivateCondition.Lose, loseIcon },
        };
    }
}