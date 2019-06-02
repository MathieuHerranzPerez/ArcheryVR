using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; }

    private Camera cam;

    void Awake()
    {
        Instance = this;
        cam = GetComponent<Camera>();
    }

    public Camera GetCamera()
    {
        return cam;
    }
}
