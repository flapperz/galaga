using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private GameObject bulletPrefabs;
    private bool isStartAttack = false;
    private Vector3 attackPosition;


    public Vector2 GetAttackPosition()
    {
        Vector2 pos = PlayerController.Instance.Rigidbody.position;
        pos.y = pos.y + 3.5f;
        return pos;
    }

    public float GetAttackDelay()
    {
        return 0.5f;
    }

    public void Attack()
    {
        Shoot();

        return;
    }

    void Shoot()
    {
        Instantiate(bulletPrefabs, gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);

    }


}
