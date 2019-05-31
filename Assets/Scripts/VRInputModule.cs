using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera cam;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject = null;
    private PointerEventData data = null;

    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        data.Reset();
        data.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(data, currentObject);

        if (clickAction.GetStateDown(targetSource))
            ProcessPress(data);

        if (clickAction.GetStateUp(targetSource))
            ProcessRelease(data);
    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData pData)
    {
        pData.pointerCurrentRaycast = pData.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, pData, ExecuteEvents.pointerDownHandler);

        if(newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        pData.pressPosition = pData.position;
        pData.pointerPress = newPointerPress;
        pData.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData pData)
    {
        ExecuteEvents.Execute(pData.pointerPress, pData, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(pData.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(pData.pointerPress, pData, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        pData.pressPosition = Vector2.zero;
        pData.pointerPress = null;
        pData.rawPointerPress = null;
    }
}
