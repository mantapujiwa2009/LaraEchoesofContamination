using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private Vector2 lastDirection = new Vector2(0, 0); 
    Animator animator;
    SpriteRenderer spriteRenderer;
    EnemyBehavior enemyBehavior;

    public float attackRange;
    public float attackRadius;
    public LayerMask enemyLayer;
    PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerManager = GetComponent<PlayerManager>();
    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();    
    }

    void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            PerformAttack();
        }
    }

    void PerformAttack(){
        animator.SetTrigger("Attack");

        Vector2 attackPosition = (Vector2)transform.position + (lastDirection.normalized * attackRange);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemyBehavior = enemy.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.EnemyTakeDamage(playerManager.currentPlayerDamage);
            }
            Debug.Log("Hit " + enemy.name + " for " + playerManager.currentPlayerDamage + " damage.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(direction.x, direction.y, 0);
        transform.position += movement * speed * Time.deltaTime;

        if (direction != Vector2.zero)
        {
            lastDirection = direction;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Send the LAST known direction to the Animator, never the raw 0,0 direction
        animator.SetFloat("X", lastDirection.x);
        animator.SetFloat("Y", lastDirection.y);

        FlippedSprite();
    }

    void FlippedSprite()
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + (lastDirection.normalized * attackRange);
        Gizmos.DrawWireSphere(attackPosition, attackRadius);
    }
}