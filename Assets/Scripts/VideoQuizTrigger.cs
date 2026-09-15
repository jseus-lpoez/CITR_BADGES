using UnityEngine;
using UnityEngine.Video;

public class VideoQuizTrigger : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    
    [Header("UI Reference")]
    [SerializeField] private GameObject quizCanvas; // Drag your QuizCanvas here

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        
        // Hide the quiz canvas immediately when the video scene starts
        if (quizCanvas != null) quizCanvas.SetActive(false);

        // Tell Unity to run our function "OnVideoFinished" when the clip ends
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // The video ended! Pop the quiz panel up in front of the player
        if (quizCanvas != null)
        {
            quizCanvas.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        // Clean up the event listener when changing scenes
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}