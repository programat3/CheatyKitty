using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    private void Start()
    {
        SoundManager.instance.PlayMusic(1, 1f, false);  // Reproduce el primer clip en bucle
        SoundManager.instance.PlayMusic(2, 1f, true);
    }
    public void CargarEscena(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void CargarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }


    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
