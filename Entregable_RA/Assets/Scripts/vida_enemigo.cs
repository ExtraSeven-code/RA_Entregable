using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_enemigo : MonoBehaviour
{
    public int vida = 5;

    public void RecibirDa�o(int da�o)
    {
        vida -= da�o;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
