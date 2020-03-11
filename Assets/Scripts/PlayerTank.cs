using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : MonoBehaviour
{
    TankControls controls;

    Vector2 movementInput;
    Vector2 aimInput;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        controls = new TankControls();

        controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;

        controls.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero; 
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        Vector3 movementVector = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime;
        transform.Translate(movementVector, Space.World);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
