using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vida_player : MonoBehaviour
{
    [SerializeField] private int vida_player;
    [SerializeField] private TextMeshProUGUI vida_vista;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vida_vista.text = vida_player.ToString();
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
