using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Camera))]
public class Pointer : MonoBehaviour
{
    public static Camera Cam { get; private set; }

    [SerializeField]
    private float defaultLenght = 25f;

    [Header("Setup")]
    [SerializeField]
    private GameObject dotGO = default;
    [SerializeField]
    private VRInputModule inputModule = default;

    // ---- INTERN ----
    private LineRenderer lineRenderer = null;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Cam = GetComponent<Camera>();
    }

    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        PointerEventData data = inputModule.GetData();
        float targetLenght = data.pointerCurrentRaycast.distance == 0 ? defaultLenght : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLenght);

        Vector3 endPosition = transform.position + (transform.forward * targetLenght);

        if(hit.collider != null)
        {
            endPosition = hit.point;
        }
        dotGO.transform.position = endPosition;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float lenght)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, lenght);

        return hit;
    }
}
