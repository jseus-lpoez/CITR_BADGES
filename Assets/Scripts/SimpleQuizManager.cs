using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SimpleQuizManager : MonoBehaviour
{
    public Button[] answerButtons;
    public TMP_Text messageText;

    public int correctAnswerIndex = 0;
    public string badgeKey = "cottonBadgeCollected";

    public string nextSceneName = "LabScene";
    public float transitionDelay = 3f;

    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private bool answeredCorrectly = false;

    public void CheckAnswer(int answerIndex)
    {
        // 1. Prevent multiple clicks if already answered
        if (answeredCorrectly) return;

        // 2. Safety check for button bounds
        if (answerIndex < 0 || answerIndex >= answerButtons.Length) return;

        Button clickedButton = answerButtons[answerIndex];

        // 3. Strict IF/ELSE to separate Correct vs. Wrong SFX
        if (answerIndex == correctAnswerIndex)
        {
            answeredCorrectly = true;
            clickedButton.image.color = correctColor;

            // 🎵 Play ONLY Correct Answer SFX
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayCorrectAnswer();
            }

            if (messageText != null)
            {
                messageText.text = "Congratulations! You collected a new badge. Check it out in the Lobby.";
            }

            // 🏷️ Save badge progress & companion station
            PlayerPrefs.SetInt(badgeKey, 1);
            int currentStation = PlayerPrefs.GetInt("SavedStationIndex", 0);
            PlayerPrefs.SetInt("SavedStationIndex", currentStation + 1);
            PlayerPrefs.Save();

            StartCoroutine(LoadNextSceneAsync());
        }
        else
        {
            clickedButton.image.color = wrongColor;

            // 🎵 Play ONLY Wrong Answer SFX
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayWrongAnswer();
            }
        }
    }

    private IEnumerator LoadNextSceneAsync()
    {
        yield return new WaitForSeconds(transitionDelay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}