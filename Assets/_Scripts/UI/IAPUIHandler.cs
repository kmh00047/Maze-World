using UnityEngine;
using UnityEngine.UI;

public class IAPUIHandler : MonoBehaviour
{
    [SerializeField] private Button IAPButton;
    [SerializeField] private Button IAPBackButton;

    [SerializeField] private GameObject IAPMenu;

    private void Start()
    {
        if(IAPBackButton != null && IAPButton != null)
        {
            IAPButton.onClick.AddListener(IAPButtonClicked);
            IAPBackButton.onClick.AddListener(IAPBackButtonClicked);
        }
    }

    private void IAPButtonClicked()
    {
        IAPMenu.SetActive(true);
    }

    private void IAPBackButtonClicked()
    {
        IAPMenu.SetActive(false);
    }
}
