using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public void StartFloorFirstScene()
    {
        SceneManager.LoadSceneAsync("Floor 1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
