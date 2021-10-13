using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int score;
    public Text MainScore;
    public GameObject StartImage;

    public GameObject GameOverImage;
    public GameObject GameWinImage;
    public GameObject RestartImage;
    public AudioSource MainAudio;
    public GameObject HealthBar;
    public float timer=1f;
    // Start is called before the first frame update
    void Start()
    {
        score =0;
    }

    public void StartGame()
    {
        StartImage.SetActive(false);  
        HealthBar.SetActive(true);
        MainAudio.Play();      
    }



    // Update is called once per frame
    void Update()
    {
        MainScore.text = score.ToString();
        if(GameOverImage.activeSelf)
        {
            if(timer<=0)
            {
                GameOverImage.SetActive(false);
                RestartImage.SetActive(true);
            }
            else timer-=Time.deltaTime;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
