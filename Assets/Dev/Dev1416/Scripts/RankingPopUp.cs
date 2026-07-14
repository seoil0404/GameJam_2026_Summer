using System.Collections.Generic;
using UnityEngine;

public class RankingPopUp : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject rankSlotPrefab; // 1단계에서 만든 프리팹
    [SerializeField] private Transform contentParent;   // 자식으로 배치될 부모 오브젝트 (예: ScrollView의 Content)
    public bool isActive = false;
    public static RankingPopUp Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

    }
    // 순위표 화면이 켜질 때 자동으로 실행되는 유니티 생명주기 함수
    private void OnEnable()
    {
        UpdateLeaderboard();
    }

    // 순위표 화면이 꺼질 때 자동으로 실행되는 함수
    private void OnDisable()
    {
        ClearLeaderboard();
    }

    // [핵심 함수] 순위표 데이터를 새로고침하는 함수
    public void UpdateLeaderboard()
    {
        // 1. 기존에 생성되어 있던 자식 오브젝트들을 완전히 비우기
        ClearLeaderboard();

        if (TimerManager.Instance == null)
        {
            Debug.LogError("GameRecordManager를 찾을 수 없습니다.");
            return;
        }

        // 2. 매니저로부터 저장되어 정렬된 랭킹 리스트 가져오기
        List<float> rankList = TimerManager.Instance.GetRankList();

        // 3. 데이터 수만큼 프리팹을 생성하여 자식으로 등록
        for (int i = 0; i < rankList.Count; i++)
        {
            // 프리팹 생성하면서 contentParent의 자식으로 바로 집어넣기
            GameObject slotGo = Instantiate(rankSlotPrefab, contentParent);

            // 프리팹에 붙어있는 스크립트 컴포넌트 가져오기
            RankingText slotScript = slotGo.GetComponent<RankingText>();

            if (slotScript != null)
            {
                int currentRank = i + 1; // 인덱스는 0부터 시작하므로 순위는 +1
                float clearTime = rankList[i];

                // 텍스트 세팅 (예: "1. 45.67")
                slotScript.SetText(currentRank, clearTime);
            }
        }
    }

    // [자식 오브젝트 완전히 비우는 함수]
    public void ClearLeaderboard()
    {
        Debug.Log("삭제");
        // 부모 오브젝트(contentParent) 밑에 있는 모든 자식을 반복문으로 파괴
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
        isActive = true;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
    // 랭킹 창에 만들 '초기화 버튼'에 연결할 함수
    public void OnClickResetButton()
    {
        if (TimerManager.Instance == null) return;

        // 1. 데이터 및 파일 초기화 실행
        TimerManager.Instance.ResetAllRanking();

        // 2. 현재 화면에 생성되어 있던 프리팹 자식들을 지우고 다시 그려서 빈 화면으로 갱신
        UpdateLeaderboard();
    }
}