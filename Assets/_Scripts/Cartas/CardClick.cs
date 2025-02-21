using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClick : MonoBehaviour
{
    public GameObject path; // Objeto que representa el camino
    public GameObject temporizador; // Temporizador del juego
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>(); // Buscar GameController en la escena
    }

    void OnMouseDown()
    {
        if (path != null)
        {
            path.SetActive(true); // Activa el camino
        }

        if (temporizador != null)
        {
            temporizador.SetActive(true); // Activa el temporizador
        }

        if (gameController != null)
        {
            gameController.ActivarTemporizador(); // Inicia el temporizador
        }

        Debug.Log("Carta clickeada: Camino y temporizador activados.");
    }
}