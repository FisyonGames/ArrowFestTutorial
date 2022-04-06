using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform arrowContainer;
    [SerializeField] private Transform target;
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
        //arrowContainer.GetComponent<ArrowContainerMovement>().enabled = false;
    }

    void Update()
    {
        /* // Sol veya sağ ok tuşları ile hareket başlatılır...Bu tuşlarla kontrol edilir.
        if (Input.touchCount > 0 || Input.anyKeyDown)
        {
            arrowContainer.GetComponent<ArrowContainerMovement>().enabled = true;
        } */

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

    /* void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    } */
}
