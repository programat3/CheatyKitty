using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CardDrag : MonoBehaviour
{
    public GameObject zonaDestinoObjeto;
    public float moveDuration = 1f;

    private bool puedeMoverse = false; // Solo se moverá la carta que se haya seleccionado

    private void Start()
    {
        // Suscribirse al evento de MouseFollow
        MouseFollow.OnBolaTocoManga += ActivarMovimiento;
    }

    private void OnDestroy()
    {
        // Evitar problemas al destruir el objeto
        MouseFollow.OnBolaTocoManga -= ActivarMovimiento;
    }

    private void OnMouseDown()
    {
        // Al hacer clic en la carta, esta se vuelve la seleccionada para moverse
        puedeMoverse = true;
    }

    private void ActivarMovimiento()
    {
        if (puedeMoverse)
        {
            StartCoroutine(MoverCartaAlCentro());
        }
    }

    private IEnumerator MoverCartaAlCentro()
    {
        if (zonaDestinoObjeto == null) yield break;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = zonaDestinoObjeto.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Destroy(gameObject); // Destruye la carta al llegar
    }
}