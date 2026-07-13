using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    public Field PlayerField { get; private set; }
    public Field EnemyField { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}
