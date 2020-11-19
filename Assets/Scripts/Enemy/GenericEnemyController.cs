using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyController : MonoBehaviour, IHittable
{
    public int Score;

    public void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.gameObject.tag;
        if (targetTag == "Player")
        {
            IHittable target = other.gameObject.GetComponent<IHittable>();
            target.OnHit();
        }
    }

    public void OnHit()
    {
        GameManager.Score += this.Score;
        Destroy(gameObject);
    }
}
