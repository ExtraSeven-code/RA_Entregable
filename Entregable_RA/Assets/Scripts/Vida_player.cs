using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;


public class Vida_player : MonoBehaviour
{
    [SerializeField] private int vida_player;
    [SerializeField] private TextMeshProUGUI vida_vista;
    public Menu_controller controller_menu;


    // Start is called before the first frame update
    void Start()
    {
        controller_menu = FindObjectOfType<Menu_controller>();

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
                EliminarObjetosPorEtiqueta("Enemy");
                EliminarObjetosPorEtiqueta("Torreta");
                Destroy(gameObject);
                controller_menu.Perdiste();

            }
        }
    }
    void EliminarObjetosPorEtiqueta(string etiqueta)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(etiqueta);
        foreach (GameObject obj in objetos)
        {
            Destroy(obj);
        }
    }
}
