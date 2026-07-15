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
            base.Health = value < 0 ? 0 : value;
            if (base.Health == 0)
            {
                GameFlowManager.instance.Victory();
            }

            healthView.SetHealthText(value);
        }
    }

    public override HealthView HealthView => healthView;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        // 테스트용 치트: K 누르면 적 체력 0으로 만들어서 승리씬으로 이동
        if (Input.GetKeyDown(KeyCode.K))
        {
            Health = 0;
        }
    }
}