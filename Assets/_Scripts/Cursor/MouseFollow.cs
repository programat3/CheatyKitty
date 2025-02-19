using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float followSpeed = 10f;
    public float pickupRadius = 0.4f; // Tamaño del collider al recoger
    private float normalRadius; // Tamaño normal del collider
    private bool isDragging = false;
    private Vector3 startPosition; // Posición inicial

    private CircleCollider2D circleCollider; // Referencia al collider

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        normalRadius = circleCollider.radius; // Guardar el tamaño original
        startPosition = transform.position; // Guardar posición inicial
    }

    void Update()
    {
        if (isDragging)
        {
            FollowMousePosition();
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        circleCollider.radius = pickupRadius; // Aumenta el tamaño del collider
    }

    void OnMouseUp()
    {
        isDragging = false;
        circleCollider.radius = normalRadius; // Vuelve al tamaño normal
    }

    void FollowMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Path")) // Si toca "Path", reinicia la bola
        {
            Debug.Log("¡Chocaste con el camino! Reiniciando...");
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = startPosition; // Mueve la bola al inicio
        isDragging = false; // Detiene el seguimiento del mouse
        circleCollider.radius = normalRadius; // Restablece el tamaño del collider
    }
}