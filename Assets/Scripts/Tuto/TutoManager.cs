using System.Collections;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public ArrowManager arrowManager;
    public GameObject goodSphereGO1;
    public GameObject goodSphereGO2;
    public GameObject wrongSphereGO1;
    public GameObject wrongSphereGO2;
    public GameObject questionsContainerGO;

    public GameObject screenTutoGetArrow;
    public GameObject screenTutoPutArrow;
    public GameObject screenTutoPullString;
    public GameObject screenTutoLoseArrow;
    public GameObject screenTutoQuestion;
    public GameObject screenTutoShootSphere;


    private State state = State.GET_ARROW;

    void Start()
    {
        // display how to get an arrow
        screenTutoGetArrow.SetActive(true);
    }

    void Update()
    {
        if(state == State.GET_ARROW)
        {
            if(arrowManager.GetCurrentArrow() != null)
            {
                GoToNextState();
            }
        }
        else if(state == State.PUT_ARROW)
        {
            if(arrowManager.IsArrowAttached())
            {
                GoToNextState();
            }
        }
        else if(state == State.PULL_STRING)
        {
            if (arrowManager.IsStringPulled())
            {
                GoToNextState();
            }
        }
        else if(state == State.LOSE_ARROW)
        {
            if(arrowManager.GetCurrentArrow() == null)
            {
                GoToNextState();
            }
        }
        else if(state == State.QUESTIONS)
        {
            // todo
        }
        else if(state == State.SHOOT_SPHERE)
        {
            if (goodSphereGO1 == null && goodSphereGO2 == null)
            {
                GoToNextState();
            }
        }
    }


    private void GoToNextState()
    {
        if (state == State.GET_ARROW)
        {
            state = State.PUT_ARROW;
            screenTutoGetArrow.SetActive(false);

            // display how to put the arrow
            screenTutoPutArrow.SetActive(true);
        }
        else if (state == State.PUT_ARROW)
        {
            state = State.PUT_ARROW;
            screenTutoPutArrow.SetActive(false);

            // display how to pull string
            screenTutoPullString.SetActive(true);
        }
        else if (state == State.PULL_STRING)
        {
            state = State.LOSE_ARROW;
            screenTutoPullString.SetActive(false);

            // display hom to lose arrow
            screenTutoLoseArrow.SetActive(true);
        }
        else if (state == State.LOSE_ARROW)
        {
            state = State.QUESTIONS;
            screenTutoLoseArrow.SetActive(false);

            // display question and cursor to show it
            screenTutoQuestion.SetActive(true);


            StartCoroutine(CountTimeForQuestion());
        }
        else if (state == State.QUESTIONS)
        {
            state = State.SHOOT_SPHERE;
            screenTutoQuestion.SetActive(false);

            // display how to shoot
            screenTutoShootSphere.SetActive(true);
            questionsContainerGO.SetActive(true);

            goodSphereGO1.SetActive(true);
            goodSphereGO2.SetActive(true);
            wrongSphereGO1.SetActive(true);
            wrongSphereGO2.SetActive(false);
        }
        else if (state == State.SHOOT_SPHERE)
        {
            state = State.SCORE;
            screenTutoShootSphere.SetActive(false);
            questionsContainerGO.SetActive(false);
        }
        else if (state == State.SCORE)
        {
            
        }

        Debug.Log("State : " + state);
    }

    public void LeaveTuto()
    {

    }

    private IEnumerator CountTimeForQuestion()
    {
        float time = 0f;
        while(time < 5f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        GoToNextState();
    }

    private enum State
    {
        GET_ARROW,
        PUT_ARROW,
        PULL_STRING,
        LOSE_ARROW,
        QUESTIONS,
        SHOOT_SPHERE,
        SCORE
    }
}
