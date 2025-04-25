using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Spawner_Torreta : MonoBehaviour
{
    [SerializeField] private GameObject Torreta;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject spawn_Torretas;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            if (raycastManager.Raycast(screenPosition, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;


                GameObject newPlayer = Instantiate(Torreta, hitPose.position, hitPose.rotation);

                Collider col = newPlayer.GetComponent<Collider>();
                spawn_Torretas.SetActive(false);
                if (col != null)
                {
                    Vector3 pos = newPlayer.transform.position;
                    pos.y += col.bounds.extents.y;
                    newPlayer.transform.position = pos;
                }

            }
        }
    }
}
