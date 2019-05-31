using UnityEngine;

public class BowPart : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Bow bow = default;

    private Arrow lastArrow;
    private bool isColliding = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arrow" && !isColliding)
        {
            isColliding = true;
            lastArrow = other.GetComponent<Arrow>();
            bow.NotifyHitCollider(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Arrow" && isColliding)
        {
            isColliding = false;
            bow.NotifyQuitCollide();
        }
    }

    void OnTriggerStay(Collider other)
    {
        bow.TryToAttach();
    }

    public bool AttachArrow()
    {
        return lastArrow.TryAttachArrow();
    }
}
