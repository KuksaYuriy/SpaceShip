using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVoidDie : MonoBehaviour
{
    public float maxYToLive = -80f;

    void Update()
    {
        if (transform.position.y <= maxYToLive) SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
    }
}
