using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update

    void Awake()
    {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        
    }

    // FindObjectOfType<AudioManager>().Play("theme");

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        if (s == null) return;
        s.source.Play();
    }
    
    public void MuteAll(){
        foreach(Sound sound in sounds){
            sound.source.mute = !sound.source.mute;
        }
    }

    void Start(){
        Play("AmbienceOffice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
