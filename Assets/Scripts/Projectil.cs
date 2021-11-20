using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    internal string collisionTag;

    void Awake() {
        LevelManager.instance.OnGameOver += HandleGameOver;
    }

    private void HandleGameOver(object sender, EventArgs e)
    {
        if (this == null || gameObject == null) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(collisionTag))
        {
            collider.GetComponent<ITakeDamage>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
