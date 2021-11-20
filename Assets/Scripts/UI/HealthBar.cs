using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] internal LifeManager lifeManager;

    private float fullLife;
    private float life;

    void Awake()
    {
        lifeManager.OnLifeChanged += HandleLifeChanged;
        lifeManager.OnFullLifeChanged += HandleFullLifeChanged;
    }

    private void HandleFullLifeChanged(object sender, float e)
    {
        fullLife = e;
        life = e;
        hpImage.fillAmount = 1;
    }

    private void HandleLifeChanged(object sender, float e)
    {
        life = e;
        hpImage.fillAmount = life / fullLife;
    }
}
