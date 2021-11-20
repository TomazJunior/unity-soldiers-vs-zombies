using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI numberOfEnemiesText;
    [SerializeField] TextMeshProUGUI numberOfEnemiesReachedEndLevelText;
    [SerializeField] TextMeshProUGUI coinsText;
    
    void Awake()
    {
        LevelManager.instance.OnRoundChanged += HandleRoundChanged;
        LevelManager.instance.OnTotalOfEnemiesChanged += HandleTotalOfEnemiesChanged;
        LevelManager.instance.OnTotalOfEnemiesReachedEndLevelChanged += HandleTotalOfEnemiesReachedEndLevelChanged;
        LevelManager.instance.OnPlayerCoinsChanged += HandlePlayerCoinsChanged;
    }

    private void HandlePlayerCoinsChanged(object sender, int e)
    {
        UpdateCoinsText(e);
    }


    private void HandleTotalOfEnemiesChanged(object sender, int e)
    {
        UpdateNumberOfEnemiesText(e);
    }

    private void HandleRoundChanged(object sender, int e)
    {
        UpdateRoundText(e);
    }

    private void HandleTotalOfEnemiesReachedEndLevelChanged(object sender, int e)
    {
        UpdateTotalOfEnemiesReachedEndLevelText(e);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateRoundText(int round)
    {
        roundText.text = $"Round: {round}";
    }

    void UpdateNumberOfEnemiesText(int enemies)
    {
        numberOfEnemiesText.text = $"Remaining Enemies: {enemies}";
    }

    void UpdateTotalOfEnemiesReachedEndLevelText(int enemies)
    {
        numberOfEnemiesReachedEndLevelText.text = $"Reached the end level: {enemies}";
    }

    private void UpdateCoinsText(int e)
    {
         coinsText.text = $"Coins: {e}";
    }
}
