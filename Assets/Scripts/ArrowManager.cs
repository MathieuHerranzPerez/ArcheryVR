using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour
{
    public static ArrowManager Instance { get; private set; }

    [Header("Setup")]
    [SerializeField]
    private GameObject arrowPrefab = default;
    [SerializeField]
    private Transform stringAttachTransform;
    [SerializeField]
    private Transform arrowStartPoint = default;
    [SerializeField]
    private Vector3 spawnPoint = new Vector3(0f, 0f, 0.34f);


    public SteamVR_Behaviour_Pose trackedObject;

    // ---- INTERN ----
    private GameObject currentArrow;

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
    }

    private void AttachArrow()
    {
        if(currentArrow == null)
        {
            // todo retirer fleche de la main dans le hierarchie
            currentArrow = (GameObject)Instantiate(arrowPrefab, transform.position, transform.rotation, trackedObject.transform);
            currentArrow.transform.localPosition = spawnPoint;
        }
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachTransform;
        currentArrow.transform.localPosition = arrowStartPoint.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.rotation;
    }
}
