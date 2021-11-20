using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            LevelManager.instance.EnemyReachedEndLine(collider.GetComponent<Enemy>());            
        }
    }
}
