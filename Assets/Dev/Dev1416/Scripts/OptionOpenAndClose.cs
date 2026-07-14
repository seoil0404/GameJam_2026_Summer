using System.Xml.Serialization;
using UnityEngine;

public class OptionOpenAndClose : MonoBehaviour
{
    public static OptionOpenAndClose Instance { get; private set; }
    public bool isOpen = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OpenOption()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().SetBool("OpenClose", true);
        isOpen = true;
    }
    public void CloseOption()
    {
        gameObject.GetComponent<Animator>().SetBool("OpenClose", false);
        isOpen = false;
    }
    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
