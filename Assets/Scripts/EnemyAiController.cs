using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiController : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRange = 20f;
    public int damage = 10;
    public bool canAttack = true;
   public float attackDistance = 5f;
    public float cooldownAfterAttackTime = 1.5f;
    public bool isMenuOpen;

    public GameObject player;
    public PlayerHealth playerHealthScript;
    public BackToMainMenuFromGame backToMainMenuFromGameScript;
    
    private Vector3 targetPosition;

    void Start()
    {
        playerHealthScript = player.GetComponent<PlayerHealth>();    
    }

    void Update()
    {
        isMenuOpen = backToMainMenuFromGameScript.isMenuOpen;
        if (isMenuOpen) return;
        
        if (player == null) Debug.LogError("player variable in EnemyAiController is null");
        if (playerHealthScript == null) Debug.LogError("playerHealthScript variable in  EnemyAiController is null");
        
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            AttackPlayer();
        }

        MoveToTarget();
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        LookAtTarget(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) SetRandomTargetPosition();
    }

    public void LookAtTarget(Vector3 targetPos)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
    }

    public void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-20f, 20f);
                float randomZ = Random.Range(-20f, 20f);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    public void AttackPlayer()
    { 
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance && canAttack) 
        { 
            playerHealthScript.TakeDamage(damage);
            StartCoroutine(CooldownAfterAttack());
        }
        else transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    IEnumerator CooldownAfterAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldownAfterAttackTime);
        canAttack = true;
    }
}