using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private string bgmName;

    private void Start()
    {
        AudioManager.Instance.SetBGM(bgmName, 1f);
    }
}
