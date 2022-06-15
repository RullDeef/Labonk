using System.Collections.Generic;
using UnityEngine;

public class NavPathHolder : MonoBehaviour
{
    public List<Vector3> GetPath()
    {
        List<Vector3> path = new();

        for (int i = 0; i < transform.childCount; i++)
            path.Add(transform.GetChild(i).position);

        return path;
    }
}
