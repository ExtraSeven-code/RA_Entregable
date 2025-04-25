using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_player : MonoBehaviour
{
    [SerializeField] private int vida_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            vida_player -= 1;
            if (vida_player == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
