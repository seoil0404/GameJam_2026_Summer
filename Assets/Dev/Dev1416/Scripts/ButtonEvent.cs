using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private OptionOpenAndClose OptionOCMan;
    public Animator animator; //버튼에 마우스 갖다대면
    //public GameObject Option;
    bool canAnime = false;

    private void Awake()
    {
        OptionOCMan = OptionOpenAndClose.Instance;
    }
    public void SceneMove() //씬 메인으로 이동
    {
        Debug.Log("버튼클릭");

        SceneController.LoadScene(SceneType.MainScene);
    }
    public void GameQuit() // 게임 종료
    {
        Debug.Log("나가기클릭");
    }
    public void OptionActive() // 창 열고닫기
    {
        if (!OptionOCMan.isOpen)
        {
            Debug.Log("옵션활성화");
            OptionOCMan.OpenOption();
        }

        else
        {
            Debug.Log("옵션비활성화");
            OptionOCMan.CloseOption();
        }
    }
    public void CanAnime() // 시작시 애니메이션 재생 완료 후에 다은 애니메이션ㅇ ㅣ되게하기
    {
        canAnime = true;
        animator.SetBool("CanAnime", true);
    }

    public void OnButton() // 마우스 올리기
    {
        if (canAnime)
        {
            animator.SetBool("IsOnMouse", true);
        }
    }
    public void ExitButton() // 마우스 내리기
    {
        if (canAnime)
        {
            animator.SetBool("IsOnMouse", false);
        }
    }
}
