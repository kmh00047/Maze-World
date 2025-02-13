using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public enum BallSkins
    {
        Ball1,
        Ball2,
        Ball3,
        Ball4,
        Ball5
    }

    [SerializeField] private List<GameObject> skins = new List<GameObject>();
    private Dictionary<BallSkins, GameObject> skinPrefabs;
    private BallSkins selectedSkin = BallSkins.Ball1; // Default skin

    void Awake()
    {
        if (instance == null)
            instance = this;

        InitializeSkins();
    }

    void InitializeSkins()
    {
        skinPrefabs = new Dictionary<BallSkins, GameObject>
        {
            { BallSkins.Ball1, skins[0] },
            { BallSkins.Ball2, skins[1] },
            { BallSkins.Ball3, skins[2] },
            { BallSkins.Ball4, skins[3] },
            { BallSkins.Ball5, skins[4] }
        };
    }

    public void ApplySkin(GameObject player)
    {
        Transform skinHolder = player.transform.Find("SkinHolder");  // Find the SkinHolder inside Player

        if (skinHolder == null)
        {
            Debug.LogError("SkinHolder not found on Player!");
            return;
        }

        // Remove old skin
        foreach (Transform child in skinHolder)
        {
            Destroy(child.gameObject);
        }

        // Spawn new skin
        GameObject newSkin = Instantiate(skinPrefabs[selectedSkin], skinHolder);
        newSkin.transform.localPosition = Vector3.zero; // Ensure correct positioning
    }

    public void SetSkin(BallSkins newSkin)
    {
        selectedSkin = newSkin;
    }

    public BallSkins GetSelectedSkin()
    {
        return selectedSkin;
    }

    // Debugging 
}
