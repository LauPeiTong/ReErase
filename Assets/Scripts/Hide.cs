using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public float after = 5.0f;

    IEnumerator hideUI (GameObject guiParentCanvas, float secondsToWait,bool show = false)
    {
        yield return new WaitForSeconds (secondsToWait);
        guiParentCanvas.SetActive (show);
    }
}
