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

    [Header ("Fuel")]
    public float maxFuel = 100f;
    public float fuelBoost = 20f;
    public float currentFuel;

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
    }

    void ShipMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Quaternion rotation = Quaternion.Euler(0, horizontalInput * speedRotation * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * rotation);

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime * verticalInput;
        rb.MovePosition(rb.position + movement);
    }

    void MovementBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentFuel > 0)
        {
            currentSpeed *= speedBoostMultiple;
        }
    }
}
