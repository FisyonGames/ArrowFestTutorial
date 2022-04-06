using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform levelCompletedPanel;
    [SerializeField] private Transform failPanel;

    private bool isLevelCompleted = false;
    private bool isFail = false;

    public bool IsLevelCompleted { get {return isLevelCompleted;} set {isLevelCompleted = value;}}
    public bool IsFail { get {return isFail;} set {isFail = value;}}


    void Start()
    {
        levelCompletedPanel.gameObject.SetActive(false);
        failPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if(IsLevelCompleted) Invoke("ActivateLevelCompletedPanel", 2.0f);
        if(isFail) failPanel.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ActivateLevelCompletedPanel() 
    {
        levelCompletedPanel.gameObject.SetActive(true);
    }
    void ActivateFailPanel() 
    {
        levelCompletedPanel.gameObject.SetActive(true);
    }
}
