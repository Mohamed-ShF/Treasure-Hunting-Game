using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Skeleton : MonoBehaviour
{

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Damage,
        Die
    }

   // public EnemyState currentState = EnemyState.Idle;
    public Transform playerTransform;
    [SerializeField] Transform attackpoint;
    [SerializeField] Transform skeletonAttackPoint;
    [SerializeField] LayerMask PlayerLayer;
    public LayerMask enemyLayers;
    public float swordAttackRange = 0.5f;
    public float moveSpeed = 2.0f;
    public float attackDamage = 10.0f;
    public float attackRange = 3.0f;
    public float playerenemydiff = 7.0f;
    public float attackRate = 2f;
    //float nextAttackTime = 0f;
    int maxHealth = 50;
    int currentHealth;
    CapsuleCollider2D normalCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D skeletonRigidbody;
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
        normalCollider= GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skeletonRigidbody= GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        float diff = Math.Abs(transform.position.x - playerTransform.position.x);
        if (diff <= 7.0f && diff >= 1.5f && currentHealth > 0)
        {
            Chase();
        }
        else if (diff < 1.5f && currentHealth > 0)
        {
          //  Debug.Log("FirstAttackMode");
            Attack();
        }
        else if(diff >7.0f && currentHealth > 0)
        {
         //   Debug.Log("IdleMode");
            Idle();

        }
        else
        {
          //  Debug.Log("ElseAttackMode");
            Attack();
        }
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            this.enabled = false;
        }

       
    }

    private void Idle()
    {
        animator.SetInteger("run", 0);
        animator.SetBool("attack", false);


    }

    private void Chase()
    {
       // Debug.Log("ChaseMode");
        animator.SetInteger("run", 1);
        animator.SetBool("attack", false);


        if (playerTransform.position.x > transform.position.x)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector2(-1, 1);

        }
        else if (playerTransform.position.x < transform.position.x)
        {
           // spriteRenderer.flipX = true;
            transform.localScale=new Vector2(1,1);
        }

        Vector2 direction = playerTransform.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);



       
    }

    private void Attack()
    {
        // Stop moving
        animator.SetBool("attack", true);

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(skeletonAttackPoint.position, swordAttackRange, PlayerLayer);

        //// loop on the array to find the enemy
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    //Debug.Log("Player hit");
            

        //}





    }



    public void takeDamage(int damage)
    {
        //currentState = EnemyState.Damage;
        animator.SetTrigger("damage");


        currentHealth -= damage;

        animator.SetBool("attack", false);
      
        if (currentHealth <= 0)
        {
            die();
        }
      //  animator.SetInteger("run", 0);


    }



    void die()
    {
        //currentState = EnemyState.Die;
        if (animator.gameObject.activeSelf)
        {
            animator.SetTrigger("die");
            animator.SetBool("attack", false);

        }

        // disable enemy
        Invoke("DisableEnemy", 1.5f); 

    }

    private void DisableEnemy()
    {
        normalCollider.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerTouched");


        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("BulletTouchedSkeleton");
            takeDamage(25);
        }
        if (other.gameObject.CompareTag("AttackPoint"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, swordAttackRange, enemyLayers);

                // loop on the array to find the enemy
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("enemy hit");
                    takeDamage(25);

                }
               
            }

                
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerTouched");
            //Attack();
        }
      

    }

}