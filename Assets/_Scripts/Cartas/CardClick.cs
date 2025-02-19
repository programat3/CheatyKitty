using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClick : MonoBehaviour
{
    public GameObject targetObject; // El objeto que se activará/desactivará

    void OnMouseDown()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf); // Alterna el estado
            Debug.Log("Objeto " + targetObject.name + " ahora está " + (targetObject.activeSelf ? "activo" : "inactivo"));
        }
    }
}