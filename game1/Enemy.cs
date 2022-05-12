using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private RaycastHit2D hit;

    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public bool canMove = true;
    public bool canDie = true;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (canMove)
        {
            hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
            move();
        }

    }

    void move()
    {

        if (hit.collider)
        {
            Vector2 pos = transform.position;
            pos.x = pos.x + speed * Time.deltaTime;
            // rgb.MovePosition(pos);
            transform.position = pos;
        }
        else
        {

            Vector2 face = transform.localScale;
            face.x *= -1;
            transform.localScale = face;
            speed *= -1;

        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        // animator.SetBool("IsDead", true);
        this.enabled = false;
        gameObject.SetActive(false);
    }


}



