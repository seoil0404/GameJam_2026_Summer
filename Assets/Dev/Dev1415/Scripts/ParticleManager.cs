using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance = null; // 메모리 할당 변수

    private void Awake()  // singleton pattern
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }










        void Start() {


        
        
        
        }









    }










    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
