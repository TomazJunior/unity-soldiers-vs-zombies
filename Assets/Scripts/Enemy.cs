using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public class Enemy : MonoBehaviour, ITakeDamage
{
    const int SPEED_FACTOR = 10;
    [SerializeField] CharacterAttack characterAttack;
    [SerializeField] float power = 1;
    [SerializeField] float fireRateInSeconds = 1;
    [SerializeField] LayerMask allyLayer;
    [SerializeField] internal LifeManager lifeManager;

    public float Speed { get; set; }

    private Sprite sprite;
    private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;
    private float damageLoopDuration = .1f;
    private System.DateTime lastTimeAttack;
    private TweenerCore<Color, Color, ColorOptions> takeDamageTweener;

    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            GetComponent<SpriteRenderer>().sprite = value;
            sprite = value;
        }
    }


    public float DistanceToAttack { get; set; }
    public int CoinsAwards { get; set; }

    void Awake()
    {
        this.rig = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        lifeManager.OnLifeChanged += HandleLifeChanged;
    }

    private void HandleLifeChanged(object sender, float e)
    {
        if (e == 0)
        {
            LevelManager.instance.RemoveEnemy(this);
        }
    }

    void FixedUpdate()
    {
        var direction = transform.position + new Vector3(-DistanceToAttack, 0);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector2(-1, 0), DistanceToAttack, allyLayer);
        if (raycastHit2D.collider != null)
        {
            this.rig.velocity = Vector2.zero;
            System.TimeSpan timeSpan = System.DateTime.UtcNow - lastTimeAttack;
            if (timeSpan.TotalSeconds >= fireRateInSeconds)
            {
                Attack();
            }
        }
        else
        {
            this.rig.velocity = new Vector2(-1, 0) * (Speed * SPEED_FACTOR) * Time.fixedDeltaTime;
        }
    }

    void Attack()
    {
        lastTimeAttack = System.DateTime.UtcNow;
        characterAttack.Attack(power, "Ally");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-DistanceToAttack, 0));
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
