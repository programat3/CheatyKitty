using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject temporizador;


    public float followSpeed = 10f;
    public float pickupRadius = 0.4f; // Tamaño del collider al recoger
    private float normalRadius;
    private bool isDragging = false;
    private Vector3 startPosition; // Posición inicial

    private CircleCollider2D circleCollider;

    void Start()
    {

        circleCollider = GetComponent<CircleCollider2D>();
        normalRadius = circleCollider.radius;
        startPosition = transform.position;
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
        circleCollider.radius = pickupRadius;
    }

    void OnMouseUp()
    {
        isDragging = false;
        circleCollider.radius = normalRadius;
    }

    void FollowMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Path"))
        {
            Debug.Log("¡Chocaste con el camino! Reiniciando...");
            ResetBall();
        }
        else if (collision.CompareTag("Manga"))
        {
            Debug.Log("¡Escondiste la tarjeta!");
            
            if (path != null) path.SetActive(false);
            if (temporizador != null) temporizador.SetActive(false);

            // Buscar el GameController y detener el temporizador
            GameController gameController = FindObjectOfType<GameController>();
            gameController.SumarPuntos(100);
            if (gameController != null)
            {
                gameController.DesactivarTemporizador();
            }

            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = startPosition; // Mueve la bola al inicio
        isDragging = false;
        circleCollider.radius = normalRadius;
    }
}