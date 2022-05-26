using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    //HP
    public Image hp;
    public TMP_Text hpText;

    //EXP
    public Image exp;
    public TMP_Text expText;
    public TMP_Text expUpText;

    //Level
    [SerializeField] private Canvas canvasLevelUp;
    public AudioClip levelUpClip;
    public TMP_Text levelText;

    //pause
    [SerializeField] private Canvas canvasPause;
    public Button pauseButton;
    
    //game over
    [SerializeField] private Canvas canvasGameOver;
    public AudioClip gameOverClip;
    public static PlayerUI instance{get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }


    public void UpdateHP(int currentHp, int maxHp){
        hp.fillAmount = (float) currentHp / (float) maxHp;
        hpText.text = currentHp + " / " + maxHp;
    }

    public void UpdateEXP(int currentEXP, int maxEXP){
        exp.fillAmount = (float) currentEXP / (float) maxEXP;
        expText.text = currentEXP + " / " + maxEXP;
    }

    public void LevelUp(int currentLevel){
        AudioManager.instance.AudioPlay(levelUpClip);
        canvasLevelUp.gameObject.SetActive(true);
        levelText.text = currentLevel + "";

        StartCoroutine (LateCall());
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds (1f);
        canvasLevelUp.gameObject.SetActive (false);
    }
    
    public void PauseGame(){
        canvasPause.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void PlayGame(){
        canvasPause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu(){
        canvasGameOver.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void PlayAgain(){
        canvasGameOver.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void GameOver(){
        AudioManager.instance.AudioPlay(gameOverClip);
        canvasGameOver.gameObject.SetActive(true);
    }

    public void ShowEXPUp(int amount){
        expUpText.text = "+ " + amount;

        expUpText.gameObject.SetActive(true);
        StartCoroutine(hideText(2f));

    }

    IEnumerator hideText (float secondsToWait)
    {
        yield return new WaitForSeconds (secondsToWait);
        expUpText.gameObject.SetActive(false);
        
    }
   
}
