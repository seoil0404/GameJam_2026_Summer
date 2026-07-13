using UnityEngine;

public enum Attribute //속성(불, 물, 풀)
{
    Fire,
    Wate,
    Grass,
}
public enum VictoryConditions //승리 기준(승리,무승부,패베)
{
    Fire,
    Grass,
    Water
}
public class CardEffect : MonoBehaviour
{
    public Attribute attribute;
    public VictoryConditions victoryConditions;

    public Sprite cardImage; // 카드 이미지

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
