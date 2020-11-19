using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHittable
{
    // singleton
    private static PlayerController _instance;

    public static PlayerController Instance {  get { return _instance; } }

    // movement
    private Vector3 movementVelocity = new Vector3();
    [SerializeField]
    private float speedX;

    // shooting
    [SerializeField]
    private GameObject bulletPrefab;

    private bool isShootable = true;
    private float shootDelay = 0.3f;

    // live
    [SerializeField]
    private int MAX_LIVE;

    public int Live { get; private set; }
    private float immortalDelay = 1f;

    // component reference
    public Rigidbody2D Rigidbody;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Live = MAX_LIVE;
    }


    void Update()
    {
        // horizontal movement
        movementVelocity = ( Input.GetAxisRaw("Horizontal") * speedX ) * Vector3.right;

        // shoot
        if (Input.GetButton("Jump"))
        {
            ShootBtnCallback();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = movementVelocity;
    }

    void ShootBtnCallback()
    {
        if (isShootable)
        {
            isShootable = false;
            Instantiate(bulletPrefab, Rigidbody.position, Quaternion.identity);
            Invoke("ResetShootable", shootDelay);
        }
      
    }

    void ResetShootable()
    {
        isShootable = true;
    }

    public void OnHit()
    {
        if (Live > 1)
        {
            Live -= 1;
            GetComponent<Collider2D>().enabled = false;
            Invoke("ResetImmortal", immortalDelay);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            GameManager.GameOver();
        }
    }

    void ResetImmortal()
    {
        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

}