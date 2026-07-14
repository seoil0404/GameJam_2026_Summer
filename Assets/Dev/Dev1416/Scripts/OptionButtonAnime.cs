using UnityEngine;

public class OptionButtonAnime : MonoBehaviour
{
    public Animator animator; //버튼에 마우스 갖다대면

    public void OnButton() // 마우스 올리기
    {
            animator.SetBool("IsOnMouse", true);
    }
    public void ExitButton() // 마우스 내리기
    {
        animator.SetBool("IsOnMouse", false);
    }
}
