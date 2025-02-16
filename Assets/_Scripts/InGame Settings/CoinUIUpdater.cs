using TMPro;
using UnityEngine;

public class CoinUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if (text != null && AudioManager.instance != null)
        {
            text.text = "NitCoins: " + AudioManager.coinCount;
            Debug.Log("Coins UI updated");
        }
        else
        {
            Debug.Log("CoinUIUpdater: Missing TMP_Text or AudioManager instance!");
        }
    }
}
