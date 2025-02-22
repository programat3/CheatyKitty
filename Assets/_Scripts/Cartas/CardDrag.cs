using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CardDrag : MonoBehaviour
{
    private bool isCardClicked = false;
    private GameObject cartaGuardada;
    public GameObject zonaDestinoObjeto; // Zona destino hacia donde la carta se mover�

    // Tiempo que tardar� en moverse la carta
    public float moveDuration = 1f;

    // Este booleano se establece cuando la carta choca con la manga
    private bool colisionoManga = false;

    private void Start()
    {
        // Inicializamos solo si es necesario
    }

    private void Update()
    {
        // Verificamos si colisionoManga es verdadero y si la zona destino est� asignada
        if (colisionoManga && zonaDestinoObjeto != null && cartaGuardada != null)
        {
            StartCoroutine(MoverCartaAlCentro(cartaGuardada)); // Pasamos la carta seleccionada
            colisionoManga = false; // Desactivamos la colisi�n despu�s de iniciar el movimiento
        }
    }

    private void OnMouseDown() // Cuando se hace clic en la carta
    {
        if (!isCardClicked)
        {
            isCardClicked = true;
            GuardarCartaDesdeSpawner(); // Guardamos la carta en el spawner donde se hace clic
        }
    }

    private void GuardarCartaDesdeSpawner()
    {
        // Detectamos el spawner donde se hace clic y guardamos la carta correspondiente
        Transform spawner = transform.parent; // Asumimos que la carta est� dentro del spawner
        cartaGuardada = spawner.GetComponentInChildren<Transform>().gameObject; // O buscamos el hijo que es la carta
    }

    private IEnumerator MoverCartaAlCentro(GameObject carta)
    {
        Vector3 startPosition = carta.transform.position; // Posici�n inicial de la carta
        Vector3 targetPosition = zonaDestinoObjeto.transform.position; // Posici�n de destino

        float elapsedTime = 0f; // Tiempo transcurrido en el movimiento

        // Movimiento Lerp
        while (elapsedTime < moveDuration)
        {
            carta.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null; // Espera hasta el siguiente frame
        }

        carta.transform.position = targetPosition; // Asegurarse de que llegue exactamente a la posici�n de destino
    }

    // M�todo que es llamado cuando la carta colisiona con la "Manga"
    public void OnColisionoConManga()
    {
        colisionoManga = true; // Activar el movimiento cuando se detecta la colisi�n con la manga
    }
}

