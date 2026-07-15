using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod]
    private static void GenerateSceneController()
    {
        GameObject sceneController = new GameObject(typeof(SceneController).Name);
        sceneController.AddComponent<SceneController>();
    }
#endif

    private static SceneController instance { get; set; }

    [SerializeField] private GameObject fadeOutViewPrefab;
    [SerializeField] private GameObject fadeInViewPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;

    }

    private void Start()
    {

    }

    public static void LoadScene(SceneType sceneType)
    {
        instance.StartCoroutine(LoadSceneByFade(sceneType));
    }

    private static IEnumerator LoadSceneByFade(SceneType sceneType)
    {
        Instantiate(instance.fadeOutViewPrefab);

        AudioManager.Instance.PlaySFX("2-2");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneType.ToString());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Instantiate(instance.fadeInViewPrefab);
    }
}
