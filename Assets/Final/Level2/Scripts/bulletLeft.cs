using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLeft : MonoBehaviour
{
    SpriteRenderer sprite;
    Rigidbody2D rigidbody;
    Animator animator;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
        StartCoroutine(bulletDestroyAnimation());

    }

    // Update is called once per frame
    void Update()
    {
        sprite.flipX = true;
        rigidbody.velocity = new Vector2(-speed,0);
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
