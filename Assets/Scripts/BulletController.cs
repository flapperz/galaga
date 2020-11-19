using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speedY;
    
    public string targetTag = "";

    void Awake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.up * speedY;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject targetGameObject = other.gameObject;
        if ( targetGameObject.tag == targetTag )
        {
            IHittable targetHittable = targetGameObject.GetComponent<IHittable>();
            targetHittable.OnHit();
            Destroy(gameObject);
        }
    }
}
