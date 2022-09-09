using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordManager : MonoBehaviour
{
    InputManager inputManager;
    InputAction attackInput;

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
        Debug.Log("Kýlýçla saldýrý!!");
    }
}
