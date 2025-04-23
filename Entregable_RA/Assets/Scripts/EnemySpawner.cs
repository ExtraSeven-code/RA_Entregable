using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;      // Prefab del enemigo
    [SerializeField] private float spawnInterval = 2f;    // Intervalo entre cada spawn (en segundos)
    [SerializeField] private ARPlaneManager planeManager;  // ARPlaneManager para obtener los planos

    private bool isSpawning = true;  // Bandera para controlar el spawn

    void Start()
    {
        // Llama al método para empezar a spawnear enemigos
        StartCoroutine(SpawnEnemiesRoutine());
    }

    // Coroutine para spawnear enemigos de forma continua
    IEnumerator SpawnEnemiesRoutine()
    {
        while (isSpawning)
        {
            // Esperar el intervalo de tiempo antes de spawnear el siguiente enemigo
            yield return new WaitForSeconds(spawnInterval);

            // Solo spawn si hay planos disponibles
            if (planeManager.trackables.count > 0)
            {
                ARPlane plane = GetRandomPlane();
                if (plane != null)
                {
                    Vector3 spawnPosition = GetRandomPositionOnPlane(plane);
                    SpawnEnemy(spawnPosition);
                }
            }
        }
    }

    // Función para obtener un plano aleatorio de los planos detectados
    ARPlane GetRandomPlane()
    {
        int index = Random.Range(0, planeManager.trackables.count);
        int i = 0;
        foreach (var plane in planeManager.trackables)
        {
            if (i == index)
                return plane;
            i++;
        }
        return null;
    }

    // Función para obtener una posición aleatoria en el plano seleccionado
    Vector3 GetRandomPositionOnPlane(ARPlane plane)
    {
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        var triangles = mesh.triangles;
        var vertices = mesh.vertices;

        if (triangles.Length < 3)
            return plane.center;

        int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;

        Vector3 point = RandomInTriangle(
            vertices[triangles[triangleIndex]],
            vertices[triangles[triangleIndex + 1]]
        );

        return plane.transform.TransformPoint(point);
    }

    // Genera un punto aleatorio dentro de un triángulo (para la geometría del plano)
    Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0f, 1f);
        float v = Random.Range(0f, 1f);
        if (v + u > 1f)
        {
            u = 1f - u;
            v = 1f - v;
        }

        return (v1 * u) + (v2 * v);
    }

    // Método para spawnear el enemigo en la posición especificada
    void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Collider col = newEnemy.GetComponent<Collider>();
        if (col != null)
        {
            // Ajusta la posición del enemigo para que no se quede bajo el plano
            Vector3 adjustedPosition = spawnPosition;
            adjustedPosition.y += col.bounds.extents.y;
            newEnemy.transform.position = adjustedPosition;
        }
    }
}
