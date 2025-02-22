using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;
    [SerializeField] private int vidas;
    [SerializeField] private int puntos;

    private int puntajeMaximo;

    private float tiempoActual;
    private bool tiempoActivado = false;

    public GameObject gameOverScreen;
    public GameObject temporizador;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI puntajeMaximoText;

    public Transform[] spawnPoints; // Ya no es necesario cardHolder
    private Transform lastSpawnPoint;
    [SerializeField] private GameObject[] cardPrefabs;

    private void Start()
    {
        puntajeMaximo = PlayerPrefs.GetInt("PuntajeMaximo", 0);
        ActualizarPuntajeMaximoUI();
        ActualizarVidasUI();
        ActualizarPuntosUI();
        InicializarCartas();
    }

    private void Update()
    {
        if (!tiempoActivado || (temporizador != null && !temporizador.activeSelf)) return;
        CambiarContador();
    }

    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual >= 0) slider.value = tiempoActual;
        if (tiempoActual <= 0)
        {
            PerderVida();
            if (vidas > 0) ActivarTemporizador();
            else CambiarTemporizador(false);
        }
    }

    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        CambiarTemporizador(true);
    }

    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }

    public void PerderVida()
    {
        vidas--;
        ActualizarVidasUI();
        if (vidas <= 0) GameOver();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        if (puntos > puntajeMaximo)
        {
            puntajeMaximo = puntos;
            ActualizarPuntajeMaximoUI();
        }
        ActualizarPuntosUI();
    }

    private void ActualizarVidasUI()
    {
        if (vidasText != null) vidasText.text = "" + vidas;
    }

    private void ActualizarPuntosUI()
    {
        if (puntosText != null) puntosText.text = "Score: " + puntos;
    }

    private void ActualizarPuntajeMaximoUI()
    {
        if (puntajeMaximoText != null)
            puntajeMaximoText.text = "Max. Score: " + puntajeMaximo;
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("PuntajeMaximo", puntajeMaximo);
        PlayerPrefs.Save();
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    private void InicializarCartas()
    {
        // Instanciamos las cartas en los puntos de spawn
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnCard(spawnPoint);
        }
    }

    public void SpawnCard(Transform spawnPoint)
    {
        if (spawnPoint && cardPrefabs.Length > 0)
        {
            // Seleccionamos aleatoriamente una carta del prefab
            GameObject randomCardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Length)];

            // Instanciamos la carta en la posición del spawnPoint
            GameObject cardInstance = Instantiate(randomCardPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
    }

    public void ReemplazarCarta(GameObject carta)
    {
        if (carta != null)
        {
            lastSpawnPoint = carta.transform;
            Destroy(carta);
            Invoke(nameof(SpawnNuevaCartaEnPosicion), 0.5f);
        }
    }

    private void SpawnNuevaCartaEnPosicion()
    {
        if (lastSpawnPoint != null)
        {
            SpawnCard(lastSpawnPoint);
        }
    }

    private void SpawnNuevaCarta()
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        SpawnCard(randomSpawn);
    }
}
