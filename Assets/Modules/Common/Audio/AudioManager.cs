using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public List<AudioClip> sounds;
    public List<AudioClip> musics;

    private AudioSource soundSource;
    private AudioSource musicSource;

    public void PlaySound(string name)
    {
        foreach (AudioClip clip in sounds)
        {
            if (clip.name != name) continue;
            if (!soundSource) soundSource = CameraController.Ins.gameObject.AddComponent<AudioSource>();
            Play(soundSource, clip);
            break;
        }
    }

    public void PlayMusic(string name)
    {
        foreach (AudioClip clip in musics)
        {
            if (clip.name != name) continue;
            if (!musicSource) musicSource = CameraController.Ins.gameObject.AddComponent<AudioSource>();
            Play(musicSource, clip);
            break;
        }
    }

    private void Play(AudioSource source, AudioClip clip)
    {
        if (source)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
