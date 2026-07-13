using UnityEngine;
using DG.Tweening;

public class ButtonEvent_2 : MonoBehaviour
{
    public void SceneMove_Retry()
    {
        //transform
        //    .DOPunchScale(Vector3.one * 0.15f, 0.2f) // Dotween animation
        //    .OnComplete(() =>
        //    {
        //        Debug.Log("재도전 클릭");
        //        SceneController.LoadScene(SceneType.MainScene);
        //    });

        
        transform
            .DOScale(0.9f, 0.1f)
            .OnComplete(() =>
            {
        transform
            .DOScale(1f, 0.1f)
            .OnComplete(() =>
            {
                SceneController.LoadScene(SceneType.MainScene);
            });
            });

    }




    public void SceneMove_Return()
    {
        transform
            .DOScale(0.9f, 0.1f)
            .OnComplete(() =>
            {
                transform
                    .DOScale(1f, 0.1f)
                    .OnComplete(() =>
                    {
                        SceneController.LoadScene(SceneType.StartScene);
                    });
            });
    }
}