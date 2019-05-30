using System.Collections;
using UnityEngine;

public class TargetNumber : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private ParticleSystem effectPrefabForArrow = default;
    [SerializeField]
    private int num = default;

    // ---- INTERN ----
    private float timeBetweenCollide = 1f;
    private bool canCollide = true;
    private bool hasExit = true;

    void OnTriggerEnter(Collider other)
    {
        if(canCollide && hasExit)
        {
            if (other.gameObject.tag == "Arrow")
            {
                Arrow arrow = other.GetComponent<Arrow>();
                arrow.MultiplyPower(num, effectPrefabForArrow);
                hasExit = false;
                canCollide = false;
                StartCoroutine(StartDelay());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasExit = true;
    }

    private IEnumerator StartDelay()
    {
        float time = 0f;
        while(time < timeBetweenCollide)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canCollide = true;
    } 
}
