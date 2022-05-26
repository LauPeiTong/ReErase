using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //speed, rigidbody, anim
    public float speed = 5f;
    private Rigidbody2D rbody;
    private Animator anim;
    private AudioSource audioSource;

    //bullet
    public GameObject bulletPrefab; 
    public Button attackButton;
    
    //moving direction
    private Vector2 lookDirection = new Vector2(1, 0);

    //points
    private int currentHP;
    private int currentEXP;
    private int currentLevel;
    private int currentMaxEXP;
    private int maxHP = 5;
    private int maxEXP;
    [SerializeField] private List<int> maxEXPList;

    //invincible time
    private float invincibleTime = 2f;
    private float invincibleTimer;
    private bool isInvincible;

    //joystick
    public Joystick joystick;

    //audio
    public AudioClip playerHitClip;
    public AudioClip lauchClip;

    // Start is called before the first frame update
    void Start()
    {
         rbody = GetComponent<Rigidbody2D>();
         anim = GetComponent<Animator>();
         audioSource = GetComponent<AudioSource>();

         currentHP = 2;
         currentEXP = 0;
         currentLevel = 1;
         currentMaxEXP = maxEXPList[0];
         maxEXP = maxEXPList[maxEXPList.Count - 1];
         invincibleTimer = 0;
         attackButton.onClick.AddListener(() => Onclick(attackButton));

        PlayerUI.instance.UpdateHP(currentHP, maxHP);
        Debug.Log(currentEXP + "/" + currentMaxEXP);
        PlayerUI.instance.UpdateEXP(currentEXP, currentMaxEXP);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        //********************moving direction**************************
        Vector2 moveVector = new Vector2(moveX, moveY);
        if(moveVector.x != 0 || moveVector.y != 0){
            lookDirection = moveVector;

            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(audioSource.clip);
        }
        anim.SetFloat("MoveX", lookDirection.x);
        anim.SetFloat("MoveY", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);

        //********************moving**************************
        Vector2 position = rbody.position;
        position += moveVector * speed * Time.fixedDeltaTime;
        rbody.MovePosition(position);

        //********************invincible**************************
        if(isInvincible){
            invincibleTimer -= Time.fixedDeltaTime;
            if(invincibleTimer < 0){
                isInvincible = false;
                
            }

        }

        //********************level**************************
        if(currentEXP >= maxEXPList[currentLevel - 1]){
            currentLevel++;
            currentMaxEXP = maxEXPList[currentLevel];

            Debug.Log(currentLevel);
            Debug.Log(currentEXP + "/" + currentMaxEXP);

            PlayerUI.instance.LevelUp(currentLevel);

        }

        //********************exp**************************
        PlayerUI.instance.UpdateEXP(currentEXP, currentMaxEXP);
        
        
        
    }

    //********************hp**************************
    public void ChangeHP(int amount)
    {
         //is attacked
        if(amount < 0){
            if(isInvincible == true){
                return;
            }
            isInvincible = true;
            invincibleTimer = invincibleTime;
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(playerHitClip);
        }

        //health is between 0 and max
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        PlayerUI.instance.UpdateHP(currentHP, maxHP);

        Debug.Log(currentHP + "/" + maxHP);

        //********************game over**************************
        if(currentHP <= 0){
            PlayerUI.instance.GameOver();
        }
    }

    //********************exp**************************
    public void ChangeEXP(int amount)
    {
        currentEXP = Mathf.Clamp(currentEXP + amount, 0, maxEXP);
        PlayerUI.instance.ShowEXPUp(amount);
        
    }

    

    //********************attack**************************
    public void Onclick(Button btn){
        AudioManager.instance.AudioPlay(lauchClip);

        GameObject bullet = Instantiate(bulletPrefab, rbody.position + Vector2.up * -0.5f, Quaternion.identity);
        BulletController bc = bullet.GetComponent<BulletController>();
        if(bc != null){
            bc.Move(lookDirection, 500);
        }
    }

    //********************getter**************************
    public int MyMaxHP{get {return maxHP;}}
    public int MyCurrentHP{get{return currentHP;}}
}
