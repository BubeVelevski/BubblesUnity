using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gm;
    public TMP_Text textPoints;
    public TMP_Text textCoins;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        textPoints.text = gm.points.ToString();
        textCoins.text = gm.coinsCollected.ToString();
    }
}
