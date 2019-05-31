using System.Collections;
using UnityEngine;

public abstract class Buzzer : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenTwoHit = 0.3f;
    [SerializeField]
    protected BuzzerManager buzzerManager = default;

    // ---- INTERN ----
    private bool canBeHit = true;

    protected abstract void ActionWhenHit();

    void OnCollisionEnter(Collision other)
    {
        if (canBeHit)
        {
            if (other.gameObject.tag == "Hammer")
            {
                canBeHit = false;
                ActionWhenHit();

                StartCoroutine(StartDelay());
            }
        }
        else
        {
            Debug.Log("Can't be hit");
        }
    }

    private IEnumerator StartDelay()
    {
        float time = 0f;
        while(time < timeBetweenTwoHit)
        {
            time += Time.deltaTime;
            yield return null;
        }

        canBeHit = true;
    }
}
