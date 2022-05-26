using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private QuestionUI questionUI;
    [SerializeField]
    private List<Question> questions;
    private Question selectedQuestion;

    // Start is called before the first frame update
    void Start()
    {
        SelectQuestion();
    }

    // Update is called once per frame
    void SelectQuestion()
    {
        int value = Random.Range(0, questions.Count);
        selectedQuestion = questions[value];

        questionUI.SetQuestion(selectedQuestion);
    }

    public bool Answer(string answered)
    {
        bool isCorrect = false;
        if(answered == selectedQuestion.correctAns){
            isCorrect = true;
        }
        else{
            
        }

        // Invoke("SelectQuestion", 0.4f);
        return isCorrect;
    }
}

[System.Serializable]
public class Question
{
    public string questionTitle;
    public List<string> options;
    public string correctAns;

}
