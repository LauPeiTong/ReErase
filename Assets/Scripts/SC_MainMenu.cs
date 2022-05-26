using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;

    //audio
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private float delay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 0.4f;

        AudioManager.instance.AudioPlay(mainMenuClip);
        MainMenuButton();
    }

    // Update is called once per frame
    public void PlayNowButton(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void MainMenuButton(){
        MainMenu.SetActive(true);
        Debug.Log("main menu");

    }

    public void QuitButton(){
        Application.Quit();
    }

    IEnumerator AudioShow(){
        float timer = 0;
        float currentVolumn = AudioListener.volume;

        while(timer < delay){
            timer += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolumn, 0.4f, timer/delay);
            yield return null;
        }
    }

    
}
