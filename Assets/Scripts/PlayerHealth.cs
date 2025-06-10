using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHp = 100;
    public Camera mainCamera;

    public void TakeDamage(int damage)
    {
        playerHp -= damage;
        if (playerHp <= 0) Die();
    }

    public void Die()
    {
        mainCamera.gameObject.transform.SetParent(null);
    }
}
