using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balas : MonoBehaviour
{
    public float velocidad = 10f;
    public int daño = 1;
    public float tiempoDeVida = 5f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            vida_enemigo enemigo = other.GetComponent<vida_enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(daño);
            }

            Destroy(gameObject);
        }
    }
}
