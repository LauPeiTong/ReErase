using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//********************Question with 4 options**************************
public class QuestionUI : MonoBehaviour
{
    // image
    [SerializeField] private Sprite correctImage, wrongImage, normalImage;
    [SerializeField] private Sprite monster, monsterB, email, software;

    //text
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text correctText;
    [SerializeField] private TMP_Text wrongText;
    [SerializeField] private TMP_Text nameText;

    //canvas
    [SerializeField] private Canvas canvasQuestion;
    
    //question manager
    [SerializeField] private QuestionManager questionManager;

    //game object
    [SerializeField] private List<Button> options;
    [SerializeField] private GameObject player;
    [SerializeField] private Image enemyImage;
    private GameObject enemy;

    //EXP points
    [SerializeField] private List<int> expList;

    //question
    private Question question;

    //isAnswered
    private bool isAnswered;
    private bool isCorrect;

    //audio
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip wrongClip;

    //touch count
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        canvasQuestion.gameObject.SetActive(true);
        
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => Onclick(localBtn));
        }
    }

    void Update()
    {
        //Input.anyKeyDown is only for testing
        if ((Input.touchCount > 0 || Input.anyKeyDown) && isAnswered){
            count++;
        }

        if(count > 1){
            StartCoroutine (hideUI (canvasQuestion, 1f, isCorrect));
            count = 0;
        }
       
    }

    //********************show question and options on screen**************************
    public void SetQuestion(Question question)
    {
        questionText.gameObject.SetActive(true);
        correctText.gameObject.SetActive(false);
        wrongText.gameObject.SetActive(false);

        this.question = question;
        questionText.text = question.questionTitle.Replace("<br>", "\n");

        UsernameData data = SaveLoad.LoadData();
        questionText.text = question.questionTitle.Replace("<name>", data.username);

        nameText.text = question.enemyName;

        List<string> answerList = question.options;
        
        for(int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TMP_Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.sprite = normalImage;

        }

        isAnswered = false;
    }

    
    //********************show correct/wrong condition on screen**************************
    private void Onclick(Button btn)
    {
        if(!isAnswered)
        {
            isAnswered = true;
            isCorrect = questionManager.Answer(btn.name);

            questionText.gameObject.SetActive(false);

            if(isCorrect)
            {
                btn.image.sprite = correctImage;
                correctText.text = question.correctDialog;
                correctText.gameObject.SetActive(true);
                AudioManager.instance.AudioPlay(correctClip);

                //player points
                int index = Random.Range(0, expList.Count);
                int exp = expList[index];
                player.GetComponent<PlayerController>().ChangeEXP(exp);

            }
            else
            {
                btn.image.sprite = wrongImage; 
                wrongText.text = question.wrongDialog;
                wrongText.gameObject.SetActive(true);

                //player points
                player.GetComponent<PlayerController>().ChangeHP(-1);
            }

        }
    }
    
    //********************set the enemy**************************
    public void SetEnemy(GameObject other){
        enemy = other;

        string enemyType = enemy.GetComponent<EnemyController>().GetEnemyType();
        SetEnemyType(enemyType);
    }

    public void SetEnemyType(string enemyType){
        if(string.Equals(enemyType, "Monster", System.StringComparison.OrdinalIgnoreCase))
        {
            enemyImage.sprite = monster;
        }
        else if(string.Equals(enemyType, "MonsterB", System.StringComparison.OrdinalIgnoreCase))
        {
            enemyImage.sprite = monsterB;
        }
        else if(string.Equals(enemyType, "Email", System.StringComparison.OrdinalIgnoreCase))
        {
            enemyImage.sprite = email;
        }
        else{
            enemyImage.sprite = software;
        }

    }

    //********************make canvas disappear**************************
    //********************need to change**************************
    IEnumerator hideUI (Canvas guiParentCanvas, float secondsToWait, bool val, bool show = false)
    {
        yield return new WaitForSeconds (secondsToWait);
        guiParentCanvas.gameObject.SetActive (show);

        if(val){
            enemy.GetComponent<EnemyController>().Vanished();
        }
        else{
            enemy.GetComponent<EnemyController>().SetIsAttacked(false);
        }
    }




}
