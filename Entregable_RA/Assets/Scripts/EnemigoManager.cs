using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoManager : MonoBehaviour
{
    public static EnemigoManager instancia;

    public int vidaActual = 5;
    public float tiempoAumento = 30f; // cada 30 segundos aumenta vida
    public int incrementoVida = 1;

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(AumentarVidaConTiempo());
    }

    IEnumerator AumentarVidaConTiempo()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoAumento);
            vidaActual += incrementoVida;
            Debug.Log("Vida de enemigos aumentada a: " + vidaActual);
        }
    }
}
