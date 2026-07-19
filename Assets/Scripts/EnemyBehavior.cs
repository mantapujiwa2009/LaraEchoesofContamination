using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Stats")]
    public BasicEnemyStat enemyStats;
    int currentHealth;
    int currentDamage;
    public float moveSpeed = 3f; 

    [Header("Detection & Attack")]
    public float chaseRadius = 5f;   
    public float attackRadius = 1f;  
    public float attackCooldown = 1.5f; 
    private float lastAttackTime;

    // References to the Player
    private Transform playerTarget;
    private PlayerManager playerManager;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHealth = enemyStats.enemyHealth;
        currentDamage = enemyStats.enemyDamage;

        // Automatically find the player in the scene
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
            playerManager = playerObject.GetComponent<PlayerManager>();
        }
    }

    void Update()
    {
        if (playerTarget == null) return; 

        FlippedSprite();

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= attackRadius)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= chaseRadius)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack");
            playerManager.TakeDamage(currentDamage);
            lastAttackTime = Time.time; // Reset the cooldown timer
            Debug.Log(gameObject.name + " attacked the player for " + currentDamage);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        animator.SetTrigger("Death");
        Debug.Log("Enemy has died");
        Destroy(gameObject); // Removes the enemy from the scene
    }

    // Draws the visual circles in the Unity Editor so you can balance the ranges!
    void OnDrawGizmosSelected()
    {
        // Draw the Chase Radius in Yellow
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        // Draw the Attack Radius in Red
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void FlippedSprite()
    {
        if (playerTarget != null)
        {
            if (playerTarget.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (playerTarget.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}