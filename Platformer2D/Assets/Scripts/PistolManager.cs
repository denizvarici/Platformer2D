using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolManager : MonoBehaviour
{
    InputManager inputManager;
    InputAction shootInput;

    //DestroyMySelf
    [SerializeField]
    private float destroyBulletTime;
    //shooting
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletWay;
    [SerializeField]
    private float bulletSpeed;
    
    void Awake()
    {
        inputManager = new InputManager();
    }
    private void OnEnable()
    {
        shootInput = inputManager.Player.Shoot;
        shootInput.Enable();
        shootInput.performed += Shoot;
    }
    void OnDisable()
    {
        shootInput.Disable();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void Shoot(InputAction.CallbackContext context)
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, bulletWay.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWay.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bulletObject, destroyBulletTime);
    }

    
}
