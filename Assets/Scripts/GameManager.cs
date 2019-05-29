using System;
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

        // récupere profil + grade + progression
        StartCoroutine(ProfileManager.LoadProfileInformation(1,this));

    }

    public void ContinueStart()
    {
        Debug.Log("profil : " + ProfileManager.PROFIL.nom); // affD
        Debug.Log("progression : " + ProfileManager.PROGRESSION.difficulteMaths + " / " + ProfileManager.PROGRESSION.xpmaths); // affD
        Debug.Log("grade : " + ProfileManager.GRADE.nom); // affD

        quizzManager.StartSpawningForSchoolAndLevel();
    }


    
}
