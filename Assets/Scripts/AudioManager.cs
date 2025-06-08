using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Música de Fondo")]
    public AudioClip backgroundMusic;
    private AudioSource musicSource;

    [Header("SFX Player")]
    public AudioClip[] playerDamageClips;
    public AudioClip playerDeathClip;

    [Header("SFX Skeleton")]
    public AudioClip[] skeletonDamageClips;
    public AudioClip skeletonDeathClip;

    private AudioSource sfxSource;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Fuente para música
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.5f; // Ajusta al gusto

        // Fuente para SFX
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;
    }

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource.clip != null)
            musicSource.Play();
    }

    public void PlayPlayerDamage()
    {
        if (playerDamageClips.Length == 0) return;
        var clip = playerDamageClips[Random.Range(0, playerDamageClips.Length)];
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPlayerDeath()
    {
        if (playerDeathClip != null)
            sfxSource.PlayOneShot(playerDeathClip);
    }

    public void PlaySkeletonDamage()
    {
        if (skeletonDamageClips.Length == 0) return;
        var clip = skeletonDamageClips[Random.Range(0, skeletonDamageClips.Length)];
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySkeletonDeath()
    {
        if (skeletonDeathClip != null)
            sfxSource.PlayOneShot(skeletonDeathClip);
    }
}