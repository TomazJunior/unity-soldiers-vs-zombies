using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Ally : MonoBehaviour, ITakeDamage
{
    [SerializeField] internal LifeManager lifeManager;
    internal int Cost { get; set; }

    internal SpriteRenderer spriteRenderer;
    private float damageLoopDuration = .1f;
    private TweenerCore<Color, Color, ColorOptions> takeDamageTweener;

    protected virtual void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        lifeManager.OnLifeChanged += HandleLifeChanged;
    }

    private void HandleLifeChanged(object sender, float e)
    {
        if (e == 0)
        {
            LevelManager.instance.RemoveAlly(this);
        }
    }

    public void TakeDamage()
    {
        this.lifeManager.Life--;
        var color = spriteRenderer.color;
        KillDamageTweener();
        takeDamageTweener = spriteRenderer.DOColor(Color.red, damageLoopDuration)
                .SetLoops(2, LoopType.Yoyo);
    }

    void OnDestroy()
    {
        KillDamageTweener();
    }

    private void KillDamageTweener()
    {
        if (takeDamageTweener != null && takeDamageTweener.active && takeDamageTweener.IsPlaying()) takeDamageTweener.Kill();
    }
}
