using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    public GameObject posicionIntermedia;  // Primera posición
    public GameObject zonaDestinoObjeto;   // Destino final
    public float moveDuration = 1f;
    public GameObject cartaPrefab;         // Prefab para reemplazo
    private Vector3 posicionInicial;       // Guarda la posición inicial

    private bool enIntermedia = false;
    private bool puedeMoverse = false;
    private bool tiempoTerminado = false;

    private void Start()
    {
        posicionInicial = transform.position; // Guarda la posición original
        MouseFollow.OnBolaTocoManga += MoverALaZonaDestino;
        GameController.OnTiempoTerminado += ResetearCarta;
    }

    private void OnDestroy()
    {
        MouseFollow.OnBolaTocoManga -= MoverALaZonaDestino;
        GameController.OnTiempoTerminado -= ResetearCarta;
    }

    private void OnMouseDown()
    {
        if (!enIntermedia && posicionIntermedia != null && !tiempoTerminado)
        {
            StartCoroutine(MoverCarta(posicionIntermedia.transform.position));
            enIntermedia = true;
            puedeMoverse = true;
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
            ReemplazarCarta();
            Destroy(gameObject);
        }
    }

    private void ReemplazarCarta()
    {
        if (cartaPrefab != null)
        {
            Instantiate(cartaPrefab, posicionInicial, Quaternion.identity);
        }
    }

    private void ResetearCarta()
    {
        tiempoTerminado = false;  // Reiniciar el estado
        enIntermedia = false;     // Reiniciar la posición de la carta
        puedeMoverse = false;     // Habilitar nuevamente el movimiento de la carta
        StartCoroutine(MoverCarta(posicionInicial));
    }
}