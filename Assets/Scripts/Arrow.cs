using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    public SteamVR_Action_Single squeezeAction;

    // ---- INTERN ----
    private bool isAttached = false;
    private bool isFired = false;
    private Rigidbody rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isFired)
            FollowGravity();
    }

    void OnTriggerStay(Collider other)
    {
        AttachArrow();
    }

    void OnTriggerEnter(Collider other)
    {
        AttachArrow();
    }

    public void Fire(Vector3 force)
    {
        isFired = true;
        rBody.velocity = force;
        rBody.useGravity = true;
    }


    private void FollowGravity()
    {
        float angle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //https://www.youtube.com/watch?v=bn8eMxBcI70
    private void AttachArrow()
    {
        // right hand /!\
        if(SteamVR_Input.GetBooleanAction("GrabGrip").GetState(SteamVR_Input_Sources.RightHand)) // Any ?
        {
            Debug.Log("GrabGrip down");
        }

        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
        if(!isAttached && triggerValue > 0.05f)
        {
            isAttached = true;
            Debug.Log("Squeezed : " + triggerValue);
            ArrowManager.Instance.AttachBowToArrow();
        }
    }
}
