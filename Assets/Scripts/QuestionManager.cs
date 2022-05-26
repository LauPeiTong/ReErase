using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private List<Question> questions;
    private Question selectedQuestion;
    private bool isCorrect;

    // Start is called before the first frame update
    void Start()
    {
        SelectQuestion();
        
    }

    //********************randomly choose question**************************
    public void SelectQuestion()
    {
        int value = Random.Range(0, questions.Count);
        selectedQuestion = questions[value];

        questionUI.SetQuestion(selectedQuestion);
    }

    //********************check the answer**************************
    public bool Answer(string answered)
    {
        isCorrect = false;
        
        if(string.Equals(answered.Trim(), selectedQuestion.correctAns.Trim(), System.StringComparison.OrdinalIgnoreCase)){
            isCorrect = true;
        }
        else{
            
        }
        Debug.Log(isCorrect);

        // Invoke("SelectQuestion", 0.4f);
        
        return isCorrect;
    }

    
    
}


