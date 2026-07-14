using System;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectSlot : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Outline selectedOutline;

    public CharacterData CharacterData => characterData;
    public event Action<CharacterSelectSlot> OnClicked;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetView();
        selectButton.onClick.AddListener(() => OnClicked?.Invoke(this));
        SetHighlight(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetView()
    {
        if (characterData == null)
        {
            return;

        }

        portraitImage.sprite = characterData.Protrait;
        nameText.text = characterData.CharacterName;
    }

    public void SetHighlight(bool isSelected)
    {
        if (selectedOutline != null)
        {
            selectedOutline.enabled = isSelected;
        }
    }
}