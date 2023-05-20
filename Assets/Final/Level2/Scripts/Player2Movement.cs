using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Player2Movement : MonoBehaviour
{
    Vector2 respawnPoint;
    Rigidbody2D playerRigidBody;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float playerDirection;
    int attackIndex = 0;
    int score = 0;
    //add
    public float swordAttackRate = 0.5f;
    public float gunAttackRate = 1f;
    float swordNextAttackTime = 0f;
    float gunNextAttackTime = 0f;
    public Transform attackpoint;
    public float swordAttackRange = 0.5f;
    public int swordAttackDamage = 25;
    public LayerMask enemyLayers;
    public float maxHealth = 100;
    public float currentHealth;
    //
    Animator checkPointAnimator;
    [SerializeField] Animator chestAnimator;
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpPower = 0f;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject[] MapPiece;
    [SerializeField] GameObject bulletRight;
    [SerializeField] GameObject bulletLeft;
    [SerializeField] GameObject key;
    [SerializeField] SkeletonNotMoving skeleton;
    //[SerializeField] Rigidbody2D bulletRigidBody;
    //[SerializeField] Animator bulletAnimator;
    [SerializeField] AudioSource whereIsTheMapSound;
    [SerializeField] AudioSource Laugh;
    [SerializeField] AudioSource Jump;
    [SerializeField] AudioSource Death;
    [SerializeField] AudioSource collectables;
    [SerializeField] AudioSource gunShot;
    [SerializeField] AudioSource swordSlash;
    [SerializeField] AudioSource checkPointSound;

    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] ControlHealthBar1 healthBar;
    [SerializeField] Image fill;


    void Start()
    {
        respawnPoint = transform.position;
        playerRigidBody = GetComponent<Rigidbody2D>();
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
       

    }
    void movePlayer()
    {
        playerDirection = Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = new Vector2(speed * playerDirection, playerRigidBody.velocity.y);

    }
    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
           Jump.Play();
            Debug.Log("Jump");
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpPower);
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
            //  spriteRenderer.flipX = false;
            transform.localScale = new Vector2(1, 1);
        }
        else if (playerDirection < 0)
        {
            animator.SetInteger("State", 1);
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector2(-1,1);

        }
        else
        {
            animator.SetInteger("State", 0);
        }

        if (playerRigidBody.velocity.y > 0f && !isGrounded())
        {
            animator.SetInteger("State", 2);
        }
        if (playerRigidBody.velocity.y < 0f && !isGrounded())
        {
            animator.SetInteger("State", 3);
        }

    }
    void PlayerAttack()
    {
        //to make the player doesn't attack consecutive, using time
        if (Time.time >= swordNextAttackTime) {

            if (Input.GetKeyDown(KeyCode.U))
            {
                swordSlash.Play();
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
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, swordAttackRange, enemyLayers);

                // loop on the array to find the enemy
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<SkeletonNotMoving>().takeDamage(25);
                   

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
                gunShot.Play();
                if (transform.localScale.x > 0)
                {
                    playerRigidBody.bodyType = RigidbodyType2D.Static;
                    StartCoroutine(bullletspawnRight());
                    gunNextAttackTime = Time.time + gunAttackRate;


                }
                else if (transform.localScale.x < 0)
                {
                    playerRigidBody.bodyType = RigidbodyType2D.Static;
                    StartCoroutine(bullletspawnLeft());
                    gunNextAttackTime = Time.time + gunAttackRate;

                }
                animator.SetTrigger("GunUse");
            }
        }
            

    }
    void die()
    {
        Death.Play();
        animator.SetTrigger("Death");
        playerRigidBody.bodyType = RigidbodyType2D.Static;
        boxCollider.enabled = false;


    }
    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator DeathManagement()
    {
        die();
        yield return new WaitForSeconds(1f);
        transform.position = respawnPoint;
        playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = true;
        currentHealth = maxHealth;
        healthBarSlider.value = currentHealth;
        fill.color = Color.green;


    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        Debug.Log("player health: " + currentHealth);
        //healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathManagement());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
        //|| collision.gameObject.CompareTag("Spike")
    {
        if (collision.gameObject.CompareTag("Ball[1]") || collision.gameObject.CompareTag("Ball[0]") || collision.gameObject.CompareTag("Pearl") || collision.gameObject.CompareTag("Spike"))
        {
            //
            //StartCoroutine(DeathManagement());
            TakeDamage(20);
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
            checkPointSound.Play();
            respawnPoint = transform.position;
            checkPointAnimator= collision.GetComponent<Animator>();
            Destroy(collision.GetComponent<BoxCollider2D>());
            checkPointAnimator.SetTrigger("Touch");
            Invoke("FlagOut", 1.0f);
           // FlagOut();

        }

        if (collision.gameObject.CompareTag("MapPiece"))
        {
            collision.gameObject.SetActive(false);
            Laugh.Play();
        }



        if (collision.gameObject.CompareTag("Fall"))
        {
            Death.Play();
            TakeDamage(5);
            transform.position = respawnPoint;

        }
        if (collision.gameObject.CompareTag("IntoTheShip"))
        {
            StartCoroutine(loadNextLevel());

        }
        if (collision.gameObject.CompareTag("skeleton"))
        {
            animator.SetTrigger("Hit");
            TakeDamage(10f);
        }
        if (collision.gameObject.CompareTag("Chest") && key.gameObject.activeSelf==false)
        {
           // Invoke("LoadNextLevel", 1f);
            chestAnimator.SetTrigger("Open");
            StartCoroutine(loadNextLevel());
          //  loadNextLevel();

        }
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Touch");
            StartCoroutine(KeyDisappear(collision));

        }


        ScoreText.text = "Treasures: " + score;

    }

    IEnumerator KeyDisappear(Collider2D collision)
    {
        yield return new WaitForSeconds(0.6f);
        collision.gameObject.SetActive(false);
    }


    private void FlagOut()
    {
        checkPointAnimator.SetTrigger("FlagOut");
    }

   
    IEnumerator WhereIsTheMapSound()
    {
        yield return new WaitForSeconds(5f);
       // whereIsTheMapSound.Play();
    }
    IEnumerator bullletspawnRight()
    {
        var pos = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(0.5f);
            Debug.Log("Right");

            var bulletClone = Instantiate(bulletRight, pos, Quaternion.identity);
            playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(1.5f);
            Destroy(bulletClone);
        

    }
    IEnumerator bullletspawnLeft()
    {
        var pos = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(0.5f);
        var bulletClone = Instantiate(bulletLeft, pos, Quaternion.identity);
        playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1.5f);
        Destroy(bulletClone);

    }
    public void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;
        Gizmos.DrawWireSphere(attackpoint.position, swordAttackRange);
    }

}
