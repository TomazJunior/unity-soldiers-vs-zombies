using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    internal event System.EventHandler<float> OnLifeChanged;
    internal event System.EventHandler<float> OnFullLifeChanged;

    private float fullLife;
    public float FullLife
    {
        get
        {
            return fullLife;
        }
        set
        {
            fullLife = value;
            life = value;
            OnFullLifeChanged?.Invoke(this, fullLife);
        }
    }

    private float life;
    public float Life
    {
        get { return life; }
        set
        {
            life = value;
            OnLifeChanged?.Invoke(this, life);
        }
    }
}
