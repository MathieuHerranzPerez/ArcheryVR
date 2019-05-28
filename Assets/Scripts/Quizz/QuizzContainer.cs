using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizzContainer
{
    public List<Quizz> listQuizz;

    public void SetListQuizz(List<Quizz> listQuizz)
    {
        this.listQuizz = listQuizz;
    }
}
