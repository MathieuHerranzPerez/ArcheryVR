using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzEditorWrap : MonoBehaviour
{
    public QuizzContainer quizzContainer;

    public void SaveJson()
    {
        SaveJsonSystem.SaveQuizzes(quizzContainer);
    }
}
