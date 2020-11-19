using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyAttack : MonoBehaviour, IEnemyAttack
{
    private bool isStartAttack = false;
    private Vector3 attackPosition;
    private float speed;

    public Vector2 GetAttackPosition()
    {
        return PlayerController.Instance.Rigidbody.position;
    }

    public float GetAttackDelay()
    {
        return 0.5f;
    }

    public void Attack()
    {
        return;
    }

    
}
