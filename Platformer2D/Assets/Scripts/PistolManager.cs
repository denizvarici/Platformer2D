using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolManager : MonoBehaviour
{
    InputManager inputManager;
    InputAction shootInput;
    
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
        Debug.Log("Ateþ ET");
    }
}
