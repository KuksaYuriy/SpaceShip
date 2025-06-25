using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public float cooldownAfterAttack = 0.5f;
    public bool canShoot = true;
   
    public GameObject laser;
    public Transform firePoint;
    public AudioClip audioAttacking;
    public AudioSource audioSource;
    public Camera mainCamera;
    public SpaceShipInteraction spaceShipInteractionScript;
    public BackToMainMenu backToMainMenuScript;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (spaceShipInteractionScript.isPlayerInShip) canShoot = true;
        else canShoot = false;
        canShoot = !backToMainMenuScript.isMenuOpen;
        
        if (Input.GetMouseButtonDown(0) && canShoot) Shoot();
  
    }

    public void Shoot()
    {
        GameObject laserClone = Instantiate(laser, firePoint.position, Quaternion.identity);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            laserClone.transform.LookAt(hitPoint);
            Destroy(laserClone, 5f);
            laserClone.transform.forward = firePoint.forward;
            Debug.Log(hitPoint + "   ShipShooting");
        }

        PlayShootSound();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(audioAttacking);
    }
}
