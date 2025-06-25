using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float cooldownAfterAttack = 0.5f;
    public bool canShoot = true;
   
    public GameObject laser;
    public Transform blasterPosition;
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
        
        
        if (spaceShipInteractionScript.isPlayerInShip) canShoot = false;
        else canShoot = true;
        canShoot = !backToMainMenuScript.isMenuOpen;
        
        if (Input.GetMouseButtonDown(0) && canShoot) Shoot();
  
    }

    public void Shoot()
    {
        GameObject laserClone = Instantiate(laser, blasterPosition.position, Quaternion.identity);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            laserClone.transform.LookAt(hitPoint);
            Debug.Log(hitPoint + "   PlayerShooting");
            Debug.Log(Input.mousePosition + "    MousePos PlayerShooting");
            Destroy(laserClone, 5f);
            laserClone.transform.forward = blasterPosition.forward;
        }

        PlayShootSound();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(audioAttacking);
    }
}