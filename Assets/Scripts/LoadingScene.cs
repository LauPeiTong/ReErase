using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Image progressBar;
    void Start()
    {
        
        StartCoroutine(LoadSceneAsynchronously());
    }

    IEnumerator LoadSceneAsynchronously()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(3);

        while(loadingOperation.isDone == false){
            Debug.Log(loadingOperation.progress);
            progressBar.fillAmount = loadingOperation.progress;

            yield return new WaitForEndOfFrame();
        }
            
    }
}
