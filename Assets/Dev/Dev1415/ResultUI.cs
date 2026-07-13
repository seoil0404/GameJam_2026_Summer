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
        character.localScale = Vector3.zero;
        //titleText.rectTransform.localScale = Vector3.zero;
        retryButton.localScale = Vector3.zero;
        returnButton.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        PlayAnimation();
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

