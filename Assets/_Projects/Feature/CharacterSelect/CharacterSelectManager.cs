using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField] private CharacterSelectSlot[] slots;
    [SerializeField] private Text detailNameText;
    [SerializeField] private Text detailDescriptionText;
    [SerializeField] private Button confirmButton;

    private CharacterData selectedCharacter;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        foreach (var slot in slots)
        {
            slot.OnClicked += OnSlotClicked;
        }
        confirmButton.onClick.AddListener(OnConfirm);
        confirmButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSlotClicked(CharacterSelectSlot clickedSlot)
    {
        selectedCharacter = clickedSlot.CharacterData;

        foreach (var slot in slots)
        {
            slot.SetHighlight(slot == clickedSlot);
        }

        detailNameText.text = selectedCharacter.CharacterName;
        detailDescriptionText.text = selectedCharacter.Description;

        confirmButton.interactable = true;
    }

    private void OnConfirm()
    {
        if(confirmButton == null)
        {
            return;
        }

        CharacterSelection.Select(selectedCharacter);
        SceneController.LoadScene(SceneType.MainScene);
    }


}
