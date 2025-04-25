using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class Spawner_Torreta : MonoBehaviour
{
    [SerializeField] private GameObject Torreta;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject spawn_Torretas;
    [SerializeField] private float checkRadius;
    [SerializeField] private int costo_Torreta;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            if (raycastManager.Raycast(screenPosition, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;
                Collider[] colliders = Physics.OverlapSphere(hitPose.position, checkRadius);
                foreach (var col in colliders)
                {
                    if (col.CompareTag("Torreta"))
                    {
                        Debug.Log("Ya hay una torreta en esta posición.");
                        return;
                    }
                }

                // 💰 Verificamos si hay suficientes puntos
                if (puntosManager.instancia.puntosTotales >= costo_Torreta)
                {
                    GameObject newPlayer = Instantiate(Torreta, hitPose.position, hitPose.rotation);

                    Collider playerCollider = newPlayer.GetComponent<Collider>();
                    spawn_Torretas.SetActive(false);
                    if (playerCollider != null)
                    {
                        Vector3 pos = newPlayer.transform.position;
                        pos.y += playerCollider.bounds.extents.y;
                        newPlayer.transform.position = pos;
                    }

                    // 🧾 Restamos los puntos
                    puntosManager.instancia.SumarPuntos(-costo_Torreta);
                }
                else
                {
                    Debug.Log("No tienes suficientes puntos para colocar una torreta.");
                }
            }
        }
    }
}
