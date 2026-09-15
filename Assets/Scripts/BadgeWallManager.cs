using UnityEngine;

public class BadgeWallManager : MonoBehaviour
{
    public GameObject lintBadge;
    public GameObject cottonBadge;
    public GameObject sliverBadge;
    public GameObject yarnBadge;
    public GameObject cutPiecesBadge;
    public GameObject printedShirtBadge;
    public GameObject dyedShirtBadge;
    public GameObject fabricBadge;
    public GameObject assembledShirtBadge;
    public GameObject finishedShirtBadge;
    public GameObject dtgShirtBadge;

    void Start()
    {
        // PlayerPrefs.DeleteAll(); // Keep commented out for regular runs

        if (lintBadge != null) lintBadge.SetActive(PlayerPrefs.GetInt("lintBadgeCollected", 0) == 1);
        if (cottonBadge != null) cottonBadge.SetActive(PlayerPrefs.GetInt("cottonBadgeCollected", 0) == 1);
        if (sliverBadge != null) sliverBadge.SetActive(PlayerPrefs.GetInt("sliverBadgeCollected", 0) == 1);
        if (yarnBadge != null) yarnBadge.SetActive(PlayerPrefs.GetInt("yarnBadgeCollected", 0) == 1);
        if (cutPiecesBadge != null) cutPiecesBadge.SetActive(PlayerPrefs.GetInt("cutPiecesBadgeCollected", 0) == 1);
        if (printedShirtBadge != null) printedShirtBadge.SetActive(PlayerPrefs.GetInt("printedShirtBadgeCollected", 0) == 1);
        if (dyedShirtBadge != null) dyedShirtBadge.SetActive(PlayerPrefs.GetInt("dyedShirtBadgeCollected", 0) == 1);
        if (fabricBadge != null) fabricBadge.SetActive(PlayerPrefs.GetInt("fabricBadgeCollected", 0) == 1);
        if (assembledShirtBadge != null) assembledShirtBadge.SetActive(PlayerPrefs.GetInt("assembledShirtBadgeCollected", 0) == 1);
        if (finishedShirtBadge != null) finishedShirtBadge.SetActive(PlayerPrefs.GetInt("finishedShirtBadgeCollected", 0) == 1);
        if (dtgShirtBadge != null) dtgShirtBadge.SetActive(PlayerPrefs.GetInt("dtgShirtBadgeCollected", 0) == 1);
    }
}