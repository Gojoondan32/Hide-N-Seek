using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    // Singleton instance
    public static Audio_Manager Instance { get; private set; }

    // List of audio clips to be used
    [SerializeField] private List<AudioClip> audioClips;

    // Dictionary to store object pools for each audio clip
    private Dictionary<AudioClip, Object_Pool<AudioSource>> audioObjectPools = new Dictionary<AudioClip, Object_Pool<AudioSource>>();

    // Initialize the singleton instance
    private void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create an object pool for each audio clip
        foreach (AudioClip clip in audioClips)
        {
            // Create a new object pool for the clip
            Object_Pool<AudioSource> pool = new Object_Pool<AudioSource>(CreateAudioSource(clip), transform, 2);
            audioObjectPools.Add(clip, pool);

        }
    }

    // Create an audio source object for a given audio clip
    private AudioSource CreateAudioSource(AudioClip clip)
    {
        GameObject newAudioObject = new GameObject("AudioSource_" + clip.name);
        AudioSource audioSource = newAudioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 40f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        return audioSource;
    }

    // Play an audio clip at a given position
    public void Play(string clipName, Vector3 position, float volume = 1f)
    {
        AudioClip clip = audioClips.Find(x => x.name == clipName);

        if (clip == null)
        {
            Debug.LogWarning("AudioManager: No audio clip found with name " + clipName);
            return;
        }

        // Get the object pool for the given clip
        Object_Pool<AudioSource> pool = audioObjectPools[clip];

        // Get an object from the pool, or create a new one if none are available
        AudioSource audioObject = pool.GetObjectFromPool();

        // Set the position of the audio object and play the clip
        audioObject.transform.position = position;
        audioObject.volume = volume;
        audioObject.Play();

        // Return the audio object to the pool when the clip is finished playing
        StartCoroutine(ReturnToPool(audioObject, clip.length));
    }

    // Return an audio object to its object pool after a given delay
    private IEnumerator ReturnToPool(AudioSource audioObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioObject.Stop();
        audioObjectPools[audioObject.clip].ReturnObjectToPool(audioObject);
        audioObject.transform.position = Vector3.zero;
    }
}
