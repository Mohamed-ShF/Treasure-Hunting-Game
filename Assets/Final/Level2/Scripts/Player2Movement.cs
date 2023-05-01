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

    }


    void Update()
    {
       
        movePlayer();
        playerJump();
        Animation();
        PlayerAttack();
        playerAnimationAttackWithGun();
        Debug.Log(playerDirection);

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
            Debug.Log("Jump");
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (attackIndex == 0)
            {
                animator.SetTrigger("Attack1");
                attackIndex++;
                attackIndex %= 3;

            }
            else if(attackIndex == 1) {
                animator.SetTrigger("Attack2");
                attackIndex++;
                attackIndex %= 3;

            }
            else
            {
                animator.SetTrigger("Attack3");
                attackIndex++;
                attackIndex %= 3;

            }

            

            // animator.SetInteger("AttackIndex", attackIndex);

            Debug.Log(attackIndex);
        }
        
    }
    void playerAnimationAttackWithGun() 
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (spriteRenderer.flipX==false)
            {
                rigidbody.bodyType = RigidbodyType2D.Static;
                StartCoroutine(bullletspawnRight());

            }
            else if(spriteRenderer.flipX==true) {
                rigidbody.bodyType = RigidbodyType2D.Static;
                StartCoroutine(bullletspawnLeft());
            }
            animator.SetTrigger("GunUse");
        }

    }
    void die()
    {
        Death.Play();
        animator.SetTrigger("Death");
        rigidbody.bodyType = RigidbodyType2D.Static;
        boxCollider.enabled = false;


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

        ScoreText.text = "Treasures: " + score;

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
  
}
