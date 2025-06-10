using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float speedLaserThrow = 40f;
    public float laserLifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, laserLifeTime);    
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedLaserThrow * Time.deltaTime);   
    }
}
