using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_enemigo : MonoBehaviour
{
    public int vida = 5;

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
