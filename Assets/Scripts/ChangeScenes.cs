using UnityEngine;
using UnityEngine.SceneManagement; // Don't forget this!

public class ChangeScenes : MonoBehaviour
{
    public void gotoSceneTwo()
    {
        SceneManager.LoadScene("scene two");
    }
}