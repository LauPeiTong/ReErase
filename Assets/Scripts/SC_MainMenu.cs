using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    // Update is called once per frame
    public void PlayNowButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene1");
    }

    public void MainMenuButton(){
        MainMenu.SetActive(true);
    }

    public void QuitButton(){
        Application.Quit();
    }
}
