using UnityEngine;

public class CombatTestRunner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Test(CombatAttribute.Fire, CombatAttribute.Fire);
        Test(CombatAttribute.Fire, CombatAttribute.Water);
        Test(CombatAttribute.Fire, CombatAttribute.Grass);
        Test(CombatAttribute.Water, CombatAttribute.Fire);
        Test(CombatAttribute.Water, CombatAttribute.Water);
        Test(CombatAttribute.Water, CombatAttribute.Grass);
        Test(CombatAttribute.Grass, CombatAttribute.Fire);
        Test(CombatAttribute.Grass, CombatAttribute.Water);
        Test(CombatAttribute.Grass, CombatAttribute.Grass);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Test(CombatAttribute owner, CombatAttribute opponent)
    {
        int result = Judge(owner, opponent);
        Debug.Log($"{owner} vs {opponent} = {result}");

    }
    public int Judge(CombatAttribute owner, CombatAttribute opponent)
    {
        if (owner == opponent)
            return 0;

        switch (owner)
        {
            case CombatAttribute.Fire:
                if (opponent == CombatAttribute.Water)
                    return 1;
                else
                    return -1;

            case CombatAttribute.Water:
                if (opponent == CombatAttribute.Grass)
                    return 1;
                else
                    return -1;


            case CombatAttribute.Grass:
                if (opponent == CombatAttribute.Fire)
                    return 1;
                else
                    return -1;
        }
        return 0;
    }
    
}
