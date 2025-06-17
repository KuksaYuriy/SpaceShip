using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHp = 100;
    public int playerHp = 100;
    public Camera mainCamera;
    public Text hpText;

    void Start()
    {
        hpText.text = "HP: " + playerHp.ToString() + " | " + maxPlayerHp.ToString();       
    }

    public void TakeDamage(int damage)
    {
        playerHp -= damage;
        hpText.text = "HP: " + playerHp.ToString() + " | " + maxPlayerHp.ToString();
        if (playerHp <= 0) Die();
        
    }

    public void Die()
    {
        //SceneManager.LoadScene();
    }
}
