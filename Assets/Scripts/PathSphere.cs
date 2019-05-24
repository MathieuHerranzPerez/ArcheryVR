using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSphere : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private List<Transform> listTargetPath = new List<Transform>();
    [SerializeField]
    private TargetPointSphere finalTarget = default;

    public Transform GetTarget(int index)
    {
        if (index >= listTargetPath.Count || index < 0)
            throw new System.Exception("incorrect index for the path");

        return listTargetPath[index];
    }

    public int GetPathLenght()
    {
        return listTargetPath.Count;
    }

    public TargetPointSphere GetLastPoint()
    {
        return finalTarget;
    }
}
