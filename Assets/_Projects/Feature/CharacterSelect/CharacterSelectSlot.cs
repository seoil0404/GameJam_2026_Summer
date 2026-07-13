using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectSlot : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Button selectButton;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetView();
        selectButton.onClick.AddListener(OnSelect);

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

    private void OnSelect()
    {
        CharacterSelection.Select(characterData);
        SceneController.LoadScene(SceneType.MainScene);

    }
}
