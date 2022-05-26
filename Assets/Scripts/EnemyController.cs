using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //speed, rbody, anim, type
    public float speed = 3f;
    private Rigidbody2D rbody;
    private Animator anim;
    private bool isVanished;
    private bool isAttacked;
    private AudioSource audioSource;
    public AudioClip vanishedClip;
    [SerializeField] private string enemyType;


    //direction
    public bool isVertical;
    private Vector2 moveDirection;

    //direction timer
    public float changeDirTime = 2f;
    private float changeDirTimer;

    //question
    [SerializeField] private Canvas canvasQuestion;
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private QuestionUI questionUI;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        //direction
        moveDirection = isVertical? Vector2.up : Vector2.right;

        //direction timer
        changeDirTimer = changeDirTime;
        isVanished = false;
        isAttacked = false;

    }

    // Update is called once per frame
    void Update()
    {
        //if player answer question correctly
        if(isVanished) {
            audioSource.mute = true;
        }

        //if the question is displayed
        if(isAttacked){
            return;
        }

        //direction timer
        changeDirTimer -= Time.deltaTime;
        if(changeDirTimer < 0)
        {
            moveDirection *= -1;
            changeDirTimer = changeDirTime;
        }

        if(enemyType.Contains("Monster")){
            //dierection
            Vector2 position = rbody.position;
            position.x += moveDirection.x * speed * Time.deltaTime;
            position.y += moveDirection.y * speed * Time.deltaTime;

            rbody.MovePosition(position);

            //animator
            anim.SetFloat("MoveX", moveDirection.x);
            anim.SetFloat("MoveY", moveDirection.y);

        }
    }

    //********************collide by player or bullet**************************
    void OnCollisionEnter2D(Collision2D others)
    {
        PlayerController pc = others.gameObject.GetComponent<PlayerController>();
        BulletController bc = others.gameObject.GetComponent<BulletController>();

        if(others.gameObject.CompareTag("Player"))
        {
            if(isAttacked){
                return;
            }
            
            pc.ChangeHP(-1);
            rbody.isKinematic = true;
            rbody.velocity = Vector2.zero;
        }
        if(others.gameObject.CompareTag("Bullet")){
            rbody.isKinematic = true;
            rbody.velocity = Vector2.zero;
        }
    }

    //********************attack**************************
    public void Attacked()
    {      
        isAttacked = true;

        questionUI.SetEnemy(this.gameObject);
        questionManager.SelectQuestion();
        canvasQuestion.gameObject.SetActive(true);

    }

    public void Vanished()
    {
        isVanished = true;
        isAttacked = false;
        AudioManager.instance.AudioPlay(vanishedClip);
        anim.SetTrigger("Vanished");

        // Destroy(this.gameObject);
    }

    public string GetEnemyType(){
        return enemyType;
    }

    public void SetIsAttacked(bool isAttacked){
        this.isAttacked = isAttacked;
    }

}

