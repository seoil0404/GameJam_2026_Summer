using UnityEngine;
using TMPro;

public class RankingText : MonoBehaviour
{
    // 프리팹 내부의 텍스트 컴포넌트 연결용
    public TextMeshProUGUI rankText;

    // 순위와 시간을 받아 "1. 12.34" 형태로 텍스트를 셋팅해주는 함수
    public void SetText(int rank, float time)
    {
        // 소수점 2자리 포맷("F2")을 지정하여 원하는 형태로 출력
        rankText.text = $"{rank}. {time:F2}";
    }
}