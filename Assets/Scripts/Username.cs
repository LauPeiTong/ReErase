using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Username : MonoBehaviour
{
    //name
    public string username;
    public TextMeshProUGUI usernameText;
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_Text welcomeName;

    //button
    [SerializeField] private Sprite disabledImage;
    public Button enterButton;

    //canvas
    [SerializeField] private Canvas canvasSetUsername;
    [SerializeField] private Canvas canvasWelcome;
    [SerializeField] private Canvas canvasNoticeBanner;

    //audio
    [SerializeField] private AudioClip buttonClip, welcomeClip;
    [SerializeField] private float delay = 1f;
    
    void Start(){
        UsernameData data = SaveLoad.LoadData();
        AudioManager.instance.AudioPlay(welcomeClip);
        canvasSetUsername.gameObject.SetActive(true);
        enterButton.interactable = false;
        
        if(data != null){
            username = data.username;
            welcomeName.text = "Welcome to Hilti City\n" + username + " !";
            StartCoroutine(hideUI(canvasSetUsername, canvasWelcome, canvasNoticeBanner, 7f));

        }
        
        // inputUsername.text = username;
    }

    //********************onclick**************************
    public void SaveGame(){
        SaveLoad.SaveData(this);
        AudioManager.instance.AudioPlay(buttonClip);
        welcomeName.text = "Welcome to Hilti City\n" + username + " !";

        StartCoroutine(hideUI(canvasSetUsername, canvasWelcome, canvasNoticeBanner, 7f));

    }

    //********************name**************************
    public void UpdateName(TMP_InputField inputField){
        usernameText.text = inputUsername.text;
        username = inputField.text;

        if(username.Length < 4 || username.Length > 10){
            enterButton.interactable = false;
        }
        else{
            enterButton.interactable = true;
        }

    }

    IEnumerator hideUI (Canvas canvasClose, Canvas canvasOpen, Canvas canvasOpen2, float secondsToWait)
    {
        canvasClose.gameObject.SetActive (false);
        canvasOpen.gameObject.SetActive (true);

        yield return new WaitForSeconds (secondsToWait - 2f);

        canvasOpen.gameObject.SetActive (false);
        canvasOpen2.gameObject.SetActive(true);

        yield return new WaitForSeconds (secondsToWait);

        float timer = 0;
        float currentVolumn = AudioListener.volume;
        while(timer < delay){
            timer += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolumn, 0, timer/delay);
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // LoadingScene.instance.LoadScene(1);

        


    }


}
