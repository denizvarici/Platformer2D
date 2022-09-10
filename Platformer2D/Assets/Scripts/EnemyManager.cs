using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Enemy features
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private bool lookingRight;
    [SerializeField]
    private float enemySpeed;

    //Patrol around
    [SerializeField]
    private bool patrolMode = false;
    [SerializeField]
    private float patrolTimer;
    [SerializeField]
    private float patrolBaseTimer;

    //Shoot to player
    [SerializeField]
    private bool shootMode = false;

    //Chasing  Player
    [SerializeField]
    public bool chaseMode = false;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float chaseSpeed;

    //Getting hit
    [SerializeField]
    private Transform attackPointTransform;
    [SerializeField]
    private Transform bulletWayTransform;
    [SerializeField]
    private float recoilAfterHit;

    //Enemy Components
    private Rigidbody2D enemyRigidbody;
    

    

    void Start()
    {
        currentHealth = maxHealth;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        //InvokeRepeating("PatrolWay", 0f, 3f);
        playerTransform = GameObject.FindWithTag("Player").transform;
        patrolTimer = patrolBaseTimer;
        playerTransform = GameObject.Find("Player").transform;
        
    }

    void Update()
    {
        
        
    }


    void FixedUpdate()
    {
        ChangePatrolWay();
        Patrol();
        Chase();

    }

    public void TakeDamage(int damage,string weaponName)
    {
        enemyRigidbody.AddForce(playerTransform.right * recoilAfterHit);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void Patrol()
    {
        if (patrolMode)
        {
            if (lookingRight)
            {
                enemyRigidbody.velocity = new Vector2(1 * enemySpeed * Time.fixedDeltaTime, enemyRigidbody.velocity.y);
            }
            else
            {
                enemyRigidbody.velocity = new Vector2(-1 * enemySpeed * Time.fixedDeltaTime, enemyRigidbody.velocity.y);
            }
        }

    }

    void ChangePatrolWay()
    {
        if (patrolMode)
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
            {
                ChangeFacing();
                patrolTimer = patrolBaseTimer;
            }
        }
    }

    void ChangeFacing()
    {
        lookingRight = !lookingRight;
        var temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
    
    void Chase()
    {
        if (chaseMode)
        {           
            if (playerTransform.position.x < transform.position.x && lookingRight)
            {
                ChangeFacing();
            }
            if (playerTransform.position.x > transform.position.x && !lookingRight)
            {
                ChangeFacing();
            }
            transform.position = Vector2.MoveTowards(transform.position,playerTransform.position,chaseSpeed * Time.fixedDeltaTime);
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ChangeFacing();
        }

        
    }
}
