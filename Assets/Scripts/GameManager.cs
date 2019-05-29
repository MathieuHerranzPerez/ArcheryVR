using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private QuizzManager quizzManager = default;

    void Start()
    {

        // récupere profil + grade + progression
        StartCoroutine(LoadProfileInformation(1));

    }

    void ContinueStart()
    {
        Debug.Log("profil : " + ProfileManager.PROFIL.nom); // affD
        Debug.Log("progression : " + ProfileManager.PROGRESSION.difficulteMaths + " / " + ProfileManager.PROGRESSION.xpmaths); // affD
        Debug.Log("grade : " + ProfileManager.GRADE.nom); // affD

        quizzManager.StartSpawningForSchoolAndLevel();
    }


    private IEnumerator LoadProfileInformation(int id)
    {
        Debug.Log("get profile..."); // affD
        yield return StartCoroutine(LoadProfil(id));
        Debug.Log("get progression..."); // affD
        yield return StartCoroutine(LoadProgression(id)); 
        Debug.Log("get grade..."); // affD
        yield return StartCoroutine(LoadGrade(ProfileManager.PROGRESSION.gradeId));

        // continue le déroulement du programme une fois les données chargées
        ContinueStart();
    }


    private IEnumerator LoadProfil(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr2019.azurewebsites.net/api/ProfilAPI/" + id))
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                ProfileManager.PROFIL = JsonUtility.FromJson<Profil>(json);


            }

        }
    }

    private IEnumerator LoadProgression(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr2019.azurewebsites.net/api/ProgressionAPI/" + id)) 
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                ProfileManager.PROGRESSION = JsonUtility.FromJson<Progression>(json);

            }

        }
    }

    private IEnumerator LoadGrade(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr2019.azurewebsites.net/api/GradeAPI/" + id)) // TODO remettre en Post et le form en param
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                ProfileManager.GRADE = JsonUtility.FromJson<Grade>(json);
            }

        }
    }
}
