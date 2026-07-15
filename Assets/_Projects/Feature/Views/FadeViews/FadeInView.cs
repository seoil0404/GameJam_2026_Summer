using UnityEngine;

public class FadeInView : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
