using UnityEngine;

public class EnemyInstanceTestRunner : MonoBehaviour
{
    private void Start()
    {
        Enemy enemy = gameObject.AddComponent<Enemy>();

        Debug.Log($"[테스트] Enemy.Instance가 방금 만든 enemy와 같음: {Enemy.Instance == enemy}");

        Entity fromBridge = EnemyStateBridge.GetEnemy();
        Debug.Log($"[테스트] GetEnemy()가 null 아님: {fromBridge != null}");
        Debug.Log($"[테스트] GetEnemy()가 enemy와 같음: {fromBridge == enemy}");
    }
}
