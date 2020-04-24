using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrClockScript : MonoBehaviour
{
    public float speed;
    public bool vertical;
    Rigidbody2D rigidbody2D;
    public ParticleSystem smokeparticle;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    public bool facingright = false;
    Animator animator;
    private bool broken = true;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        if (direction < 0 && !facingright)
        {
            Flip();
        }
        else if (direction > 0 && facingright)
        {
            Flip();
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingright)
            Flip();
        else if (h < 0 && facingright)
            Flip();
    }

    private void Flip()
    {
        facingright = !facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        smokeparticle.Stop();
        animator.SetTrigger("Fixed");
    }
}
