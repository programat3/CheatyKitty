using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float followSpeed = 10f;
    public float pickupRadius = 0.4f; // Tama�o del collider al recoger
    private float normalRadius; // Tama�o normal del collider
    private bool isDragging = false;
    private Vector3 startPosition; // Posici�n inicial

    private CircleCollider2D circleCollider; // Referencia al collider

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        normalRadius = circleCollider.radius; // Guardar el tama�o original
        startPosition = transform.position; // Guardar posici�n inicial
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
        circleCollider.radius = pickupRadius; // Aumenta el tama�o del collider
    }

    void OnMouseUp()
    {
        isDragging = false;
        circleCollider.radius = normalRadius; // Vuelve al tama�o normal
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
            Debug.Log("�Chocaste con el camino! Reiniciando...");
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = startPosition; // Mueve la bola al inicio
        isDragging = false; // Detiene el seguimiento del mouse
        circleCollider.radius = normalRadius; // Restablece el tama�o del collider
    }
}