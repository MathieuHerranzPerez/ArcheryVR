using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Arrow : MonoBehaviour
{
    [Header("Setup")]
    public SteamVR_Action_Single squeezeAction = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

    [SerializeField]
    private GameObject effectGO = default;
    [SerializeField]
    private Transform firePoint = default;

    [HideInInspector]
    public int power = 1;

    // ---- INTERN ----
    private bool isAttached = false;
    private bool isFired = false;
    private Rigidbody rBody;
    private int nbTriggerActive = 0;

    private Vector3 positionBase;
    private Quaternion rotationBase;

    private void Start()
    {
        Init();
        positionBase = transform.localPosition;
        rotationBase = transform.localRotation;
    }

    void Update()
    {
        if (isFired)
            FollowGravity();
        /*else if(!isAttached)
        {
            transform.localPosition = positionBase;
            transform.localRotation = rotationBase;
        }*/
    }
    /*
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bow")
        {
            Debug.Log("IN");
            if (nbTriggerActive >= 2)
                AttachArrow();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("GGGGGGGGG");
        if (other.gameObject.tag == "Bow")
        {
            Debug.Log("IN");
            ++nbTriggerActive;
            if (nbTriggerActive >= 2)
                AttachArrow();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bow")
        {
            Debug.Log("IN");
            --nbTriggerActive;
        }
    }*/

    void OnCollisionEnter(Collision other)
    {
        if (isFired)
        {
            if (other.transform.tag == "Ground")
            {
                // freeze the arrow
                rBody.velocity = Vector3.zero;
                rBody.useGravity = false;

                transform.parent = other.transform;
            }

            Destroy(gameObject, 3f);
        }
    }

    public void Fire(Vector3 force)
    {
        GetComponent<BoxCollider>().enabled = true;
        isFired = true;
        if (rBody == null)
            Init();
        rBody.velocity = force;
        rBody.useGravity = true;

        effectGO.SetActive(true);
    }

    public void MultiplyPower(int power, ParticleSystem particlePrefab)
    {
        Debug.Log("Multiply : " + power);
        this.power *= power;
        this.power = this.power > 999 ? 999 : this.power;
        Instantiate(particlePrefab, firePoint);
    }

    private void FollowGravity()
    {
        transform.LookAt(transform.position + rBody.velocity);
    }

    //https://www.youtube.com/watch?v=bn8eMxBcI70
    public bool TryAttachArrow()
    {
        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
        if(!isAttached && triggerValue > 0.05f)
        {
            isAttached = true;
            ArrowManager.Instance.AttachBowToArrow();
            return true;
        }
        return false;
    }

    private void Init()
    {
        rBody = GetComponent<Rigidbody>();
    }
}
