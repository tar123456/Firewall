using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;


public class S_PlayerMovement : MonoBehaviour
{
    #region Variables
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
    public float timeBetweenShots = 0.4f;
    public Transform raycastOrigin;
    #endregion

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
                    Fire(); 
                    lastFireTime = Time.time;
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
            rigidbody.velocity = transform.TransformDirection(velocity);
            RaycastHit hit;
            Vector3 raycastDirection = transform.TransformDirection(Vector3.right);
            float raycastLength = 40.0f; 
            Debug.DrawRay(raycastOrigin.position, raycastDirection * raycastLength, Color.red);
            if (Physics.Raycast(raycastOrigin.position, raycastDirection, out hit, raycastLength) && PlayerPrefs.HasKey("Auto-Shoot") && PlayerPrefs.GetInt("Auto-Shoot") == 1)
            {
                if (hit.collider.gameObject.CompareTag("Enemy")|| hit.collider.gameObject.CompareTag("EnemyProjectile"))
                {
                    if (Time.time - lastFireTime >= timeBetweenShots)
                    {
                        Fire();
                        lastFireTime = Time.time;
                    }
                }
            }
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
