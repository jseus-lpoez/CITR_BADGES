using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class Station
    {
        public string stationName;
        public Transform stationTransform; // Waypoint position for the companion
        [TextArea(3, 5)]
        public string companionDialogue;   // Text spoken at this station
    }

    [Header("Setup References")]
    [SerializeField] private NavMeshAgent companionAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueCanvas;

    [Header("Tour Route Configuration")]
    [SerializeField] private Station[] stations; 

    private int currentStationIndex = 0;
    private bool tourCompleted = false;

    private const string STATION_INDEX_KEY = "SavedStationIndex";

    private void Start()
    {
        if (companionAgent == null) 
            companionAgent = GetComponent<NavMeshAgent>();

        // Load the saved station index
        currentStationIndex = PlayerPrefs.GetInt(STATION_INDEX_KEY, 0);
        
        // Ensure companion warps to the saved station instantly if reloading from a quiz
        PositionCompanionAtCurrentStation();
        
        DisplayAndMoveToStation();
    }

    private void Update()
    {
        if (tourCompleted) return;

        // Turn companion toward player when at destination
        if (companionAgent != null && (companionAgent.isStopped || companionAgent.remainingDistance <= companionAgent.stoppingDistance))
        {
            LookAtPlayerFace();
        }
    }

    private void PositionCompanionAtCurrentStation()
    {
        if (stations == null || stations.Length == 0) return;
        if (currentStationIndex >= stations.Length) return;

        Station currentStation = stations[currentStationIndex];
        if (currentStation.stationTransform != null && companionAgent != null)
        {
            // Snap agent directly to the target waypoint on scene load
            companionAgent.Warp(currentStation.stationTransform.position);
        }
    }

    private void DisplayAndMoveToStation()
    {
        if (stations == null || stations.Length == 0) return;

        // Check if the tour is completed
        if (currentStationIndex >= stations.Length)
        {
            tourCompleted = true;
            if (dialogueText != null) dialogueText.text = "The tour is complete! Enjoy the lab.";
            return;
        }

        Station currentStation = stations[currentStationIndex];
        
        // Update UI
        if (dialogueCanvas != null) dialogueCanvas.SetActive(true);
        if (dialogueText != null) dialogueText.text = currentStation.companionDialogue;
            
        // Move Companion to station target position
        if (currentStation.stationTransform != null && companionAgent != null)
        {
            companionAgent.isStopped = false;
            companionAgent.SetDestination(currentStation.stationTransform.position);
        }
    }

    // 🚀 CALL THIS WHEN A VIDEO OR QUIZ STAGE FINISHES
    public void OnVideoFinished()
    {
        if (tourCompleted) return;

        // Advance to next station
        currentStationIndex++;
        
        // Save state so scene transitions don't lose progress
        PlayerPrefs.SetInt(STATION_INDEX_KEY, currentStationIndex);
        PlayerPrefs.Save();

        if (companionAgent != null) companionAgent.isStopped = true;
        
        DisplayAndMoveToStation();
    }

    private void LookAtPlayerFace()
    {
        if (playerTransform == null || companionAgent == null) return;

        Vector3 lookDirection = playerTransform.position - companionAgent.transform.position;
        lookDirection.y = 0f; 

        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            companionAgent.transform.rotation = Quaternion.Slerp(companionAgent.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    // Call this if you want to restart the tour from Station 1
    [ContextMenu("Reset Station Progress")]
    public void ResetStationProgress()
    {
        PlayerPrefs.DeleteKey(STATION_INDEX_KEY);
        currentStationIndex = 0;
        tourCompleted = false;
        PositionCompanionAtCurrentStation();
        DisplayAndMoveToStation();
    }
}