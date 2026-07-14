using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField] private string characterName;

    [TextArea(3, 10)]
    [SerializeField] private string description;

    [SerializeField] private Sprite protrait;

    [Header("능력치 - 놀부 (자원 비축형)")]
    [Tooltip("기본 손패 개수에 더해지는 보너스 장수")]
    [SerializeField] private int handSizeBonus;

    [Header("능력치 - 거머리 (주기 회복형)")]
    [Tooltip("몇 턴마다 회복하는지 (0이면 이 능력 사용 안 함)")]
    [SerializeField] private int healIntervalTurns;
    [Tooltip("상대 현재 HP의 몇 %를 회복하는지 (0.3 = 30%)")]
    [SerializeField] private float healPercentOfEnemyHp;

    [Header("능력치 - 잭팟 (도박형)")]
    [Tooltip("효과 발동 시 2배가 될 확률 (0.2 = 20%)")]
    [SerializeField] private float effectDoubleChance;

    public string CharacterName => characterName;
    public string Description => description;
    public Sprite Protrait => protrait;

    public int HandSizeBonus => handSizeBonus;
    public int HealIntervalTurns => healIntervalTurns;
    public float HealPercentOfEnemyHp => healPercentOfEnemyHp;
    public float EffectDoubleChance => effectDoubleChance;
}