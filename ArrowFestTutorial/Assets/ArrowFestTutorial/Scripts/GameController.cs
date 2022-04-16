using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform levelCompletedPanel;
    [SerializeField] private Transform failPanel;
    [SerializeField] private Transform HoldAndMovePanel;
    [SerializeField] private Text scoreText;
    
    private int score;

    public int Score { get { return score; }  set { score = value; }}

    void Start()
    {
        score = 0;
        
        levelCompletedPanel.gameObject.SetActive(false);
        failPanel.gameObject.SetActive(false);
        HoldAndMovePanel.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.anyKeyDown)
        {
            HoldAndMovePanel.gameObject.SetActive(false);
        }
    }
    

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateLevelCompletedPanel() 
    {
        scoreText.text = score.ToString();
        levelCompletedPanel.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("LevelCompleted");
    }

    public void ActivateFailPanel() 
    {
        failPanel.gameObject.SetActive(true);
    }
}
