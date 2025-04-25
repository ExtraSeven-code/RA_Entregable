using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class vida_enemigo : MonoBehaviour
{
    private int vida;
    private int puntos = 1;
    [SerializeField]private TextMeshProUGUI lavida_enemigo;

    void Start()
    {

        vida = EnemigoManager.instancia.vidaActual;
    }
    private void Update()
    {
        lavida_enemigo.text = vida.ToString();
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
