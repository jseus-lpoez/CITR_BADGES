using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source Reference")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip correctAnswerSFX;
    [SerializeField] private AudioClip wrongAnswerSFX;

    private void Awake()
    {
        transform.SetParent(null);
        // Keep single instance across scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null) 
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayButtonClick()
    {
        if (buttonClickSFX != null && audioSource != null)
            audioSource.PlayOneShot(buttonClickSFX);
    }

    public void PlayCorrectAnswer()
    {
    Debug.Log("AudioManager: Playing Correct SFX!");    
    if (correctAnswerSFX != null && audioSource != null)
            audioSource.PlayOneShot(correctAnswerSFX);
    else 
        Debug.LogWarning("AudioManager: Missing AudioClip or AudioSource reference!");
    }

    public void PlayWrongAnswer()
    {
        if (wrongAnswerSFX != null && audioSource != null)
            audioSource.PlayOneShot(wrongAnswerSFX);
    }
}