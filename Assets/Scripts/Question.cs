using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string questionTitle;
    public List<string> options;
    public string correctAns;
    public string correctDialog;
    public string wrongDialog;
    public string enemyName;


}