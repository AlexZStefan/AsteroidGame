using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public static bool soundDisabled = false;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    [Header("Active Music")]     public AudioClip MenuMusic;
    [Header("Background Music")] public List<AudioClip> musicList;
    [Header("Sound Effects")]    public List<AudioClip> soundEffects;

    [UnityEngine.Range(0f, 1f)] public float musicVolume = 1f;
    [UnityEngine.Range(0f, 1f)] public float sfxVolume = 1f;

    [SerializeField] AudioMixer audioMixer;
    private Dictionary<string, AudioClip> soundsDictionary;

    private void LoadSFX()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/SFX");
        soundEffects.AddRange(clips);
    }

    public void ChangeMusic(AudioClip newSong)
    {
        StartCoroutine(ChangeMusicRoutine(newSong));
    }

    public IEnumerator ChangeMusicRoutine(AudioClip newSong)
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        musicSource.clip = newSong;
        musicSource.volume = 1;
        musicSource.Play();
        yield return null;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadSFX();
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; 
        musicSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        AudioMixerGroup[] mixerGroups = audioMixer.FindMatchingGroups("SFX");
        sfxSource.outputAudioMixerGroup = mixerGroups[0];

        soundsDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in soundEffects)
        {
            soundsDictionary[clip.name] = clip;
        }

        if (MenuMusic != null)
        {
            PlayMusic(MenuMusic);
        }
    }

    public void PlayMenuMusic()
    {
        ChangeMusic(MenuMusic);
    }

    /// <summary>
    /// Play background music.
    /// </summary>
    /// <param name="clip">The AudioClip to play as background music.</param>
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    /// <summary>
    /// Play a sound effect by name.
    /// </summary>
    /// <param name="name">The name of the sound effect to play.</param>
    public void PlaySFX(string name)
    {
        if (soundsDictionary.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{name}' not found.");
        }
    }

    public void AdjustSFXPitch(float value)
    {
        sfxSource.pitch = value;
    }

    /// <summary>
    /// Adjust the music volume.
    /// </summary>
    /// <param name="volume">The new music volume (0.0 to 1.0).</param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    /// <summary>
    /// Adjust the sound effects volume.
    /// </summary>
    /// <param name="volume">The new sound effects volume (0.0 to 1.0).</param>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Mute or unmute all sounds.
    /// </summary>
    /// <param name="isMuted">True to mute, false to unmute.</param>
    public void SetMute(bool isMuted)
    {
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;
    }

    public void MuteAllSounds()
    {
        SoundManager.soundDisabled = !SoundManager.soundDisabled;
        musicSource.mute = SoundManager.soundDisabled;
        sfxSource.mute = SoundManager.soundDisabled;
    }
}