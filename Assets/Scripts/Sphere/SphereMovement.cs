using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private float delta = 0.22f;

    // ---- INTERN
    private Sphere sphere;
    private float speed;

    private PathSphere path;
    private int pathIndex = 0;
    private Transform target;
    private TargetPointSphere lastPoint;
    private bool isLastPoint = false;

    public void Init(PathSphere path, float speed)
    {
        sphere = GetComponent<Sphere>();
        
        this.speed = speed;
        this.path = path;

        target = path.GetTarget(pathIndex); // start the movement
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;  // get the direction of the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;  // rotate

            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);        // move

            // the shpere has reached the point
            if (!isLastPoint && Vector3.Distance(transform.position, target.transform.position) <= delta)
            {
                GetNextPoint();
            }
            else if(isLastPoint && Vector3.Distance(transform.position, target.transform.position) <= lastPoint.GetRangeToExplose())
            {
                sphere.Explode();
            }
        }
    }

    private void GetNextPoint()
    {
        ++pathIndex;
        if (pathIndex >= path.GetPathLenght())
        {
            isLastPoint = true;
            lastPoint = path.GetLastPoint();
            target = lastPoint.transform;
        }
        else
        {
            target = path.GetTarget(pathIndex);
        }
    }
}
