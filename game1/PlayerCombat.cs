using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    float nextEnemyHitTime = 0f;

    public int lifesCount = 3;
    public GameObject[] lifes;
    // Update is called once per frame 

    void Start()
    {
        for (int i = 2; i >= 0; i--)
            lifes[i].SetActive(false);

        lifesCount = PlayerPrefs.GetInt("lifes", 3);
        if (lifesCount < 1)
            lifesCount = 3;
        for (int i = lifesCount - 1; i >= 0; i--)
            lifes[i].SetActive(true);



    }
    void Update()
    {
        // checkHitEnemy();
        if (Time.time >= nextAttackTime)
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            // if (Time.time >= nextEnemyHitTime)

            {

                Debug.Log("enemyCol");
                lifes[lifesCount - 1].SetActive(false);
                lifesCount--;
                PlayerPrefs.SetInt("lifes", lifesCount);
                PlayerPrefs.Save();

                if (lifesCount < 1)
                {
                    Die();
                }
                else
                {
                    Hurt();
                }
                //lastFlag = col.transform.position;

                // Debug.Log("flag x" + lastFlag.x);
                nextEnemyHitTime = Time.time + 1f / attackRate;

            }
        }


    }


    void Die()
    {
        Debug.Log("die");
        //Play an attack animation 
        animator.SetBool("IsDead", true);
        // Detect enemies in range of attack 
        // this.enabled = false;

        GetComponent<Player2>().enabled = false;

        StartCoroutine(ShowScreen());


    }

    IEnumerator ShowScreen()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        GetComponent<Player2>().ui.loseScreen.SetActive(true);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    void Hurt()
    {
        Debug.Log("hurt");
        //Play an attack animation 
        animator.SetTrigger("hurt");
        // Detect enemies in range of attack 
        // this.enabled = false;

        //GetComponent<Player2>().enabled = false;

    }
    void Attack()
    {
        //Play an attack animation 
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage them 
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>().canDie)
            {
                Debug.Log(enemy.GetComponent<Enemy>().currentHealth);
                enemy.GetComponent<Enemy>().TakeDamage(10);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);


    }
}