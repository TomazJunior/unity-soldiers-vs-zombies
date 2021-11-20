using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Survivor : Ally
{
    [SerializeField] float fireRateInSeconds = 1;
    [SerializeField] CharacterAttack characterAttack;
    [SerializeField] float power = 1;
    
    private System.DateTime lastTimeAttack;
    
    protected override void Awake()
    {
        base.Awake();
    }

    void FixedUpdate()
    {
        System.TimeSpan timeSpan = System.DateTime.UtcNow - lastTimeAttack;
        if (timeSpan.TotalSeconds >= fireRateInSeconds)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastTimeAttack = System.DateTime.UtcNow;
        characterAttack.Attack(power, "Enemy");
    }
}
