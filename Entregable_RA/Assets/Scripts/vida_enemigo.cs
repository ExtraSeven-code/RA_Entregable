using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_enemigo : MonoBehaviour
{
    private int vida = 5;
    private int puntos = 1;

    void Start()
    {
        vida = EnemigoManager.instancia.vidaActual;
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            morir();
        }
    }
    public void morir()
    {
        puntosManager.instancia.SumarPuntos(puntos);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
