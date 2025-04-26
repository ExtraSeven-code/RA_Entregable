using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float rangoDeteccion = 10f;
    public Transform puntoDisparo;
    public GameObject balaPrefab;
    public int cargadorMaximo = 10;
    public float tiempoEntreDisparos = 0.2f;
    public float tiempoRecarga = 2f;

    private int balasRestantes;
    private bool puedeDisparar = true;
    private Transform enemigoObjetivo;
    public animacion animar_torreta;

    void Start()
    {
        balasRestantes = cargadorMaximo;
    }

    void Update()
    {
        BuscarEnemigo();
        if (enemigoObjetivo != null)
        {
            GirarHaciaEnemigo();
            if (puedeDisparar)
                StartCoroutine(Disparar());
        }
    }

    void BuscarEnemigo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        float menorDistancia = Mathf.Infinity;
        Transform enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < menorDistancia && distancia <= rangoDeteccion)
            {
                menorDistancia = distancia;
                enemigoCercano = enemigo.transform;
            }
        }

        enemigoObjetivo = enemigoCercano;
    }

    void GirarHaciaEnemigo()
    {
        Vector3 direccion = enemigoObjetivo.position - transform.position;
        direccion.y = 0; // opcional, para mantener la rotación en el eje Y solamente
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 5f);
    }

    IEnumerator Disparar()
    {
        puedeDisparar = false;
        animar_torreta.recibido = true;

        while (balasRestantes > 0)
        {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            balasRestantes--;
            yield return new WaitForSeconds(tiempoEntreDisparos);
        }
        animar_torreta.recibido = false;

        yield return new WaitForSeconds(tiempoRecarga);
        balasRestantes = cargadorMaximo;
        puedeDisparar = true;
    }
}

