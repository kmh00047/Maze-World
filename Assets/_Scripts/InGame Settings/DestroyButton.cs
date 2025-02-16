using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    public int coinThreshold = 1000; 
    public int button_index;
    public bool isSkin;

    private int coinCount; 
    private string buttonKey; 

    void Start()
    {
        buttonKey = "ButtonDestroyed_" + gameObject.name; // Unique key for this button

        // Check if button was destroyed before
        if (PlayerPrefs.GetInt(buttonKey, 0) == 1)
        {
            Destroy(gameObject); // Destroy if previously removed
            return;
        }

        GetComponent<Button>().onClick.AddListener(CheckAndDestroy);
    }

    void CheckAndDestroy()
    {
        coinCount = AudioManager.coinCount;
        if(isSkin)
        {
            ShopManagement.instance.PurchaseSkin(button_index);
        }
        else
        {
            Debug.Log("Calling Purchase skin from Shop Management");
            ShopManagement.instance.PurchaseTheme(button_index);
        }

        if (coinCount >= coinThreshold)
        {
            Debug.Log("Button destroyed because coin count is " + coinCount);
            PlayerPrefs.SetInt(buttonKey, 1); // Save button state
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough coins! You need at least " + coinThreshold);
        }
    }
}
