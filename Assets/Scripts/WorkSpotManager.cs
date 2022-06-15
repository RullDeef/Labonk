using UnityEngine;

public class WorkSpotManager : MonoBehaviour
{
    public WorkSpot GetNearestWorkSpot(Vector3 position)
    {
        Transform nearest = transform.GetChild(0);
        float minSqrDistace = (nearest.position - position).sqrMagnitude;

        for (int i = 1; i < transform.childCount; i++)
        {
            Transform curr = transform.GetChild(i);
            float currSqrDistance = (curr.position - position).sqrMagnitude;

            if (currSqrDistance < minSqrDistace)
            {
                nearest = curr;
                minSqrDistace = currSqrDistance;
            }
        }

        return nearest.GetComponent<WorkSpot>();
    }
}
