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

    [Header("Sound")]
    [SerializeField]
    private AudioClip audioWhenFire = default;
    [SerializeField]
    private GameObject audioPlayerPrefab = default;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float volume = 0.6f;

    [HideInInspector]
    public int power = 1;

    // ---- INTERN ----
    private bool isAttached = false;
    private bool isFired = false;
    private Rigidbody rBody;

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

                GetComponent<BoxCollider>().enabled = false;
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

        // invoke another gameobject to play the sound
        GameObject soundGO = (GameObject)Instantiate(audioPlayerPrefab, transform.position, transform.rotation, transform);
        AudioPlayer _audioPlayer = soundGO.GetComponent<AudioPlayer>();
        _audioPlayer.Play(audioWhenFire, volume);
        Destroy(soundGO, 1.5f);
    }

    public void MultiplyPower(int power, ParticleSystem particlePrefab)
    {
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
