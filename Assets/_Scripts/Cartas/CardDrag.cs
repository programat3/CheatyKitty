using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CardDrag : MonoBehaviour
{
    public GameObject posicionIntermedia; // Primera posición a la que se moverá la carta
    public GameObject zonaDestinoObjeto;  // Segunda posición (destino final)
    public float moveDuration = 1f;

    private bool enIntermedia = false; // Para saber si la carta ya está en la posición intermedia
    private bool puedeMoverse = false; // Solo se moverá si fue seleccionada

    private void Start()
    {
        // Suscribirse al evento cuando la bola toca la manga
        MouseFollow.OnBolaTocoManga += MoverALaZonaDestino;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        MouseFollow.OnBolaTocoManga -= MoverALaZonaDestino;
    }

    private void OnMouseDown()
    {
        if (!enIntermedia && posicionIntermedia != null)
        {
            StartCoroutine(MoverCarta(posicionIntermedia.transform.position));
            enIntermedia = true; // Indica que la carta ya se movió a la posición intermedia
            puedeMoverse = true; // Marca la carta como seleccionada
        }
    }

    private void MoverALaZonaDestino()
    {
        if (puedeMoverse && enIntermedia && zonaDestinoObjeto != null)
        {
            StartCoroutine(MoverCarta(zonaDestinoObjeto.transform.position, true));
        }
    }

    private IEnumerator MoverCarta(Vector3 destino, bool destruirAlFinal = false)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, destino, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = destino;

        if (destruirAlFinal)
        {
            Destroy(gameObject); // Destruye la carta cuando llega al destino final
        }
    }
}