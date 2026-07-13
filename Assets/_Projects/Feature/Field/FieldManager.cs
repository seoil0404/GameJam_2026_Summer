using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    [field: SerializeField] public Field PlayerField { get; set; }
    [field: SerializeField] public Field EnemyField { get; set; }

    [field: SerializeField] public FieldSlotView[] PlayerFieldSlotViews { get; set; }
    [field: SerializeField] public FieldSlotView[] EnemyFieldSlotViews { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }
}
