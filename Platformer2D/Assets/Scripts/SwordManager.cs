using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordManager : MonoBehaviour
{
    InputManager inputManager;
    InputAction attackInput;

    //Hit Enemy
    [SerializeField]
    private Transform attackPointTransform;
    [SerializeField]
    private float attackPointRadius;
    [SerializeField]
    private LayerMask attackPointLayer;

    //Sword features
    [SerializeField]
    private int attackDamage;
    private void Awake()
    {
        inputManager = new InputManager();
    }
    private void OnEnable()
    {
        attackInput = inputManager.Player.Attack;
        attackInput.Enable();
        attackInput.performed += Attack;
    }
    private void OnDisable()
    {
        attackInput.Disable();
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void Attack(InputAction.CallbackContext context)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointTransform.position, attackPointRadius, attackPointLayer);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyManager>().TakeDamage(attackDamage);
            //enemy.GetComponent<Rigidbody2D>().AddForce(attackPointTransform.right * 0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackPointRadius);
    }
}
