using UnityEngine;

public class WorkSpot : MonoBehaviour
{
    [SerializeField] private Transform notebookRef;

    public bool HasNotebook
    {
        get
        {
            return notebookRef != null;
        }
    }

    public void PlaceNotebook(Transform notebook)
    {
        if (HasNotebook)
        {
            Debug.LogError("notebook alredy exists in work spot");
        }

        notebookRef = notebook;

        notebook.parent = transform;
        notebook.localPosition = Vector3.zero;
        notebook.localRotation = Quaternion.identity;
    }

    public Transform GrabNotebook()
    {
        if (!HasNotebook)
        {
            Debug.LogError("notebook does not exists in work spot");
        }

        Transform res = notebookRef;
        notebookRef = null;
        return res;
    }
}
