using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab de la carta
    public Transform spawnPoint; // Punto de generación de cartas

    void Start()
    {
        SpawnCard(); // Crear la primera carta al inicio
    }

    public void SpawnCard()
    {
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        newCard.GetComponent<CardDrag>().SetSpawner(this); // Pasamos la referencia del spawner
    }
}