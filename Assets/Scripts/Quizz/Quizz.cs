using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quizz
{
    public ScholarLevel scolarLevel;
    [Range(1, 3)]
    public int difficulty = 1;
    public Subject subject;
    public string question;
    public List<string> listAnswer;
    public List<string> listBadAnswer;

    public Quizz(string question, List<string> listAnswer, List<string> listBadAnswer, ScholarLevel scolarLevel, int difficulty, Subject subject)
    {
        this.question = question;
        this.listAnswer = listAnswer;
        this.listBadAnswer = listBadAnswer;
        this.scolarLevel = scolarLevel;
        this.difficulty = difficulty;
        this.subject = subject;
    }
}

public enum ScholarLevel
{
    CM1,
    CM2,
    SIXIEME,
    CINQUIEME,
}

public enum Subject
{
    FRANCAIS,
    MATHS,
    ANGLAIS,
}
