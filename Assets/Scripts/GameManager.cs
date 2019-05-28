using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private QuizzManager quizzManager = default;

    void Start()
    {
        // todo set the scholar level and difficulty
        quizzManager.StartSpawningForSchoolAndLevel(ScholarLevel.CM1, 1);
    }
}
