using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AllySpawnerButton : MonoBehaviour
{
    [SerializeField] DragDrop allyImage;
    [SerializeField] LayerMask slotLayer;
    [SerializeField] AllyStats allyStats;
    [SerializeField] TextMeshProUGUI coinText;

    private Image image;
    void Awake()
    {
        image = allyImage.GetComponent<Image>();
        image.sprite = allyStats.sprite;
        image.SetNativeSize();
        allyImage.OnEndDragAlly += HandleEndDragAlly;
        coinText.text = allyStats.cost.ToString();
    }

    void Start()
    {
        LevelManager.instance.OnPlayerCoinsChanged += HandlePlayerCoinsChanged;
    }

    private void HandlePlayerCoinsChanged(object sender, int coins)
    {
        var hasEnoughCoins = coins >= allyStats.cost;
        if (hasEnoughCoins)
        {
            allyImage.enabled = true;
            image.color = Color.white;
        }
        else
        {
            allyImage.enabled = false;
            image.color = Color.black;
        }
    }

    private void HandleEndDragAlly(object sender, Vector3 position)
    {
        Collider2D collider2D1 = Physics2D.OverlapBox(position, Vector2.one, 0, slotLayer);
        if (collider2D1)
        {
            AllySlot allySlot = collider2D1.GetComponent<AllySlot>();
            if (!allySlot.HasAlly())
            {
                allySlot.SetAlly(allyStats);
            }
        }
    }
}
