using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour
{
    public static ArrowManager Instance { get; private set; }

    [Header("Setup")]
    public SteamVR_Action_Single squeezeAction;
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
            currentArrow = (GameObject)Instantiate(arrowPrefab, transform.position, Quaternion.identity, trackedObject.transform);
            currentArrow.transform.localPosition = spawnPoint;
        }
    }

    private void PullString()
    {
        if(isAttached)
        {
            float dist = Vector3.Distance(stringStartPoint.position, trackedObject.transform.position);
            stringAttachPointGO.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);

            float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
            if (triggerValue <= 0.05f)
            {
                Debug.Log("LOOSE : " + triggerValue);
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

        Vector3 force = currentArrow.transform.forward * 10f;
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
