using Unity.Netcode;
using UnityEngine;

public class PlayerNetController : NetworkBehaviour
{

    override public void OnNetworkSpawn()
    {
        // var movementController = GetComponent<PlayerMovementController>();

        // if (!IsClient)
        //     movementController.enabled = false;
        // else
        // {
        //     FindObjectOfType<CameraController>().Target = movementController.transform;
        // }
    }
}
