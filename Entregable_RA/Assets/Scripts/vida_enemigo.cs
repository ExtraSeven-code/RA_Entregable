using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_enemigo : MonoBehaviour
{
    public int vida = 5;
    private int puntos = 1;

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
}
