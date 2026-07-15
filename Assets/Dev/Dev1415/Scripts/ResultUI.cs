using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public enum ResultType
    {
        Victory,
        Defeat
    }

    [Header("UI")]
    [SerializeField] private RectTransform character;
    [SerializeField] private TextMeshProUGUI titleText; // change to image
    [SerializeField] private RectTransform retryButton;
    [SerializeField] private RectTransform returnButton;


    [SerializeField] private ResultType resultType;

    private void Start()
    {
        SetCharacterPortrait();

        character.localScale = Vector3.zero;
        //titleText.rectTransform.localScale = Vector3.zero;
        retryButton.localScale = Vector3.zero;
        returnButton.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        PlayAnimation();
    }

    // 캐릭터 선택 씬에서 고른 캐릭터의 초상화를 승리/패배 결과창에 반영
    private void SetCharacterPortrait()
    {
        CharacterData selected = CharacterSelection.SelectedCharacter;
        if (selected == null || selected.Protrait == null)
            return;

        Image characterImage = character.GetComponent<Image>();
        if (characterImage != null)
        {
            characterImage.sprite = selected.Protrait;
        }
    }


    private void PlayAnimation(){
        //Sequence seq = DOTween.Sequence();
        Sequence seq = DOTween.Sequence();
       
        seq.Append(character.DOScale(1f, 0.4f).SetEase(Ease.OutBack)); //character



        

        if (resultType == ResultType.Victory)
        {
            titleText.alpha = 0;
            titleText.rectTransform.localScale = Vector3.one * 0.8f;
            seq.Append(titleText.DOFade(1, 0.5f));

        }
        else if(resultType == ResultType.Defeat)
        {
            titleText.alpha = 0;

            RectTransform rt = titleText.rectTransform; //move rectTransform
           Vector2 originalPos = rt.anchoredPosition; // save original position
            rt.anchoredPosition = originalPos + Vector2.up * 50; // move up 50 

            seq.Append(rt.DOAnchorPos(originalPos, 0.3f).SetEase(Ease.OutQuad)); // move back to original position

            seq.Join(titleText.DOFade(1, 0.3f)); // fade in

            seq.Join(rt.DOShakeRotation(1f, 5f, 15)); // title shake
        }

        //titleText.alpha = 0;
        //titleText.rectTransform.localScale = Vector3.one * 0.8f;
        //seq.Append(titleText.DOFade(1, 0.5f));

        seq.Join(
            titleText.rectTransform
                .DOScale(1f, 0.5f)
                .SetEase(Ease.OutCubic)
        );
        seq.Join(retryButton.DOScale(1f, 0.4f).SetEase(Ease.OutBack)); // retry
            
        seq.Join(returnButton.DOScale(1f, 0.4f).SetEase(Ease.OutBack)); // return

    }
}

