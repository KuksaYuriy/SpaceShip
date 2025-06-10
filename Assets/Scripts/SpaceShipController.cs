using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [Header ("Movement")]
    public float speedBaseMovement = 10f;
    public float speedRotation = 5f;
    public float speedBoostMultiple = 1.7f;
    public float currentSpeed;
    public float forceMoveUp = 15f;
    public float forceMoveDown = 15f;

    [Header ("Fuel")]
    public float maxFuel = 100f;
    public float fuelBoost = 20f;
    public float currentFuel;
    public float fuelUsingSpeed = 5f;

    [Header("Attack")]
    public bool canAttack = true;
    public float attackDamage = 10f;
    public GameObject laserPrefab;
    public Transform laserPoint;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("No Rigidbody attached to SpaceShip");

        currentSpeed = speedBaseMovement;
        currentFuel = maxFuel;
    }

    void Update()
    {
        ShipMovement();
        MovementBoost();

        MoveUp();
        MoveDown();

        Debug.Log(currentFuel);
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
    }

    void MovementBoost()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFuel > 0)
        {
            currentSpeed *= speedBoostMultiple;
        }
    }

    void MoveUp()
    {
        if (Input.GetKey(KeyCode.Space) && currentFuel > 0)
        {
            Vector3 moveUp = transform.up * forceMoveUp;
            rb.AddForce(moveUp, ForceMode.Acceleration);
            currentFuel -= fuelUsingSpeed * Time.deltaTime;
        }
    }

    void MoveDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentFuel > 0)
        {
            Vector3 moveDown = -transform.up * forceMoveDown;
            rb.AddForce(moveDown, ForceMode.Acceleration);
            currentFuel -= fuelUsingSpeed * Time.deltaTime;
        }
    }

    void LowerSpeed()
    {
        Vector3 velocity = rb.velocity;
        velocity.y *= 0.95f;
        
        rb.velocity = velocity;
    }
}