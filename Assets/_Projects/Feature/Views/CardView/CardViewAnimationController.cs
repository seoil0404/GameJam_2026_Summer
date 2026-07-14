using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CardViewAnimationController : MonoBehaviour
{
    [SerializeField] private CardView currentCardView;
    private Animator animator;

    public event Action OnCardReversed;

    public bool bIsTab
    {
        set
        {
            animator.SetBool("IsTab", value);
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
