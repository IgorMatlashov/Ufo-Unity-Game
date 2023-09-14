using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void OpenLevel(int LevelId)
    {
        string levelName = "Level " + LevelId;
        SceneManager.LoadScene(levelName);
    }
}
