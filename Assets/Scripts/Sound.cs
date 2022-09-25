using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    // Start is called before the first frame update
    public AudioClip clip;
    

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
