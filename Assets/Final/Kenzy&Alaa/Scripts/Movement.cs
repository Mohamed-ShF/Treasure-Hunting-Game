using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    Vector2 respawnPoint;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float direction;
    int score = 0;
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpPower = 0f;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject[] MapPiece;
    [SerializeField] AudioSource whereIsTheMapSound;
    [SerializeField] AudioSource Laugh;
    [SerializeField] AudioSource Jump;
    [SerializeField] AudioSource Death;
    [SerializeField] AudioSource collectables;
    [SerializeField] TextMeshProUGUI ScoreText;



    void Start()
    {
        respawnPoint = transform.position;
        rigidbody= GetComponent<Rigidbody2D>();
        boxCollider= GetComponent<BoxCollider2D>();
        animator= GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        StartCoroutine(WhereIsTheMapSound());

    }

    
    void Update()
    {
        movePlayer();
        playerJump();
        Animation();
        PlayerAttack();
        
    }
    void movePlayer()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity= new Vector2(speed*direction, rigidbody.velocity.y);

    }
    void playerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump.Play();
            rigidbody.velocity= new Vector2(rigidbody.velocity.x,jumpPower);
        }
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, GroundLayer);
    }
    void Animation() {
        if (direction > 0)
        {
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            animator.SetInteger("State", 1);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetInteger("State", 0);
        }

        if (rigidbody.velocity.y>0f && !isGrounded())
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
        if(Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger("Attack");
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
        boxCollider.enabled= true;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball[1]") || collision.gameObject.CompareTag("Ball[0]")  || collision.gameObject.CompareTag("CannonBall") || collision.gameObject.CompareTag("Pearl") || collision.gameObject.CompareTag("Spike"))
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
            score+=2;
        }
        if (collision.gameObject.CompareTag("Blue"))
        {
            collectables.Play();
            Destroy(collision.gameObject);
            score+=5;
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            respawnPoint= transform.position;
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
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            transform.position = respawnPoint;

        }
        if (collision.gameObject.CompareTag("IntoTheShip"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        ScoreText.text = "Treasures: " + score;

    }
    IEnumerator WhereIsTheMapSound()
    {
        yield return new WaitForSeconds(5f);
        whereIsTheMapSound.Play();
    }
}
