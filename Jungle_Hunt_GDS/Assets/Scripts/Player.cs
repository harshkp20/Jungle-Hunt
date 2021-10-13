using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Vitals")]
    public float amount;
    public float maxHealth;
    public float movementSpeed = 40f;
    

    [Header("References")]
    public HealthBarValue HealthBar;
    CharacterController2D controller;
    Animator animator;
    float InputX = 0;
    public float currentHealth;
    bool Jump = false;
    public Text MainScore;
    public Score Score;
    public GameObject GameOverImage;
    public GameObject GameWinImage;
    public int counter,totalEnemies;
    public AudioSource GameOver;
    public AudioSource GameWin;
    public AudioSource MainAudio;

    void Start()
    {
        currentHealth=maxHealth;
        HealthBar = this.transform.GetChild(0).gameObject.GetComponent<HealthBarValue>();
        HealthBar.MaxHealthValue(maxHealth);
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        Score = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Score>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            Damage(amount);
        }        
    }

    public void Damage(float amount)
    {
        currentHealth-=amount;
        HealthBar.HealthValue(currentHealth);
        if(currentHealth<=0)
        {
            MainAudio.Stop();
            GameOver.Play();
            GameOverImage.SetActive(true);
            MainScore.text = Score.score.ToString();
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(counter==totalEnemies)
        {
            Score.score+=100;
            MainAudio.Stop();
            GameWin.Play();
            GameWinImage.SetActive(true);
        }
        if(transform.position.y <-3.44) 
        {
            MainAudio.Stop();
            GameOver.Play();
            GameOverImage.SetActive(true);
            MainScore.text = Score.score.ToString();
            this.gameObject.SetActive(false);
        }
        GetInput();
    }
    void FixedUpdate()
    {
        MovementPlayer();
    }

    private void GetInput()
    {
        InputX = Input.GetAxis("Horizontal")*movementSpeed;
        if(InputX != 0 && controller.m_Grounded)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            Jump = true;
        }
        else Jump = false;
        animator.SetBool("OnGround", controller.m_Grounded);
    }

    private void MovementPlayer()
    {
        controller.Move(InputX*Time.deltaTime ,false, Jump);
    }
}
