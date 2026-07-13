using UnityEngine;

public class Enemy : Entity
{
    public static Enemy Instance { get; private set; }

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