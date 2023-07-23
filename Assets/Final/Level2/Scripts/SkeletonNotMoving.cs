using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skeleton;

public class SkeletonNotMoving : MonoBehaviour
{
    [SerializeField] Transform attackpoint;
    public LayerMask enemyLayers;
    public float swordAttackRange = 0.7f;
    public float moveSpeed = 1.0f;
    public float attackDamage = 10.0f;
    public float attackRange = 3.0f;
    public float playerenemydiff = 7.0f;
    public float attackRate = 2f;

    float swordNextAttackTime = 0f;
    int maxHealth = 50;
    public int currentHealth;
    Animator animator;
    CapsuleCollider2D normalCollider;



    void Start()
    {
        currentHealth = maxHealth;
        normalCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //detectSwordAttack();
        if (currentHealth <= 0)
        {
            this.enabled = false;
        }
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetBool("attack", false);
        animator.SetTrigger("damage");

        if (currentHealth <= 0)
        {
            die();
        }

    }
    //public void takeDamageFromSword(int damage)
    //{
    //    currentHealth -= damage;
    //    if(currentHealth <= 0)
    //    {
    //        currentHealth = maxHealth;
    //    }
       
    //}
   


    void die()
    {
        if (animator.gameObject.activeSelf)
        {
            animator.SetTrigger("die");
            animator.SetBool("attack", false);

        }

        // disable enemy
        Invoke("DisableEnemy", 1.5f);

    }
    //void detectSwordAttack()
    //{
    //    if (Time.time >= swordNextAttackTime)
    //    {
    //        if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, swordAttackRange, enemyLayers);


    //            //// loop on the array to find the enemy
    //            foreach (Collider2D enemy in hitEnemies)
    //            {
    //                Debug.Log("enemy hit");
    //                takeDamage(25);

    //            }
    //            //takeDamage(25);
    //        }
    //    }

    //}

    private void DisableEnemy()
    {
        normalCollider.enabled = false;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("BulletTouchedSkeleton");
            takeDamage(25);
        }
       
    }
}
