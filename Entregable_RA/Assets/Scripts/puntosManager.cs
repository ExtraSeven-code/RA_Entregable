using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puntosManager : MonoBehaviour
{
    public static puntosManager instancia;
    public int puntosTotales = 0;
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI puntos_totales;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SumarPuntos(int cantidad)
    {
        puntosTotales += cantidad;

        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntosTotales;
            puntos_totales.text = "Score: " + puntosTotales.ToString();


    }
}
