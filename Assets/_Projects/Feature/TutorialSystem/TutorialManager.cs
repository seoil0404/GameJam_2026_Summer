using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialView> tutorialViewPrefabs = new List<TutorialView>();

    private int index = 0;
    private TutorialView previousTutorialView = null;

    private void Start()
    {
        ProcessView();
    }

    public void ProcessView()
    {
        if(index >= tutorialViewPrefabs.Count)
        {
            SceneController.LoadScene(SceneType.StartScene);
            return;
        }

        if(previousTutorialView != null)
        {
            Destroy(previousTutorialView.gameObject);
        }

        previousTutorialView = Instantiate(tutorialViewPrefabs[index]);
        index++;
    }
}