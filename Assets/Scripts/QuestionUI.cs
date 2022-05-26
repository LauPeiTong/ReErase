using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private Text questionText;
    [SerializeField] private List<Button> options;
    [SerializeField] private Sprite correctImage, wrongImage, normalImage;
    [SerializeField] private Canvas questionCanvas;

    private Question question;
    private bool isAnswered;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => Onclick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        questionText.text = question.questionTitle;

        List<string> answerList = question.options;
        
        for(int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.sprite = normalImage;
        }

        isAnswered = false;
    }

    private void Onclick(Button btn)
    {
        if(!isAnswered)
        {
            isAnswered = true;
            bool val = questionManager.Answer(btn.name);

            if(val)
            {
                btn.image.sprite = correctImage;
            }
            else
            {
                btn.image.sprite = wrongImage;
            }

            questionCanvas.gameObject.SetActive(false);
        }
    }



}
