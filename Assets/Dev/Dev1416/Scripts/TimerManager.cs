using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour // 랭킹 기록 밑 시간 재는 클래스
{
    public static TimerManager Instance { get; private set; }

    [Header("Timer Settings")]
    public float currentTimer = 0f;
    private bool isTimerRunning = false;

    private string savePath;
    private RankingData rankingData = new RankingData();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "ranking_time_only.json");
        LoadRanking();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTimer += Time.deltaTime;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            ResetTimer();
        }
        else if (scene.name == "CharacterSelectScene" && !isTimerRunning && currentTimer == 0f)
        {
            StartTimer();
        }
        else if (scene.name == "SucessScene")
        {
            StopTimer();
        }
        else if (scene.name == "DefeatScene")
        {
            StopTimer();
        }
    }

    public void StartTimer() => isTimerRunning = true;
    public void StopTimer() => isTimerRunning = false;
    public void ResetTimer()
    {
        isTimerRunning = false;
        currentTimer = 0f;
    }

    public string GetFormattedTime()
    {
        return currentTimer.ToString("F2");
    }

    // [이름 없이 시간만 새 기록으로 추가]
    public void AddNewRecord()
    {
        // 소수점 2자리까지만 반올림하여 데이터 정제
        float finalizedTime = Mathf.Round(currentTimer * 100f) / 100f;
        Debug.Log($"{finalizedTime}초");

        rankingData.rankList.Add(finalizedTime);

        // 오름차순(짧은 시간 순) 정렬
        rankingData.rankList.Sort();

        // 100개 이상 기록을 유지하기 위해 최대 200개까지 저장하도록 제한 규칙 설정
        if (rankingData.rankList.Count > 200)
        {
            rankingData.rankList.RemoveRange(200, rankingData.rankList.Count - 200);
        }

        SaveRanking();
    }

    public List<float> GetRankList() => rankingData.rankList;

    private void SaveRanking()
    {
        string json = JsonUtility.ToJson(rankingData, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadRanking()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            rankingData = JsonUtility.FromJson<RankingData>(json);
        }
    }
    // [랭킹 데이터 완전 초기화 함수]
    public void ResetAllRanking()
    {
        // 1. 메모리에 있는 리스트 비우기
        rankingData.rankList.Clear();

        // 2. 하드디스크에 저장된 JSON 파일 삭제하기
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("랭킹 세이브 파일이 삭제되었습니다.");
        }
        else
        {
            Debug.Log("삭제할 세이브 파일이 존재하지 않습니다.");
        }
    }
}