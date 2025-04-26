using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_controller : MonoBehaviour
{
    [SerializeField] private GameObject perdiste;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject inicio;
    [SerializeField] private GameObject spawn_enemigos;
    public void Menu()
    {
        menu.SetActive(true);
    }
    public void juego()
    {
        menu.SetActive(false);
        inicio.SetActive(true);
    }
    public void Perdiste()
    {
        perdiste.SetActive(true);
        spawn_enemigos.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
