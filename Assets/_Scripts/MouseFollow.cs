using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float followSpeed = 10f; // Velocidad con la que sigue el mouse
    private bool canFollow = false; // Solo sigue después del primer clic
    private Vector3 startPosition; // Guarda la posición inicial

    void Start()
    {
        startPosition = transform.position; // Guarda la posición inicial
        Cursor.lockState = CursorLockMode.Confined; // Mantiene el cursor dentro de la ventana
        Cursor.visible = true; // Hace visible el cursor
    }

    void Update()
    {
        if (canFollow)
        {
            FollowMousePosition();
        }
    }

    void OnMouseDown() // Cuando el jugador hace clic en la bola, empieza a seguir el mouse
    {
        canFollow = true;
    }

    void FollowMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurar que se mantenga en el plano 2D
        transform.position = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Path"))
        {
            Debug.Log("¡Chocaste con el camino! Reiniciando...");
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = startPosition; // Mueve la bola al inicio
        canFollow = false; // Detiene el seguimiento del mouse
    }
}