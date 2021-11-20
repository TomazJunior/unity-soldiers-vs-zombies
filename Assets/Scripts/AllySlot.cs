using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AllySlot : MonoBehaviour
{
    private Ally ally;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

    internal bool HasAlly()
    {
        return this.ally != null;
    }
    internal void SetAlly(AllyStats allyStats)
    {
        this.ally = Instantiate<Ally>(allyStats.allyPrefab, transform.position, Quaternion.identity);
        this.ally.lifeManager.FullLife = allyStats.life;
        this.ally.spriteRenderer.sprite = allyStats.sprite;
        this.ally.Cost = allyStats.cost;
        LevelManager.instance.AddAlly(this.ally);
    }
}
