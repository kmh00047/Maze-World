using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPrefab : MonoBehaviour
{
    [SerializeField] public GameObject[] playerPrefabs = new GameObject[5];
    [SerializeField] public int ballReference;

    private void Start()
    {
        ballReference = (int)Shop.instance.selectedSkin;
    }
}
