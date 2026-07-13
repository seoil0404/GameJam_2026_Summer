using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    [field: SerializeField] public Field PlayerField { get; set; }
    [field: SerializeField] public Field EnemyField { get; set; }

    [SerializeField] private FieldSlotView[] playerFieldSlotViews;
    [SerializeField] private FieldSlotView[] enemyFieldSlotViews;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int index = 0; index < playerFieldSlotViews.Length; index++)
        {

        }
    }
}
