using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Enemy Entered ");
            //activate chase and shoot

            this.gameObject.GetComponentInParent<EnemyManager>().chaseMode = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Enemy Exit ");
            //activate chase and shoot
            //GetComponentInParent<Transform>().LookAt(collision.transform);
            this.gameObject.GetComponentInParent<EnemyManager>().chaseMode = false;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        Debug.Log("Enemy is in Trigger");
    //    }
    //}
}
