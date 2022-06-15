using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Networking;

/**
 *  Любой предмет можно держать в руках только в 1 экземпляре кроме булок.
 *  Взять другой предмет можно только свободными руками.
 */
public class ItemController : NetworkBehaviour
{
    public PickableItem PickedItem { get; private set; }

    [SerializeField] private Transform holdingSpot;

    private HashSet<WorkSpot> nearWorkSpots = new();

    private void Start()
    {
        if (!IsLocalPlayer)
            DestroyImmediate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        WorkSpot spot = other.GetComponent<WorkSpot>();

        if (spot != null)
            nearWorkSpots.Add(spot);
    }

    private void OnTriggerExit(Collider other)
    {
        WorkSpot spot = other.GetComponent<WorkSpot>();

        if (spot != null)
            nearWorkSpots.Remove(spot);
    }

    public void OnGrab() // input action
    {
        PerformGrabActionServerRpc();
    }

    [ServerRpc]
    private void PerformGrabActionServerRpc()
    {
        if (PickedItem == null) // actual grab
        {
            Debug.Log("Grab action!");
            WorkSpot spot = GetNearestWorkSpot();
            if (spot != null && spot.HasNotebook)
            {
                Transform obj = spot.GrabNotebook();
                PickedItem = obj.GetComponent<PickableItem>();

                obj.parent = holdingSpot;
                obj.localPosition = Vector3.zero;
                obj.localRotation = Quaternion.identity;
            }
        }
        else // try to place
        {
            Debug.Log("Place action!");
            WorkSpot spot = GetNearestWorkSpot();
            if (spot != null && !spot.HasNotebook)
            {
                spot.PlaceNotebook(PickedItem.transform);
                PickedItem = null;
            }
        }
    }

    private WorkSpot GetNearestWorkSpot()
    {
        WorkSpot nearest = null;
        float distance = float.NaN;

        foreach (var spot in nearWorkSpots)
        {
            if (nearest == null)
            {
                nearest = spot;
                distance = (spot.transform.position - transform.position).sqrMagnitude;
                continue;
            }

            float currDistance = (spot.transform.position - transform.position).sqrMagnitude;
            
            if (currDistance < distance)
            {
                nearest = spot;
                distance = currDistance;
            }
        }

        return nearest;
    }
}
