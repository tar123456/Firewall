using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float movespeed;
    public InputAction action;
    public InputAction fire;
    public InputAction rotate;
    public GameObject projectile;
    
    public Transform spawnLocation;
    S_PlayerHealth playerHealth;

    Vector2 moveDirection = Vector2.zero;
    Vector2 turn = Vector2.zero;


    public void OnEnable()
    {
        action.Enable();
        fire.Enable();
        rotate.Enable();

       
        fire.performed+= Fire;
    }

    private void Start()
    {
        playerHealth = GetComponent<S_PlayerHealth>();
    }

    public void OnDisable()
    {
        action.Disable();
        fire.Disable();
        rotate.Disable();
    }

    private void Update()
    {

        if (!playerHealth.gameOver)
        {
            moveDirection = action.ReadValue<Vector2>();
            turn = Mouse.current.delta.ReadValue();
            transform.Rotate(Vector3.forward * turn.x);
        }

    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>(); 
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!playerHealth.gameOver)
        {

            Vector3 velocity = new Vector3(moveDirection.y, moveDirection.x, 0) * movespeed;

            // Apply the velocity to the Rigidbody in local space
            rigidbody.velocity = transform.TransformDirection(velocity);
        }
    
    }

    private void Fire(InputAction.CallbackContext context)
    {

        if (!playerHealth.gameOver)
        {
            Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
            AudioManager.instance.playSound("Player Shoot");
           
        }
        
    }
}
