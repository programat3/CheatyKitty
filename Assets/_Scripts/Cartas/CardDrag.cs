using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    private bool isCardClicked = false;
    private GameObject cartaGuardada;

    void OnMouseDown() // Cuando se hace clic en la carta
    {
        if (!isCardClicked)
        {
            isCardClicked = true;
            GuardarCartaTemporalmente(); // Guardamos la carta en una "zona temporal"
        }
    }

    void GuardarCartaTemporalmente()
    {
        cartaGuardada = this.gameObject; // Guardamos la carta que se ha clickeado
        cartaGuardada.SetActive(false);  // La desactivamos para que no esté visible

        // Aquí podrías agregar una animación, como que la carta se desplace a una zona de espera
    }
}