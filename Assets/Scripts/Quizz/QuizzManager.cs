﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzManager : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField]
    private int nbWave = 5;

    [Header("Setup")]
    [SerializeField]
    private GameObject wavePrefab = default;
    [SerializeField]
    private QuizzEditorWrap quizzWrap = default;

    // ---- INTERN ----
    private int currentWaveNum = 0;
    private List<List<Quizz>> listAllQuizzScholarLevelAndDifficulty; 

    public void StartSpawningForSchoolAndLevel(ScholarLevel scholarLevel, int difficulty)
    {
        LoadQuestions();

        GetQuizzesForScolarLevelAndDifficulty(scholarLevel, difficulty);
        DisplayListQuizzes(); // affD

        SpawnWave();
    }


    public void NotifyWaveEnd()
    {
        ++currentWaveNum;
        if(currentWaveNum < nbWave)
        {
            SpawnWave();
        }
        else
        {
            // TODO
        }
    }


    private void LoadQuestions()
    {
        quizzWrap.quizzContainer = SaveJsonSystem.LoadQuizzes();
    }

    private void GetQuizzesForScolarLevelAndDifficulty(ScholarLevel scholarLevel, int difficulty)
    {
        this.listAllQuizzScholarLevelAndDifficulty = new List<List<Quizz>>();

        // create temporary list with all the quizzes for scholar level and difficulty
        List<Quizz> listAllQuizzScholarLevelAndDifficulty = new List<Quizz>();
        foreach(Quizz q in quizzWrap.quizzContainer.listQuizz)
        {
            if(q.scolarLevel == scholarLevel && q.difficulty == difficulty)
            {
                listAllQuizzScholarLevelAndDifficulty.Add(q);
            }
        }

        foreach (Subject subject in Enum.GetValues(typeof(Subject)))
        {
            List<Quizz> listQuizzS = new List<Quizz>();
            foreach(Quizz q in listAllQuizzScholarLevelAndDifficulty)
            {
                if(q.subject == subject)
                {
                    listQuizzS.Add(q);
                }
            }

            if(listQuizzS.Count > 0)
                this.listAllQuizzScholarLevelAndDifficulty.Add(listQuizzS);
        }
    }

    // affD
    private void DisplayListQuizzes()
    {
        foreach(List<Quizz> lq in listAllQuizzScholarLevelAndDifficulty)
        {
            foreach(Quizz q in lq)
            {
                Debug.Log("quizz : " + q.question);
            }
        }
    }

    private void SpawnWave()
    {
        GameObject waveCloneGO = (GameObject)Instantiate(wavePrefab);
        Wave waveClone = waveCloneGO.GetComponent<Wave>();

        waveClone.SetQuizzFromMananger(GetRandomQuizz(), this);
    }

    private Quizz GetRandomQuizz()
    {
        if (listAllQuizzScholarLevelAndDifficulty.Count == 0)
            throw new Exception("quizz list is empty");

        // TODO get quizz with user score
        int indexSubject = UnityEngine.Random.Range(0, listAllQuizzScholarLevelAndDifficulty.Count);
        int indexQuizz = UnityEngine.Random.Range(0, listAllQuizzScholarLevelAndDifficulty[indexSubject].Count);
        Quizz res = listAllQuizzScholarLevelAndDifficulty[indexSubject][indexQuizz];

        listAllQuizzScholarLevelAndDifficulty[indexSubject].Remove(res); // remove it from the list
        if(listAllQuizzScholarLevelAndDifficulty[indexSubject].Count == 0)
        {
            listAllQuizzScholarLevelAndDifficulty.Remove(listAllQuizzScholarLevelAndDifficulty[indexSubject]);
        }

        return res;
    }
}