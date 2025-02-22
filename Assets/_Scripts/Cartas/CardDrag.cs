using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    public GameObject posicionIntermedia;  // Primera posici�n
    public GameObject zonaDestinoObjeto;   // Destino final
    public float moveDuration = 1f;
    private Vector3 posicionInicial;       // Guarda la posici�n inicial
    public GameObject[] cartaPrefabs;
    private bool enIntermedia = false;
    private bool puedeMoverse = false;
    private bool tiempoTerminado = false;

    private void Start()
    {
        posicionInicial = transform.position; // Guarda la posici�n original
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
            SoundManager.instance.PlayRandomSoundEffect("Move");
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
        if (cartaPrefabs.Length > 0)
        {
            // Usamos la posici�n inicial guardada antes del movimiento
            Vector3 posicionDondeEstaba = posicionInicial;

            // Elegir un prefab aleatorio y generarlo en la posici�n donde estaba la carta
            GameObject cartaRandom = cartaPrefabs[Random.Range(0, cartaPrefabs.Length)];
            Instantiate(cartaRandom, posicionDondeEstaba, Quaternion.identity);
            SoundManager.instance.PlayRandomSoundEffect("Cat");
            SoundManager.instance.PlayRandomSoundEffect("Card");
        }
    }

    private void ResetearCarta()
    {
        tiempoTerminado = false;  // Reiniciar el estado
        enIntermedia = false;     // Reiniciar la posici�n de la carta
        puedeMoverse = false;     // Habilitar nuevamente el movimiento de la carta
        SoundManager.instance.PlayRandomSoundEffect("Devolver");
        StartCoroutine(MoverCarta(posicionInicial));
    }
}