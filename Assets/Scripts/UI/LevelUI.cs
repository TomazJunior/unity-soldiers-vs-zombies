using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI numberOfRemainingEnemiesToReachEndLevelText;
    [SerializeField] TextMeshProUGUI numberOfEnemiesKilledText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Awake()
    {
        LevelManager.instance.OnRoundChanged += HandleRoundChanged;
        LevelManager.instance.OnRemainingEnemiesToCrossEndLevelChanged += HandleRemainingEnemiesToCrossEndLevelChanged;
        LevelManager.instance.OnPlayerCoinsChanged += HandlePlayerCoinsChanged;
        LevelManager.instance.OnEnemyKilledChanged += HandleEnemyKilledChanged;

    }

    private void HandleEnemyKilledChanged(object sender, int e)
    {
        numberOfEnemiesKilledText.text = e.ToString();
    }

    private void HandlePlayerCoinsChanged(object sender, int e)
    {
        coinsText.text = e.ToString();
    }

    private void HandleRoundChanged(object sender, int round)
    {
        roundText.text = $"Round: {round}";
    }

    private void HandleRemainingEnemiesToCrossEndLevelChanged(object sender, int e)
    {
        numberOfRemainingEnemiesToReachEndLevelText.text = e.ToString();
    }
}
