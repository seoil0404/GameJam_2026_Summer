using UnityEngine;

public class Entity : MonoBehaviour
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

    public int LoseAmount { get; set; } = 0;
}
