using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject menuPanel;

    void Awake()
    {
        if(LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        levelName.text = SceneManager.GetActiveScene().name;
    }

    public void GameOver()
    {
        deathPanel.SetActive(true);
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void Menu()
    {
        menuPanel.SetActive(true);
    }

    public void Back()
    {
        menuPanel.SetActive(false);
    }

    public void NextLevel()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        if (indexScene < SceneManager.sceneCountInBuildSettings - 1 )
        {
            SceneManager.LoadScene(indexScene + 1);
        }
        else{SceneManager.LoadScene(0);}
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartPage()
    {
        SceneManager.LoadScene(0);
    }
    
}
