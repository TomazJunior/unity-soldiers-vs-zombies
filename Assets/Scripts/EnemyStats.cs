using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy")]
public class EnemyStats : ScriptableObject
{
    public float speed;
    public Sprite sprite;
    public float distanceToAttack;
    public float life = 5;
    public int coinsAwards = 1;
}
