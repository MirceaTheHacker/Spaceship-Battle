using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private SoundManager()
    {
    }
    private List<AudioSource> m_runningSources;

    public enum Sound
    {
        PlayerAttack,
        PlayerDie,
        EnemyAttack,
        EnemyDie,
        ButtonHover,
        ButtonClick
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
        m_runningSources = new List<AudioSource>();
    }

    private void Update()
    {
        foreach (AudioSource audioSource in m_runningSources.ToArray())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.gameObject.SetActive(false);
                m_runningSources.Remove(audioSource);
            }
        }
    }
    
    public void PlaySound(Sound sound)
    {
        GameObject soundGameObject = ObjectPooler.Instance.GetPooledObject(typeof(AudioSource));
        soundGameObject.SetActive(true);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        m_runningSources.Add(audioSource);
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;
    }
}
