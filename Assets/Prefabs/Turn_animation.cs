using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Turn_animation : MonoBehaviour
{
    private enum TurnState
    {
        PlayerTurn,
        EnemyTurn,
        BattleTurn
    }

    [Header("UI")]
    [SerializeField] private Image turnImage;
    [SerializeField] private RectTransform rect;
    [SerializeField] private TurnState turnstate;


    private void Start()
    {
       Sequence seq = DOTween.Sequence();
       PlayAnimation();
    }

    private void PlayAnimation()
    {
        Sequence seq = DOTween.Sequence();
        rect.anchoredPosition = new Vector2(-1300, 290);
        seq.Append(rect.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic));
        //seq.Append(rect.DOScale(1.2f, 0.5f));

        //seq.AppendInterval(0.5f);


        //seq.Append(rect.DOScale(1f, 0.15f));
        seq.Append(rect.DOAnchorPosX(1300, 0.5f).SetEase(Ease.OutCubic));


        //rect.DOAnchorPosX(0, 0.5f);
        //rect.DOAnchorPosX(800, 0.5f);

    }



}
