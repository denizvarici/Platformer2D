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
    [SerializeField]
    private bool patrolMode = true;
    [SerializeField]
    private bool shootMode = false;

    //Enemy Components
    private Rigidbody2D enemyRigidbody;

    

    void Start()
    {
        currentHealth = maxHealth;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("PatrolWay", 0f, 3f);
    }


    void FixedUpdate()
    {

        Patrol();

    }

    public void TakeDamage(int damage)
    {
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

    void PatrolWay()
    {
        lookingRight = !lookingRight;
        var temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PatrolWay();
        }
    }
}
