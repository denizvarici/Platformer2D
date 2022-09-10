using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage;
    
    public Transform bulletWayTransform;
    

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(bulletDamage,"pistol");
            

            Destroy(this.gameObject);
        }
    }
}
