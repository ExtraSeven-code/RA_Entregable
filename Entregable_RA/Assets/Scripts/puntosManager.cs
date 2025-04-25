using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puntosManager : MonoBehaviour
{
    public static puntosManager instancia;
    public int puntosTotales = 0;
    public TextMeshProUGUI textoPuntos; 

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
    }
}
