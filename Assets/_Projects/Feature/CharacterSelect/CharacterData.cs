using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [SerializeField] private string characterName;
    [SerializeField] private string description;
    [SerializeField] private Sprite protrait;


    public string CharacterName => characterName;
    public string Description => description;
    public Sprite Protrait => protrait;

}
