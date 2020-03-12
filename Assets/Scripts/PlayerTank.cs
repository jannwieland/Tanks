using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : MonoBehaviour
{
    TankControls controls;
    Vector2 movementInput;
    Vector2 aimInput; 
    Transform turret;
    Vector3 turretOffset;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //setup controls
        controls = new TankControls();

        controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;

        controls.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero; 

        //setup turret reference
        turret = transform.GetChild(1);
        //set default start orientation to "forward"
        aimInput = new Vector2(turret.up.x, turret.up.z);
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    void Start()
    {
        turretOffset = transform.localToWorldMatrix.MultiplyPoint(Vector3.zero) - turret.localToWorldMatrix.MultiplyPoint(Vector3.zero);
        turretOffset = Quaternion.AngleAxis(-90, Vector3.right) * turretOffset;
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        Vector3 movementVector = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime;
        transform.Translate(movementVector, Space.World);

        turret.position = transform.position + transform.rotation * turretOffset;

        float aimAngle = getRotationAngle(aimInput);
        turret.rotation = transform.rotation * Quaternion.AngleAxis(aimAngle, Vector3.forward);        
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    float getRotationAngle(Vector2 v)
    {
        float angle = Vector2.Angle(Vector2.right, v);
        if(Vector2.Angle(Vector2.up, v) < 90) angle*=-1;
        return angle;
    }
}
