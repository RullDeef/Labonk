using System;
using UnityEngine;

public class NotebookCarrier : MonoBehaviour
{
    public enum State
    {
        NOTEBOOK_GRABBED,
        NOTEBOOK_GRABBING,
        NOTEBOOK_PLACED,
        NOTEBOOK_PLACING
    }

    public State CurrentState { get; private set; } = State.NOTEBOOK_PLACED;

    [SerializeField] private float maxGrabDistance = 2.0f;
    [SerializeField] private float maxPlaceDistance = 2.0f;
    [SerializeField] private float grabbingTime = 0.5f;
    [SerializeField] private float placingTime = 0.5f;

    [SerializeField] private WorkSpotManager workSpotManager;

    private Transform pickedNotebook;
    private WorkSpot pickedSpot;

    public Action<Transform> OnNotebookGrabbed;

    public void GrabNearest()
    {
        if (CurrentState == State.NOTEBOOK_PLACED)
        {
            WorkSpot spot = GetNearestSpotWithDistance(out float distance);
            if (distance < maxGrabDistance && spot.HasNotebook)
            {
                CurrentState = State.NOTEBOOK_GRABBING;
                pickedNotebook = spot.GrabNotebook();
                pickedSpot = spot;
                Invoke("FinishGrabbing", grabbingTime);
            }
            else
            {
                Debug.Log("conditions did not met! :( distance was: " + distance);
            }
        }
        else
        {
            Debug.Log("invalid state for grab: " + CurrentState);
        }
    }
    
    public void PlaceNearby(Transform notebook)
    {
        if (CurrentState == State.NOTEBOOK_GRABBED)
        {
            WorkSpot spot = GetNearestSpotWithDistance(out float distance);
            if (distance < maxPlaceDistance && !spot.HasNotebook)
            {
                CurrentState = State.NOTEBOOK_PLACING;
                pickedNotebook = notebook;
                pickedSpot = spot;
                Invoke("FinishPlacing", placingTime);
            }
            else
            {
                Debug.Log("conditions did not met! :( distance was: " + distance);
            }
        }
        else
        {
            Debug.Log("invalid state for put: " + CurrentState);
        }
    }

    public bool CanGrab
    {
        get
        {
            if (CurrentState == State.NOTEBOOK_PLACED)
            {
                WorkSpot spot = GetNearestSpotWithDistance(out float distance);
                if (distance < maxGrabDistance && spot.HasNotebook)
                    return true;
            }
            return false;
        }
    }

    public bool CanPlace
    {
        get
        {
            if (CurrentState == State.NOTEBOOK_GRABBED)
            {
                WorkSpot spot = GetNearestSpotWithDistance(out float distance);
                if (distance < maxPlaceDistance && !spot.HasNotebook)
                    return true;
            }
            return false;
        }
    }

    public bool IsInProcess => CurrentState == State.NOTEBOOK_GRABBING
        || CurrentState == State.NOTEBOOK_PLACING;

    private void FinishGrabbing()
    {
        CurrentState = State.NOTEBOOK_GRABBED;

        OnNotebookGrabbed.Invoke(pickedNotebook);
        pickedNotebook = null;
        pickedSpot = null;
    }

    private void FinishPlacing()
    {
        CurrentState = State.NOTEBOOK_PLACED;

        pickedSpot.PlaceNotebook(pickedNotebook);
        pickedNotebook = null;
        pickedSpot = null;
    }

    private WorkSpot GetNearestSpotWithDistance(out float distance)
    {
        WorkSpot spot = workSpotManager.GetNearestWorkSpot(transform.position);
        distance = (spot.transform.position - transform.position).magnitude;
        return spot;
    }
}
