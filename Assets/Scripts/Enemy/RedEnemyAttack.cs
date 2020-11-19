using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private GameObject bulletPrefabs;
    private bool isStartAttack = false;
    private Vector3 attackPosition;

    [SerializeField]
    private float missileNumber;


    public Vector2 GetAttackPosition()
    {
        Vector2 pos = PlayerController.Instance.Rigidbody.position;
        pos.y = pos.y + 1.5f;
        return pos;
    }

    public float GetAttackDelay()
    {
        return 0.5f;
    }

    public void Attack()
    {
        StartCoroutine("Shoot");
        
        return;
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i<missileNumber; i++)
        {
            Instantiate(bulletPrefabs, gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    
}
