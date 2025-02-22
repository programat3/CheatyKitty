using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Carta : MonoBehaviour
{
    /*
    public Sprite[] sprites; // Array de sprites para diferentes estéticas de la carta
    private int currentSpriteIndex = 0; // Índice del sprite actual

    public Transform targetObject; // Objeto de destino (ahora es un Transform)

    private void Start()
    {
        // Inicializa la carta con el primer sprite del array
        CambiarSprite(0); // Puedes inicializar con el primer sprite si lo deseas
    }

    private void Update()
    {
        
        // Si la carta tiene un destino, mueve la carta hacia ese destino
        if (targetObject != null)
        {
            MoveCardToTarget();
        }
        
    }

    private void OnMouseDown()
    {
        // Detecta clic en la carta y cambia el sprite de la carta
        // Puedes cambiarlo a otro sprite por estética o acción.
        CambiarSprite((currentSpriteIndex + 1) % sprites.Length); // Cambia al siguiente sprite de manera cíclica
    }

    // Cambia el sprite de la carta basado en el índice
    private void CambiarSprite(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index];
            currentSpriteIndex = index;
        }
    }

    // Mueve la carta hacia el objeto de destino
    public void MoveCardToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, Time.deltaTime * 5); // Velocidad de movimiento ajustable
    }

    // Establece el objeto de destino de la carta
    public void SetTargetObject(Transform newTarget)
    {
        targetObject = newTarget;

    }
*/
}