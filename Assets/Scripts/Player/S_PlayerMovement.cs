using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

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
    float bounceForce;
    private bool isFiring;
    private float lastFireTime;
    public float timeBetweenShots = 0.2f;


    public void OnEnable()
    {
        action.Enable();
        fire.Enable();
        rotate.Enable();

        fire.started += StartFiring;
        fire.canceled += StopFiring;
    }

    private void Start()
    {
        playerHealth = GetComponent<S_PlayerHealth>();
        bounceForce = 2.0f;
    }

    public void OnDisable()
    {
        action.Disable();
        fire.Disable();
        rotate.Disable();

        fire.started -= StartFiring;
        fire.canceled -= StopFiring;
    }

    private void Update()
    {

        if (!playerHealth.gameOver)
        {
            moveDirection = action.ReadValue<Vector2>();
            turn = rotate.ReadValue<Vector2>()*new Vector2(2.0f,0f);
            transform.Rotate(Vector3.forward * turn.x);
            if (isFiring)
            {
                
                if (Time.time - lastFireTime >= timeBetweenShots)
                {
                    Fire(); // Fire a shot
                    lastFireTime = Time.time; // Update the last fire time
                }
            }
        }

    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
     
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


    

    private void Fire()
    {

        if (!playerHealth.gameOver)
        {
            Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
            AudioManager.instance.playSound("Player Shoot");
           
        }
        
    }

    private void BounceBack(Vector3 collisionPoint)
    {
        Vector3 bounceDirection = transform.position - collisionPoint;
        rigidbody.AddForce(bounceDirection.normalized * bounceForce, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BounceBack(collision.contacts[0].point);
        }
    }

    private void StartFiring(InputAction.CallbackContext context)
    {
        isFiring = true;
   
    }

    private void StopFiring(InputAction.CallbackContext context)
    {
        isFiring = false;
      
    }
}
