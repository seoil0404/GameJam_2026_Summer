using System;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectSlot : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Button selectButton;

    public CharacterData CharacterData => characterData;
    public event Action<CharacterSelectSlot> OnClicked;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetView();
        selectButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("2-3");
            OnClicked?.Invoke(this);
        });
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
        // 선택 강조 표시는 현재 사용하지 않음.
    }
}