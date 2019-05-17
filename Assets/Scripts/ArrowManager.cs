using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour
{
    public static ArrowManager Instance { get; private set; }

    [Range(1f, 100f)]
    [SerializeField]
    private float maxPower = 100f;

    [Range(0f, 1.5f)]
    [SerializeField]
    private float maxDist = 0.9f;


    [Header("Setup")]
    public SteamVR_Action_Single squeezeAction/* = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze")*/;
    [SerializeField]
    private GameObject arrowPrefab = default;
    [SerializeField]
    private GameObject stringAttachPointGO = default;
    [SerializeField]
    private Transform arrowStartPoint = default;
    [SerializeField]
    private Vector3 spawnPoint = new Vector3(0f, 0f, 0.34f);


    [SerializeField]
    private Transform stringStartPoint = default;


    public SteamVR_Behaviour_Pose trackedObject;

    // ---- INTERN ----
    private GameObject currentArrow;
    private bool isAttached = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        // todo modifier pour moins moche
        AttachArrow();
        PullString();
    }

    private void AttachArrow()
    {
        if(currentArrow == null)
        {
            // todo retirer fleche de la main dans le hierarchie
            //currentArrow = (GameObject)Instantiate(arrowPrefab, transform.localPosition, Quaternion.identity, /*trackedObject.transform*/ transform);
            currentArrow = (GameObject)Instantiate(arrowPrefab);

            currentArrow.transform.parent = trackedObject.transform;
            currentArrow.transform.localRotation = Quaternion.Euler(45, 0, 0);
            currentArrow.transform.localPosition = Vector3.zero;
        }
    }

    private void PullString()
    {
        if(isAttached)
        {
            float dist = Vector3.Distance(stringStartPoint.position, trackedObject.transform.position);
            Vector3 distancePlus = new Vector3((dist > maxDist ? maxDist : dist) * 10f, 0f, 0f);
            stringAttachPointGO.transform.localPosition = stringStartPoint.transform.localPosition + distancePlus;
            
            float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
            if (triggerValue <= 0.05f)
            {
                Fire();

                stringAttachPointGO.transform.position = stringStartPoint.position;
                currentArrow = null;
                isAttached = false;
            }
        }
    }

    private void Fire()
    {
        currentArrow.transform.parent = null;
        float dist = Vector3.Distance(stringStartPoint.position, trackedObject.transform.position);
        float power;
        if (dist > maxDist)
            power = maxPower;
        else
        {
            float distRatio = dist * 100f / maxDist;
            power = distRatio * maxPower / 100f;
        }

        Vector3 force = currentArrow.transform.forward * power;
        currentArrow.GetComponent<Arrow>().Fire(force);
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPointGO.transform;
        currentArrow.transform.localPosition = arrowStartPoint.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.rotation;

        isAttached = true;
    }
}
