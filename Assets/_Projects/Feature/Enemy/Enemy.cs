using UnityEngine;

public class Enemy : Entity
{
    public static Enemy Instance { get; private set; }

    [SerializeField] private HealthView healthView;

    public override int Health
    {
        get
        {
            return base.Health;
        }
        set
        {
            base.Health = value;
            healthView.SetHealthText(value);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}