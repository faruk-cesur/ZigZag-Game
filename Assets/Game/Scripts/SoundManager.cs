using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public AudioClip moveSound;
    public AudioClip deathSound;
    public AudioClip diamondSound;
    public AudioClip multipleScoreSound;
    public AudioClip startSound;
    
    [HideInInspector] public AudioSource audioSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySound(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}