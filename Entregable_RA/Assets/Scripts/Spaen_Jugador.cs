using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaen_Jugador : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public event System.Action<GameObject> characterSpawned;

    private bool hasSpawned = false; // ← bandera para evitar múltiples spawns

    void Update()
    {
        if (!hasSpawned && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 spawnPosition = GetTouchPositionInWorld();
            if (spawnPosition != Vector3.zero)
            {
                SpawnCharacter(spawnPosition);
                hasSpawned = true; // ← ya fue instanciado, no permitir más
            }
        }
    }

    Vector3 GetTouchPositionInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    void SpawnCharacter(Vector3 spawnPosition)
    {
        GameObject newCharacter = Instantiate(player, spawnPosition, player.transform.rotation);


        newCharacter.transform.position = spawnPosition;


        characterSpawned?.Invoke(newCharacter);
    }
}
