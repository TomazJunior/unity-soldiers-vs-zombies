using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] internal GameObject bufferPrefab;
    [SerializeField] internal Projectil projectilPrefab;
    [SerializeField] internal Transform firePoint;
    internal void Attack(float projectilForce, string tag)
    {
        GameObject bufferObject = Instantiate(bufferPrefab, firePoint.position, this.firePoint.rotation, firePoint.transform);
        Destroy(bufferObject, .2f);
        
        Projectil projectil = Instantiate(projectilPrefab, firePoint.position, this.firePoint.rotation);
        projectil.collisionTag = tag;

        Rigidbody2D rb = projectil.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectilForce, ForceMode2D.Impulse);
        
        Destroy(projectil.gameObject, 5f);
    }
}
