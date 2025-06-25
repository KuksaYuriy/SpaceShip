using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    [Header ("Movement")]
    public float speedBaseMovement = 10f;
    public float speedRotation = 5f;
    public float speedBoostMultiple = 1.7f;
    public float currentSpeed;
    public float forceMoveUp = 15f;
    public float forceMoveDown = 15f;
    public bool isMenuOpen;

    [Header ("Fuel")]
    public float maxFuel = 500f;
    public float currentFuel;
    public float fuelUsingSpeed = 1f;
    public float fuelUsingWithMovementBoost = 10f;
    public float fuelUsingWithVerticalMoving = 10f;

    [Header("Attack")]
    public bool canAttack = true;
    public float attackDamage = 10f;
    public GameObject laserPrefab;
    public Transform laserPoint;

    [Header("UI")]
    public Slider fuelSlider;
    
    [Header("GameObjects & Scripts")]
    public BackToMainMenu backToMainMenuScript;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("No Rigidbody was found in SpaceShip");

        currentSpeed = speedBaseMovement;
        currentFuel = maxFuel;

        fuelSlider.maxValue = maxFuel;
        fuelSlider.minValue = 0;
        fuelSlider.value = currentFuel;
    }
    
    void Update()
    {
        isMenuOpen = backToMainMenuScript.isMenuOpen;
        if (isMenuOpen)
        {
            rb.isKinematic = true;
            return;
        }
        
        else rb.isKinematic = false;
        
        ShipMovement();
        MovementBoost();
        MoveUp();
        MoveDown();

        fuelSlider.value = currentFuel;
    }

    void ShipMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Quaternion rotation = Quaternion.Euler(0, horizontalInput * speedRotation * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * rotation);

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime * verticalInput;
        rb.MovePosition(rb.position + movement);

        LowerSpeed();
        
        currentFuel -= fuelUsingSpeed * Time.deltaTime;
    }

    void MovementBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && currentFuel > 0 && currentSpeed < currentSpeed * speedBoostMultiple)
        {
            currentSpeed *= speedBoostMultiple;
            currentFuel -= fuelUsingSpeed * Time.deltaTime;
        }
    }

    void MoveUp()
    {
        if (Input.GetKey(KeyCode.Space) && currentFuel > 0)
        {
            Vector3 moveUp = transform.up * forceMoveUp;
            rb.AddForce(moveUp, ForceMode.Acceleration);
            currentFuel -= fuelUsingWithVerticalMoving * Time.deltaTime;
        }
    }

    void MoveDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentFuel > 0)
        {
            Vector3 moveDown = -transform.up * forceMoveDown;
            rb.AddForce(moveDown, ForceMode.Acceleration);
            currentFuel -= fuelUsingWithVerticalMoving * Time.deltaTime;
        }
    }

    void LowerSpeed()
    {
        Vector3 velocity = rb.velocity;
        velocity.y *= 0.95f;
        
        rb.velocity = velocity;
    }
}