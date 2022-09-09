using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    //New Input System
    InputManager inputManager;
    InputAction moveInput;
    InputAction jumpInput;
    

    //Player Components
    private Rigidbody2D playerRigidbody;
    

    //Player features
    [SerializeField]
    private float playerSpeed = 5f;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private bool doubleJump = false;
    [SerializeField]
    private bool lookingRight = true;

    //GroundChecking
    [SerializeField]
    private Transform groundCheckerTransform;
    [SerializeField]
    private float groundCheckerRadius;
    [SerializeField]
    private LayerMask groundCheckerLayer;

    //Gun System
    [SerializeField]
    private GameObject[] playerWeapons;
    private int currentWeapon;
    [SerializeField]
    private Transform bulletWay;

    


    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        moveInput = inputManager.Player.Move;
        moveInput.Enable();

        jumpInput = inputManager.Player.Jump;
        jumpInput.Enable();
        jumpInput.performed += Jump;

        
        
    }
    private void OnDisable()
    {
        moveInput.Disable();
        jumpInput.Disable();
        
    }


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        
        
    }

    
    void Update()
    {
        OnGround();
        SelectWeapon();
        TurnPlayer();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        Vector2 direction = moveInput.ReadValue<Vector2>();
        Vector2 movement = new Vector2(direction.x * playerSpeed * Time.fixedDeltaTime,
            playerRigidbody.velocity.y);
        playerRigidbody.velocity = movement;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            doubleJump = true;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,0f);
            playerRigidbody.AddForce(Vector2.up * jumpForce);
            
        }
        else
        {
            if (doubleJump)
            {
                doubleJump = false;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(Vector2.up * jumpForce);
            }
        }
    }
    

    void OnGround()
    {
        isGrounded =  Physics2D.OverlapCircle(groundCheckerTransform.position, groundCheckerRadius, groundCheckerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckerTransform.position, groundCheckerRadius);
    }

    void SwitchWeapon(int weaponIndex)
    {
        currentWeapon = weaponIndex;
        for (int i = 0; i < playerWeapons.Length; i++)
        {
            if (i == currentWeapon)
            {
                playerWeapons[i].SetActive(true);
            }
            else
            {
                playerWeapons[i].SetActive(false);
            }
        }
    }
    
    void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);

        }
        
    }

    void TurnPlayer()
    {
        //Vector3 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //Vector2 playerPosition = transform.position;

        //mousePosition.x -= playerPosition.x;
        //mousePosition.y -= playerPosition.y;

        //float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x && !lookingRight)
        {
            FlipPlayer();
            FlipBulletWay(0);
        }
        if(mousePosition.x < transform.position.x && lookingRight)
        {    
            FlipPlayer();
            FlipBulletWay(-180f);
        }
        void FlipPlayer()
        {
            lookingRight = !lookingRight;
            var tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }
        void FlipBulletWay(float rotation)
        {
            var tempRotation = bulletWay.localRotation;
            tempRotation = Quaternion.Euler(0, 0, rotation);
            bulletWay.localRotation = tempRotation;
        }
    }

    

    
    
}
