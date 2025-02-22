using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // Instancia est�tica para acceder al AudioManager desde cualquier parte del c�digo

    public AudioSource musicSource;       // Fuente de audio para la m�sica de fondo
    public AudioSource additionalMusicSource;  // Fuente de audio para m�sica adicional
    public AudioSource effectsSource;     // Fuente de audio para los efectos de sonido

    public AudioClip[] musicClips;        // Clips de m�sica para cambiar entre diferentes pistas

    [System.Serializable]
    public class SoundCategory
    {
        public string categoryName;
        public AudioClip[] clips;
    }

    public SoundCategory[] soundEffects;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;  // Asigna esta instancia como la instancia global del AudioManager
        }
        else
        {
            Destroy(gameObject);  // Si ya existe, destruye el objeto duplicado
        }

        DontDestroyOnLoad(gameObject);  // No destruir el AudioManager al cambiar de escenas
    }

    // Reproducir m�sica de fondo con volumen personalizado
    public void PlayMusic(int clipIndex, float volume, bool loop)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Length)
        {
            musicSource.clip = musicClips[clipIndex];
            musicSource.volume = volume;
            musicSource.loop = loop;  // Define si debe ser loop o no
            musicSource.Play();
        }
    }
    public void PlayAdditionalMusic(int clipIndex, float volume, bool loop)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Length)
        {
            additionalMusicSource.clip = musicClips[clipIndex];
            musicSource.volume = volume;
            additionalMusicSource.loop = loop;
            additionalMusicSource.Play();
        }
    }

    // Reproducir un efecto de sonido aleatorio de una categor�a con volumen personalizado
    public void PlayRandomSoundEffect(string categoryName, float volume = 1f)
    {
        // Buscar la categor�a por su nombre
        SoundCategory category = System.Array.Find(soundEffects, s => s.categoryName == categoryName);

        if (category != null && category.clips.Length > 0)
        {
            // Escoge un clip aleatorio de la categor�a de efectos de sonido
            int randomIndex = Random.Range(0, category.clips.Length);
            AudioClip randomClip = category.clips[randomIndex];

            effectsSource.volume = volume;  // Ajuste del volumen
            effectsSource.PlayOneShot(randomClip);

            // Verificar si se est� aplicando el volumen correctamente
            Debug.Log("Reproduciendo efecto con volumen: " + volume);
        }
        else
        {
            Debug.LogWarning("Categor�a no encontrada o sin clips asignados: " + categoryName);
        }
    }

    public IEnumerator PlaySequentialSoundEffects(string firstCategory, string secondCategory, float volume = 1f)
    {
        PlayRandomSoundEffect(firstCategory, volume);
        yield return new WaitForSeconds(effectsSource.clip.length);  // Espera a que termine el primer sonido
        PlayRandomSoundEffect(secondCategory, volume);  // Luego reproduce el segundo sonido
    }

    // Detener la m�sica de fondo
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Detener todos los efectos de sonido
    public void StopSoundEffects()
    {
        effectsSource.Stop();
    }
}