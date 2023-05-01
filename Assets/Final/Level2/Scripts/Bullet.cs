using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //float bulletDirection=1;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
      // player2= GetComponent<Player2Movement>();
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(bulletDestroyAnimation());
    }

    // Update is called once per frame
    void Update()
    {
       // bulletDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed,0);
    }
    IEnumerator bulletDestroyAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        animator.SetTrigger("Disappear");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
