using System.IO;
using UnityEngine;

public static class SaveJsonSystem
{
    public static QuizzContainer quizzContainer;
    public static string path = Application.persistentDataPath + "/Saves/Quizz";

    public static void SaveQuizzes(/*QuizzContainer quizzContainer*/ QuizzContainer listQuizz)
    {
        string json = JsonUtility.ToJson(listQuizz);

        Debug.Log(json); // affD
        Debug.Log("Save at : " + path); // affD

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            // create it
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        // open or create
        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(json);
        } // close
    }

    public static QuizzContainer LoadQuizzes()
    {
        if (Directory.Exists(Path.GetDirectoryName(path)) && File.Exists(path))
        {
            // open
            using (StreamReader streamReader = File.OpenText(path))
            {
                string json = streamReader.ReadToEnd();
                Debug.Log(json); // affD

                return JsonUtility.FromJson<QuizzContainer>(json);
            }// close
        }
        else
        {
            Debug.LogWarning(path + "Does not exists (or we having a problem reading at it)");
            return null;
        }
    }

    /*
    public static IEnumerator LoadQuizzesFromDB(QuizzManager quizzManager)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr2019.azurewebsites.net/api/QuestionAPI"))
        {
            yield return webrequest.SendWebRequest();

            if(webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                quizzContainer = JsonUtility.FromJson<QuizzContainer>(json);

                Debug.Log("nb quizz : " + quizzContainer.listQuizz.Count);
            }

            quizzManager.continueSpawningForSchoolAndLevel();
        }
    }*/
}
