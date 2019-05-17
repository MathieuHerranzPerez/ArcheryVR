using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam = default;
    [SerializeField]
    private float lookSensitivity = 4f;

    private Vector3 rotation;
    private Vector3 cameraRotation;

    private PlayerMotor motor;

    public GameObject arrowPrefab;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // calculate rotation to turn around
        float yRot = Input.GetAxisRaw("Mouse X");

        /* Vector3 */
        rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
        // apply rotation
        motor.Rotate(rotation);


        // ---- CAMERA ROTATION ----

        // calculate camera rotation to turn around
        float xRot = Input.GetAxisRaw("Mouse Y");

        /* Vector3 */
        cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;
        // apply camera rotation
        motor.RotateCamera(cameraRotation);


        if (Input.GetButtonDown("Fire1"))
        {
            GameObject arrowGO = (GameObject)Instantiate(arrowPrefab, transform.position, transform.rotation);
            Arrow arrow = arrowGO.GetComponent<Arrow>();
            arrow.Fire(40f * cam.transform.forward);
            Destroy(arrow, 10f);
        }
    }
}
