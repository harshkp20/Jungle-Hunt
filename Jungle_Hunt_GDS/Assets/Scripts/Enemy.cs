using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public bool StartShooting;
    public float damageAmount;
    public float maxHealth;
    public float Speed = 30f;
    public float gapBetweenBullets=0.5f;
    public float visionRange = 10f;
    public float shootRange = 4f;

    [Header("References")]
    public GameObject Player;
    public HealthBarValue HealthBar;
    public Transform ShootPoint;
    public ParticleSystem BulletBurst;
    public GameObject Bullet;
    Animator animator;
    CharacterController2D controller;
    public float currentHealth;
    public Score Score;
    public AudioSource GameOver;
    public AudioSource GameWin;


    void Start()
    {
        currentHealth=maxHealth;
        HealthBar = this.transform.GetChild(3).gameObject.GetComponent<HealthBarValue>();
        HealthBar.MaxHealthValue(maxHealth);
        ShootPoint = this.transform.GetChild(2).gameObject.transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        BulletBurst = ShootPoint.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        Score = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Score>();
    }

    void Update()
    {
        if(transform.position.y<-4.2)
        {
            Player.GetComponent<Player>().counter+=1;
            Destroy(this.gameObject);
        } 
        actionOnPlayer();
        if(StartShooting)
        {
            if(gapBetweenBullets<=0)
            {
                GameObject bullet = Instantiate(Bullet,ShootPoint.position,ShootPoint.rotation);
                Vector2  Direction = Player.transform.position - ShootPoint.position;
                bullet.GetComponent<Rigidbody2D>().velocity = Direction*Speed;
                BulletBurst.Play();
                gapBetweenBullets=0.5f;
            }
            else
            {
                gapBetweenBullets-=Time.deltaTime;
            }
        }        
    }

    void actionOnPlayer()
    {
        float dist = Vector3.Distance(Player.transform.position, transform.position);

        if (dist > shootRange && dist < visionRange)
        {
            controller.Move(Speed * Time.deltaTime * Mathf.Sign(Player.transform.position.x - transform.position.x), false, false);
            animator.SetBool("Running", dist < visionRange);
        }
        else if (dist < shootRange)
        {
            StartShooting = true;
        }
        else if (dist > shootRange) StartShooting = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Damage(damageAmount);
        }        
    }

    public void Damage(float damageAmount)
    {
        currentHealth-=damageAmount;
        HealthBar.HealthValue(currentHealth);
        Score.score+=20;
        if(currentHealth<=0)
        {
            animator.SetBool("isDead", true);
            Player.GetComponent<Player>().counter+=1;
            Destroy(this.gameObject);
            Score.score+=80;
            Score.score+=Player.GetComponent<Player>().counter *10;
        }
    }

    void checkForDeath()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
