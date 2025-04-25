using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawner_Enemigo : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;      
    [SerializeField] private float spawnInterval;    
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private int enemigosPorPlano;
    [SerializeField] private int incrementoPorOleada;
    [SerializeField] private int maxEnemigosPorPlano;
    [SerializeField] private float tiempoAumentarCantidad;

    private bool isSpawning = true;  
    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(AumentarCantidadEnemigos());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (planeManager.trackables.count > 0)
            {
                foreach (var plane in planeManager.trackables)
                {
                    int enemigosAProducir = 1 + Mathf.FloorToInt(Time.time / tiempoAumentarCantidad);
                    enemigosAProducir = Mathf.Min(maxEnemigosPorPlano, enemigosAProducir);

                    for (int i = 0; i < enemigosAProducir; i++)
                    {
                        Vector3 spawnPosition = GetRandomPositionOnPlane(plane);
                        SpawnEnemy(spawnPosition);
                    }
                }
            }
        }
    }

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

    void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Collider col = newEnemy.GetComponent<Collider>();
        if (col != null)
        {
            Vector3 adjustedPosition = spawnPosition;
            adjustedPosition.y += col.bounds.extents.y;
            newEnemy.transform.position = adjustedPosition;
        }
    }
    IEnumerator AumentarCantidadEnemigos()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(tiempoAumentarCantidad);
            enemigosPorPlano = Mathf.Min(maxEnemigosPorPlano, enemigosPorPlano + incrementoPorOleada);
            Debug.Log("Ahora aparecen " + enemigosPorPlano + " enemigos por plano.");
        }
    }
}
