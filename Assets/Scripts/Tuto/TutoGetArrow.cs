using System.Collections;
using UnityEngine;

public class TutoGetArrow : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private CanvasGroup imageControllerRed = default;

    private bool canStart = true;

    void Update()
    {
        if (canStart)
        {
            StartCoroutine(ToggleController());
            canStart = false;
        }
    }

    private IEnumerator ToggleController()
    {
        float time = 0f;
        while(time < 1f)
        {
            imageControllerRed.alpha = time;
            time += Time.deltaTime;
            yield return null;
        }
        
        while(time > 0f)
        {
            imageControllerRed.alpha = time;
            time -= Time.deltaTime;
            yield return null;
        }

        canStart = true;
    }
}
