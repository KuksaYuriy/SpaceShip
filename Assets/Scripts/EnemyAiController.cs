using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public bool isMenuOpen;
    private Vector3 targetPosition;
    private Rigidbody rb;
    
    [Header("Attack")]
    public float detectionRange = 20f;
    public int damage = 10;
    public bool canAttack = true;
    public float attackDistance = 5f;
    public float cooldownAfterAttackTime = 1.5f;
   
    [Header("GameObjects & Scripts")]
    public GameObject player;
    public PlayerHealth playerHealthScript;
    public BackToMainMenu backToMainMenuScript;
    
    

    void Start()
    {
        playerHealthScript = player.GetComponent<PlayerHealth>();    
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isMenuOpen = backToMainMenuScript.isMenuOpen;
        if (isMenuOpen)
        {
            rb.isKinematic = true;
            return;
        }

        else rb.isKinematic = false;
        
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