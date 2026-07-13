using UnityEngine;

public class CombatTestRunner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Test(CombatAttribute.Rock, CombatAttribute.Rock);
        Test(CombatAttribute.Rock, CombatAttribute.Scissor);
        Test(CombatAttribute.Rock, CombatAttribute.Paper);
        Test(CombatAttribute.Scissor, CombatAttribute.Rock);
        Test(CombatAttribute.Scissor, CombatAttribute.Scissor);
        Test(CombatAttribute.Scissor, CombatAttribute.Paper);
        Test(CombatAttribute.Paper, CombatAttribute.Rock);
        Test(CombatAttribute.Paper, CombatAttribute.Scissor);
        Test(CombatAttribute.Paper, CombatAttribute.Paper);
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
            case CombatAttribute.Rock:
                if (opponent == CombatAttribute.Scissor)
                    return 1;
                else
                    return -1;

            case CombatAttribute.Scissor:
                if (opponent == CombatAttribute.Paper)
                    return 1;
                else
                    return -1;


            case CombatAttribute.Paper:
                if (opponent == CombatAttribute.Rock)
                    return 1;
                else
                    return -1;
        }
        return 0;
    }
    
}
