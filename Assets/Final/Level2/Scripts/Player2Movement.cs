using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player2Movement : MonoBehaviour
{
    Vector2 respawnPoint;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float playerDirection;
    int attackIndex = 0;
    int score = 0;
    public float swordAttackRate = 1f;
    public float gunAttackRate = 1f;
    float swordNextAttackTime = 0f;
    float gunNextAttackTime = 0f;
    public Transform attackpoint;
    public float swordAttackRange = 0.5f;
    public int swordAttackDamage = 25;
    public LayerMask enemyLayers;
    public float maxHealth = 100;
    float currentHealth;

    [SerializeField] float speed = 0f;
    [SerializeField] float jumpPower = 0f;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject[] MapPiece;
    [SerializeField] GameObject bulletRight;
    [SerializeField] GameObject bulletLeft;
    //[SerializeField] Rigidbody2D bulletRigidBody;
    //[SerializeField] Animator bulletAnimator;
    [SerializeField] AudioSource whereIsTheMapSound;
    [SerializeField] AudioSource Laugh;
    [SerializeField] AudioSource Jump;
    [SerializeField] AudioSource Death;
    [SerializeField] AudioSource collectables;
    [SerializeField] TextMeshProUGUI ScoreText;



    void Start()
    {
        playerDirection = 1;
        respawnPoint = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WhereIsTheMapSound());
        currentHealth = maxHealth;
    }


    void Update()
    {
       
        movePlayer();
        playerJump();
        Animation();
        PlayerAttack();
        playerAnimationAttackWithGun();
        //Debug.Log(playerDirection);

        // Debug.Log(playerDirection);


    }
    void movePlayer()
    {
        playerDirection = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(speed * playerDirection, rigidbody.velocity.y);

    }
    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
           // Jump.Play();
            //Debug.Log("Jump");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
        }
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, GroundLayer);
    }
    void Animation()
    {
        if (playerDirection > 0)
        {
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = false;
        }
        else if (playerDirection < 0)
        {
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetInteger("State", 0);
        }

        if (rigidbody.velocity.y > 0f && !isGrounded())
        {
            animator.SetInteger("State", 2);
        }
        if (rigidbody.velocity.y < 0f && !isGrounded())
        {
            animator.SetInteger("State", 3);
        }

    }
    void PlayerAttack()
    {
        if (Time.time >= swordNextAttackTime)    //to make the player doesn't attack consecutive, using time
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (attackIndex == 0)
                {
                    animator.SetTrigger("Attack1");
                    attackIndex++;
                    attackIndex %= 3;
                    swordNextAttackTime = Time.time + swordAttackRate;

                }
                else if (attackIndex == 1)
                {
                    animator.SetTrigger("Attack2");
                    attackIndex++;
                    attackIndex %= 3;
                    swordNextAttackTime = Time.time + swordAttackRate;

                }
                else
                {
                    animator.SetTrigger("Attack3");
                    attackIndex++;
                    attackIndex %= 3;
                    swordNextAttackTime = Time.time + swordAttackRate;

                }

                // animator.SetInteger("AttackIndex", attackIndex);

                Debug.Log(attackIndex);

                // put every thing circle touch in array 
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position,swordAttackRange,enemyLayers);

                // loop on the array to find the enemy
                foreach(Collider2D enemy in hitEnemies)
                {
                    Debug.Log("enemy hit");
                    enemy.GetComponent<Skeleton>().takeDamage(swordAttackDamage);
                }
            }
        }
    }
    void playerAnimationAttackWithGun() 
    {
        if (Time.time >= gunNextAttackTime)    //to make the player doesn't attack consecutive, using time
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (spriteRenderer.flipX == false)
                {
                    rigidbody.bodyType = RigidbodyType2D.Static;
                    StartCoroutine(bullletspawnRight());
                    gunNextAttackTime = Time.time + gunAttackRate;
                }
                else if (spriteRenderer.flipX == true)
                {
                    rigidbody.bodyType = RigidbodyType2D.Static;
                    StartCoroutine(bullletspawnLeft());
                    gunNextAttackTime = Time.time + gunAttackRate;
                }
                animator.SetTrigger("GunUse");
            }
        }
    }
    public void die()
    {
        Death.Play();
        animator.SetTrigger("Death");
        rigidbody.bodyType = RigidbodyType2D.Static;
        boxCollider.enabled = false;


    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("player health: " + currentHealth);
        //healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            die();
        }
    }
    IEnumerator DeathManagement()
    {
        die();
        yield return new WaitForSeconds(1f);
        transform.position = respawnPoint;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = true;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball[1]") || collision.gameObject.CompareTag("Ball[0]") || collision.gameObject.CompareTag("CannonBall") || collision.gameObject.CompareTag("Pearl") || collision.gameObject.CompareTag("Spike"))
        {
            StartCoroutine(DeathManagement());
        }
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectables.Play();
            Destroy(collision.gameObject);
            score++;
        }
        if (collision.gameObject.CompareTag("Red"))
        {
            collectables.Play();
            Destroy(collision.gameObject);
            score += 2;
        }
        if (collision.gameObject.CompareTag("Blue"))
        {
            collectables.Play();
            Destroy(collision.gameObject);
            score += 5;
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            respawnPoint = transform.position;
            Destroy(collision.GetComponent<BoxCollider2D>());
        }

        if (collision.gameObject.CompareTag("MapPiece"))
        {
            collision.gameObject.SetActive(false);
            Laugh.Play();
        }

        if (collision.gameObject.CompareTag("Fall"))
        {
            Death.Play();
            transform.position = respawnPoint;

        }
        if (collision.gameObject.CompareTag("IntoTheShip"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // collision with skeleton
        if(collision.gameObject.layer == 6)
        {
            TakeDamage(10);
        }

        //ScoreText.text = "Treasures: " + score;

    }
    IEnumerator WhereIsTheMapSound()
    {
        yield return new WaitForSeconds(5f);
        whereIsTheMapSound.Play();
    }
    IEnumerator bullletspawnRight()
    {
        var pos = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(0.5f);
            Debug.Log("Right");

            var bulletClone = Instantiate(bulletRight, pos, Quaternion.identity);
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(1.5f);
            Destroy(bulletClone);
        //else if(playerDirection == -1) {
        //    Debug.Log("left");

        //    var bulletClone = Instantiate(bulletLeft, pos, Quaternion.identity);
        //    yield return new WaitForSeconds(1.5f);
        //    Destroy(bulletClone);
        //}

        //var bulletClone = Instantiate(bulletRight, pos, Quaternion.identity);
        //rigidbody.bodyType = RigidbodyType2D.Dynamic;
        //    yield return new WaitForSeconds(1.5f);
        //    Destroy(bulletClone);

    }
    IEnumerator bullletspawnLeft()
    {
        var pos = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(0.5f);
        var bulletClone = Instantiate(bulletLeft, pos, Quaternion.identity);
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1.5f);
        Destroy(bulletClone);

    }
    public float getPlayerDirection()
    {
        return playerDirection;
    }
    public void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;
        Gizmos.DrawWireSphere(attackpoint.position,swordAttackRange);
    }

}
