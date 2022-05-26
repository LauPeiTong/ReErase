using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip collectableClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(other.gameObject.CompareTag("Player")){
            if(pc.MyCurrentHP < pc.MyMaxHP){
                pc.ChangeHP(1);
                AudioManager.instance.AudioPlay(collectableClip);

                Destroy(this.gameObject);
            }
        }

        
    }
}
