using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected int health = 20;

    public virtual int Health
    {
        get
        {
            return health;
        }
        set
        {
            LoseAmount += value - health;

            health = value;
        }
    }

    public abstract HealthView HealthView { get; }

    public int LoseAmount { get; set; } = 0;
}
