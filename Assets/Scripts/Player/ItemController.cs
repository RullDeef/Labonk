using System;
using UnityEngine;

/**
 *  Любой предмет можно держать в руках только в 1 экземпляре кроме булок.
 *  Взять другой предмет можно только свободными руками.
 */
public class ItemController : MonoBehaviour
{
    // public PickableItem PickedItem { get; private set; }

    // public Action<PickableItem> OnItemGrabbed;

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
        
    // }

    // public void Grab()
    // {
    //     if (PickedItem != null)
    //     {
    //         WorkSpot spot = GetNearestSpotWithDistance(out float distance);
    //         if (distance < maxGrabDistance && spot.HasNotebook)
    //         {
    //             CurrentState = State.NOTEBOOK_GRABBING;
    //             pickedNotebook = spot.GrabNotebook();
    //             pickedSpot = spot;
    //             Invoke("FinishGrabbing", grabbingTime);
    //         }
    //         else
    //         {
    //             Debug.Log("conditions did not met! :( distance was: " + distance);
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("invalid state for grab: " + CurrentState);
    //     }
    // }
    
    // public void PlaceNearby(Transform notebook)
    // {
    //     if (CurrentState == State.NOTEBOOK_GRABBED)
    //     {
    //         WorkSpot spot = GetNearestSpotWithDistance(out float distance);
    //         if (distance < maxPlaceDistance && !spot.HasNotebook)
    //         {
    //             CurrentState = State.NOTEBOOK_PLACING;
    //             pickedNotebook = notebook;
    //             pickedSpot = spot;
    //             Invoke("FinishPlacing", placingTime);
    //         }
    //         else
    //         {
    //             Debug.Log("conditions did not met! :( distance was: " + distance);
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("invalid state for put: " + CurrentState);
    //     }
    // }

    // public bool CanGrab
    // {
    //     get
    //     {
    //         if (CurrentState == State.NOTEBOOK_PLACED)
    //         {
    //             WorkSpot spot = GetNearestSpotWithDistance(out float distance);
    //             if (distance < maxGrabDistance && spot.HasNotebook)
    //                 return true;
    //         }
    //         return false;
    //     }
    // }

    // public bool CanPlace
    // {
    //     get
    //     {
    //         if (CurrentState == State.NOTEBOOK_GRABBED)
    //         {
    //             WorkSpot spot = GetNearestSpotWithDistance(out float distance);
    //             if (distance < maxPlaceDistance && !spot.HasNotebook)
    //                 return true;
    //         }
    //         return false;
    //     }
    // }

    // public bool IsInProcess => CurrentState == State.NOTEBOOK_GRABBING
    //     || CurrentState == State.NOTEBOOK_PLACING;

    // private void FinishGrabbing()
    // {
    //     CurrentState = State.NOTEBOOK_GRABBED;

    //     OnNotebookGrabbed.Invoke(pickedNotebook);
    //     pickedNotebook = null;
    //     pickedSpot = null;
    // }

    // private void FinishPlacing()
    // {
    //     CurrentState = State.NOTEBOOK_PLACED;

    //     pickedSpot.PlaceNotebook(pickedNotebook);
    //     pickedNotebook = null;
    //     pickedSpot = null;
    // }

    // private WorkSpot GetNearestSpotWithDistance(out float distance)
    // {
    //     WorkSpot spot = workSpotManager.GetNearestWorkSpot(transform.position);
    //     distance = (spot.transform.position - transform.position).magnitude;
    //     return spot;
    // }
}
