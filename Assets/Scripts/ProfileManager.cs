using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileManager
{
    public static Profil PROFIL;
    public static Grade GRADE;
    public static Progression PROGRESSION;

    public static IEnumerator LoadProfileInformation(int id, GameManager gm)
    {
        Debug.Log("get profile..."); // affD
        yield return gm.StartCoroutine(LoadProfil(id));
        Debug.Log("get progression..."); // affD
        yield return gm.StartCoroutine(LoadProgression(id));
        Debug.Log("get grade..."); // affD
        yield return gm.StartCoroutine(LoadGrade(ProfileManager.PROGRESSION.gradeId));

        // continue le déroulement du programme une fois les données chargées
        gm.ContinueStart();
    }


    private static IEnumerator LoadProfil(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/ProfilAPI/" + id))
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
                Debug.Log(json);
                ProfileManager.PROFIL = JsonUtility.FromJson<Profil>(json);
            }

        }
    }

    private static IEnumerator LoadProgression(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/ProgressionAPI/" + id))
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

    private static IEnumerator LoadGrade(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/GradeAPI/" + id)) // TODO remettre en Post et le form en param
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
