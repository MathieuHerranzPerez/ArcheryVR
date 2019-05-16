using UnityEngine;

public class Arrow : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        ArrowManager.Instance.AttachBowToArrow();
    }
}
