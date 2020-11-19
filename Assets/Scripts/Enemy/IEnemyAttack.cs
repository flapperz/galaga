using UnityEngine;

interface IEnemyAttack
{
    Vector2 GetAttackPosition();
    float GetAttackDelay();
    void Attack();
}
