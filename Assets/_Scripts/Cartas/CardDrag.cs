using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public GameObject self;
    private Vector3 originalPosition;
    private CardManager spawner; // Referencia al spawner

    void Start()
    {
        originalPosition = transform.position;
    }

    public void SetSpawner(CardManager spawnerRef)
    {
        spawner = spawnerRef; // Guardamos el spawner de origen
    }

    void OnMouseDown()
    {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0; // Mantener en el mismo plano 2D
            transform.position = newPosition;
            //Trigger MiniGame
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (!ValidDropZone())
        {
            StartCoroutine(ReturnToOriginalPosition()); // Movimiento suave de regreso
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        float elapsedTime = 0f;
        float duration = 0.3f; // Duración del regreso
        Vector3 startPos = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    bool ValidDropZone()
    {
        return transform.position.y > 0; // Ejemplo: Solo es válido si está arriba del centro de la pantalla
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vision")) // Verificar si choca con el área de visión
        {
            Debug.Log("Carta eliminada al entrar en el área de visión.");
            if (spawner != null)
            {
                spawner.SpawnCard(null); // Generar nueva carta antes de eliminarse
            }
            Destroy(gameObject); // Eliminar la carta
        }
    }

}