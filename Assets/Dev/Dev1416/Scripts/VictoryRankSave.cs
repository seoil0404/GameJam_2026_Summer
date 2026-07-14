using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryRankSave : MonoBehaviour
{
    public void OnClickRegisterRecord()
    {
        // 이름 없이 현재 달성한 시간만 랭킹 목록에 바로 등록 및 파일 저장
        TimerManager.Instance.AddNewRecord();
    }
}