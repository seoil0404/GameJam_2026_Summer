using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CardViewAnimationController : MonoBehaviour
{
    [SerializeField] private Image cardEffectImage;
    [SerializeField] private Image combatAttributeIconView;
    [SerializeField] private Image effectActivateConditionIconView;
    [SerializeField] private Sprite backgroundSprite;

    private Animator animator;
    private Sprite previousSprite = null;

    public event Action OnCardReversed;

    public bool bIsTab
    {
        set
        {
            animator.SetBool("IsTab", value);
        }
    }

    private bool isFlip = false;
    public bool bIsFlip
    {
        get
        {
            return isFlip;
        }
        set
        {
            if(isFlip != value)
            {
                if (value)
                {
                    previousSprite = cardEffectImage.sprite;
                    cardEffectImage.sprite = backgroundSprite;

                    combatAttributeIconView.gameObject.SetActive(false);
                    effectActivateConditionIconView.gameObject.SetActive(false);
                }
                else
                {
                    cardEffectImage.sprite = previousSprite;

                    combatAttributeIconView.gameObject.SetActive(true);
                    effectActivateConditionIconView.gameObject.SetActive(true);
                }
            }
            isFlip = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OverCardFight()
    {
        animator.SetTrigger("OnBattleStart_Over");
    }

    public void UnderCardFight()
    {
        animator.SetTrigger("OnBattleStart_Under");
    }

    public void FightEnd()
    {
        animator.SetTrigger("OnBattleEnd");
    }

    public void Destroy()
    {
        animator.SetTrigger("Destroy");
    }

    public void OnCardPickUp()
    {
        animator.SetTrigger("OnCardPickUp");
        animator.ResetTrigger("OnCardPickDown");
    }

    public void OnCardPickDown()
    {
        animator.SetTrigger("OnCardPickDown");
    }

    public void OnAttatch()
    {
        animator.SetTrigger("OnAttatch");
    }

    public void OnDestroyAnimationEnded()
    {
        Destroy(gameObject);
    }

}
