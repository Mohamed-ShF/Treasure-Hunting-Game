using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Idle;
    public Transform playerTransform;
    public float moveSpeed = 2.0f;
    public float attackDamage = 10.0f;
    public float attackRange = 1.0f;
    public float playerenemydiff = 1.0f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        animator.SetBool("attack", false);
        animator.SetBool("run", false);
        animator.SetBool("idle",true);
        if (Math.Abs(transform.position.x - playerTransform.position.x) <= playerenemydiff)
        {
            currentState = EnemyState.Chase;
        }
    }

    private void Chase()
    {
        animator.SetBool("attack", false);
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
        if (playerTransform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

       
        Vector2 direction = playerTransform.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange)
        {
            currentState = EnemyState.Attack;
        }
        if (Math.Abs(playerTransform.position.x - transform.position.x) > playerenemydiff)
        {
            currentState = EnemyState.Idle;
        }
    }
    
    private void Attack()
    {
        // Stop moving
        transform.Translate(Vector2.zero);

        //animation
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", true);

        // Attack the player
        if (Time.time >= nextAttackTime)
        {
            //playerTransform.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            nextAttackTime = Time.time + attackRate;
        }

        // Transition back to chase state
        currentState = EnemyState.Chase;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.Attack;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetBool("death", true);
            Destroy(transform, 2);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.Idle;
            Debug.Log("out");
        }
    }
}