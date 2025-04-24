using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Spaen_Jugador : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ARRaycastManager raycastManager;

    private bool hasSpawned = false;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (!hasSpawned && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            if (raycastManager.Raycast(screenPosition, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                GameObject newPlayer = Instantiate(player, hitPose.position, hitPose.rotation);

                // Corregir altura si tiene collider
                Collider col = newPlayer.GetComponent<Collider>();
                if (col != null)
                {
                    Vector3 pos = newPlayer.transform.position;
                    pos.y += col.bounds.extents.y;
                    newPlayer.transform.position = pos;
                }

                hasSpawned = true;
            }
        }
    }
}
