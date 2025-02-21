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

    public Transform cardHolder;
    public Transform[] spawnPoints;
    public GameObject cardPrefab;

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); Usar para borrar el tema de puntaje maximo

        puntajeMaximo = PlayerPrefs.GetInt("PuntajeMaximo", 0);  // Carga el puntaje máximo, 0 es el valor por defecto si no hay ninguno guardado
        ActualizarPuntajeMaximoUI();  // Actualiza la UI al inicio
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
            Debug.Log("Tiempo agotado, pierdes una vida.");
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
        Debug.Log("Vidas restantes: " + vidas);
        ActualizarVidasUI();
        if (vidas <= 0) GameOver();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        if (puntos > puntajeMaximo)
        {
            puntajeMaximo = puntos;
            Debug.Log("Nuevo puntaje máximo: " + puntajeMaximo);
            ActualizarPuntajeMaximoUI();  // Actualiza la UI con el nuevo puntaje máximo
        }
        Debug.Log("Puntos: " + puntos);
        ActualizarPuntosUI();
    }

    private void ActualizarVidasUI()
    {
        if (vidasText != null) vidasText.text = "Vidas: " + vidas;
    }

    private void ActualizarPuntosUI()
    {
        if (puntosText != null) puntosText.text = "Puntos: " + puntos;
    }

    private void ActualizarPuntajeMaximoUI()
    {
        if (puntajeMaximoText != null)
            puntajeMaximoText.text = "Puntaje máximo: " + puntajeMaximo;
    }

    private void GameOver()
    {
        Debug.Log("¡Game Over!");
        PlayerPrefs.SetInt("PuntajeMaximo", puntajeMaximo);  // Guarda el puntaje máximo
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
        if (spawnPoint)
        {
            GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, cardHolder);
        }
    }

    public void ReemplazarCarta(GameObject carta)
    {
        Destroy(carta);
        Invoke(nameof(SpawnNuevaCarta), 0.5f);
    }

    private void SpawnNuevaCarta()
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        SpawnCard(randomSpawn);
    }
}
