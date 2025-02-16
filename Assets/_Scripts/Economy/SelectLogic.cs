using UnityEngine;
using UnityEngine.UI;

public class SelectLogic : MonoBehaviour
{
    [SerializeField] private int Button_Index;
    [SerializeField] private bool isSkin;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    private void Select()
    {
        if (isSkin)
        {
            ShopManagement.instance.SelectSkin(Button_Index);
        }
        else
        {
            ShopManagement.instance.SelectTheme(Button_Index);
        }
    }
}
