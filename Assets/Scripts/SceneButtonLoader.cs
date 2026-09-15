using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour 
{
    public string sceneName;
    [SerializeField] private float soundDelay = 1.26f; // Gives SFX time to play in VR

    public void LoadNextScene() 
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync() 
    {
        // 1. Play the button click sound if AudioManager exists
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        // 2. Pause briefly so the audio clip actually plays through
        yield return new WaitForSeconds(soundDelay);

        // 3. Now load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone) 
        {
            yield return null;
        }
    }
}