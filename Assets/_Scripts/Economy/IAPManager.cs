using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

[Serializable]
public class InAppPurchaseRecord
{
    public string quantity; // e.g. "1", "3", etc.
}

[Serializable]
public class InAppPurchaseData
{
    public InAppPurchaseRecord[] in_app;
}

public class IAPManager : MonoBehaviour, IStoreListener
{
    [SerializeField] private string nitcoins1000;
    [SerializeField] private string nitcoins3000;
    [SerializeField] private string nitcoins5000;

    // References to your TextMeshProUGUI components on the buttons
    [SerializeField] private TextMeshProUGUI priceText1000;
    [SerializeField] private TextMeshProUGUI priceText3000;
    [SerializeField] private TextMeshProUGUI priceText5000;

    private IStoreController storeController;

    // Called when Unity IAP has successfully initialized and products are fetched
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        UpdatePriceLabels(storeController.products.all);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Unity IAP Initialization Failed: " + error);
    }

    // This method is called upon a successful purchase.
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        OnPurchaseComplete(args.purchasedProduct);
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseComplete(Product product)
    {
        int quantityMultiplier = GetQuantityFromReceipt(product.receipt);

        if (product.definition.id == nitcoins1000)
        {
            AudioManager.instance.AddCoins(1000 * quantityMultiplier);
        }
        else if (product.definition.id == nitcoins3000)
        {
            AudioManager.instance.AddCoins(3000 * quantityMultiplier);
        }
        else if (product.definition.id == nitcoins5000)
        {
            AudioManager.instance.AddCoins(5000 * quantityMultiplier);
        }
        else
        {
            Debug.Log("Invalid product id!");
        }
    }

    // Parse the receipt JSON to sum up the "quantity" fields.
    private int GetQuantityFromReceipt(string receipt)
    {
        int totalQuantity = 1; // default to 1 if parsing fails

        try
        {
            InAppPurchaseData data = JsonUtility.FromJson<InAppPurchaseData>(receipt);
            if (data != null && data.in_app != null && data.in_app.Length > 0)
            {
                totalQuantity = 0;
                foreach (var record in data.in_app)
                {
                    int qty = 1;
                    if (!string.IsNullOrEmpty(record.quantity))
                    {
                        int.TryParse(record.quantity, out qty);
                    }
                    totalQuantity += qty;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Failed to parse receipt for quantity. Defaulting to 1. Error: " + ex.Message);
        }
        return totalQuantity;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        string output = "Purchase Failed, Reason: " + failureDescription;
        Debug.Log(output);
    }

    // Update the TextMeshProUGUI texts on your buttons with the localized price.
    public void UpdatePriceLabels(Product[] products)
    {
        foreach (Product product in products)
        {
            if (product.definition.id == nitcoins1000 && priceText1000 != null)
            {
                priceText1000.text = product.metadata.localizedPriceString;
            }
            else if (product.definition.id == nitcoins3000 && priceText3000 != null)
            {
                priceText3000.text = product.metadata.localizedPriceString;
            }
            else if (product.definition.id == nitcoins5000 && priceText5000 != null)
            {
                priceText5000.text = product.metadata.localizedPriceString;
            }
        }
    }

    // Optionally, add a method to initiate the purchase if needed.
    public void BuyProduct(string productId)
    {
        if (storeController != null)
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log($"Purchasing product: '{product.definition.id}'");
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProduct: Product not found or not available.");
            }
        }
        else
        {
            Debug.Log("BuyProduct FAIL. Store not initialized.");
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new NotImplementedException();
    }
}
