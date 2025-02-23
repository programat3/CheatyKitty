using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagenesTutorial : MonoBehaviour
{
    [SerializeField] private GameObject menuAyuda;
    [SerializeField] private GameObject botonEntrar;
    [SerializeField] private GameObject botonSalir;


    public void AbrirImagenesObjetos()
    {
        Time.timeScale = 0f;
        Debug.Log("Juego pausado");
        menuAyuda.SetActive(true);
    }

   
    public void Reanudar()
    {
        Time.timeScale = 1f;
        Debug.Log("Juego reanudado");

        menuAyuda.SetActive(false);
    }

}
