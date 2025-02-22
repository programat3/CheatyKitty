using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static event System.Action OnTiempoTerminado; // 🔹 Evento agregado

    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;
    [SerializeField] private int vidas;
    [SerializeField] private int puntos;
    [SerializeField] private float tiempoMinimo = 3f;  // El tiempo mínimo que puede llegar a ser el temporizador
    [SerializeField] private float tiempoReduccionPor100Puntos = 0.5f; // Cuánto se reduce el tiempo por cada 100 puntos


    private int puntajeMaximo;
    private float tiempoActual;
    private bool tiempoActivado = false;

    public GameObject gameOverScreen;
    public GameObject temporizador;
    public GameObject path;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI puntajeMaximoText;

    public Transform[] spawnPoints;
    private Transform lastSpawnPoint;
    [SerializeField] private GameObject[] cardPrefabs;

    private void Start()
    {
        SoundManager.instance.PlayMusic(0, 1f, true);  // Reproduce el primer clip en bucle
        SoundManager.instance.PlayAdditionalMusic(3, 0.3f, true);  
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

            if (vidas > 0)
            {
                ActivarTemporizador();
            }
            else
            {
                CambiarTemporizador(false);
            }

            OnTiempoTerminado?.Invoke();

            temporizador.SetActive(false);
            path.SetActive(false);
            DesactivarTemporizador();
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

        // Reducir el tiempo según los puntos obtenidos
        ReducirTiempoPorPuntos();
    }

    private void ReducirTiempoPorPuntos()
    {
        // Cada 100 puntos se reduce el tiempo
        int cantidadReducciones = puntos / 100;

        // Asegurarse de que el tiempo no baje de 3 segundos
        tiempoMaximo = Mathf.Max(tiempoMinimo, tiempoMaximo - cantidadReducciones * tiempoReduccionPor100Puntos);
    }


    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }

    public void PerderVida()
    {
        vidas--;
        SoundManager.instance.PlayRandomSoundEffect("Dogs");
        ActualizarVidasUI();
        if (vidas <= 0)
        {
            SoundManager.instance.PlayRandomSoundEffect("Lose");
            GameOver();

        }
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
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnCard(spawnPoint);
        }
    }

    public void SpawnCard(Transform spawnPoint)
    {
        if (spawnPoint && cardPrefabs.Length > 0)
        {
            GameObject randomCardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Length)];
            Instantiate(randomCardPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
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
