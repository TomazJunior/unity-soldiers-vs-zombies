using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<EnemyStats> enemyStats = new List<EnemyStats>();
    [SerializeField] Enemy enemyPrefab;
    


    internal void Spawn(float incrementSpeed)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var enemyStats = GetRandomEnemyStats();
        enemy.DistanceToAttack = enemyStats.distanceToAttack;
        enemy.Sprite = enemyStats.sprite;
        enemy.Speed = enemyStats.speed * incrementSpeed;
        enemy.lifeManager.FullLife = enemyStats.life; 
        enemy.CoinsAwards = enemyStats.coinsAwards;
        LevelManager.instance.AddEnemy(enemy);
    }

    private EnemyStats GetRandomEnemyStats()
    {
        var index = Random.Range(0, enemyStats.Count);
        return enemyStats[index];
    }

}
