using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceShipInteraction : MonoBehaviour
{
    [Header("Game Objects & Scripts")]
    public GameObject player;
    public Camera mainCamera;
    public CameraFollow cameraFollowScript;
    public PlayerMovement playerMovementScript;
    public SpaceShipController spaceShipControllerScript;

    [Header("Postions")]
    public Transform cameraPostionInShip;
    public Transform playerCameraPosition;
    public Transform playerOriginalParent;
    private Vector3 playerOriginalPosition;

    [Header("Values")]
    public float interactionDistance = 3f;
    public bool isPlayerInShip = false;

    void Update()
    {
        if (interactionDistance <= Vector3.Distance(player.transform.position, transform.position) && Input.GetKeyDown(KeyCode.E) && !isPlayerInShip) EnterShip();
        if (Input.GetKeyDown(KeyCode.R) && isPlayerInShip) ExitShip();

        //    if (interactionDistance <= Vector3.Distance(player.transform.position, transform.position) && Input.GetKeyDown(KeyCode.E))
        //    {
        //        if (!isPlayerInShip)
        //        { 
        //            EnterShip();
        //            Debug.Log("EnterShip() Method Started");
        //        }

        //        else if (isPlayerInShip)
        //        {
        //            ExitShip();
        //            Debug.Log("ExitShip() Method Started");
        //        }
        //    }
    }



    public void EnterShip()
    {
        playerOriginalPosition = player.transform.position;
        playerOriginalParent = player.transform.parent;
        player.transform.SetParent(transform);

        player.transform.localPosition = new Vector3(0, 1, 0);

        player.SetActive(false);

        isPlayerInShip = true;
        playerMovementScript = player.GetComponent<PlayerMovement>();

        if (playerMovementScript != null) playerMovementScript.enabled = false;

        cameraFollowScript.enabled = false;
        playerMovementScript.enabled = false;
        spaceShipControllerScript.enabled = true;

        mainCamera.transform.SetParent(cameraPostionInShip);
        mainCamera.transform.localPosition = new Vector3(0, 0.5f, -1);
        mainCamera.transform.localRotation = Quaternion.identity;

        StartCoroutine(SmoothTransition(cameraPostionInShip));
    }

    public void ExitShip()
    {
        player.transform.SetParent(null);
        player.transform.position = transform.position + new Vector3(0, 0, 2);
        player.SetActive(true);
        isPlayerInShip = false;
        playerMovementScript = player.GetComponent<PlayerMovement>();

        if (playerMovementScript != null) playerMovementScript.enabled = true;
        else Debug.LogError("No PlayerMovement Script was found in player");

        cameraFollowScript.enabled = true;
        spaceShipControllerScript.enabled = false;

        mainCamera.transform.SetParent(null);
        mainCamera.transform.localRotation = Quaternion.identity;

        StartCoroutine(SmoothTransition(playerCameraPosition));
    }

    IEnumerator SmoothTransition(Transform targetPosition)
    {
        float timeElapsed = 0f;
        Transform startPosition = mainCamera.transform;
        Quaternion startRotation = mainCamera.transform.rotation;

        while (timeElapsed < 1)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition.position, targetPosition.position, timeElapsed / 1);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetPosition.rotation, timeElapsed / 1);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition.position;
        mainCamera.transform.rotation = targetPosition.rotation;
        mainCamera.transform.SetParent(cameraPostionInShip);
    }
}
