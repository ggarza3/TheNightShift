using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZoneKey : MonoBehaviour
{
    public int currLevel;
    private GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("JanitorSkeleton");
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x > transform.position.x)
        {
            nextLevel();
        }
    }

    public void nextLevel()
    {
        if(currLevel == 1)
        {
            SceneManager.LoadSceneAsync("Floor 2");
        }
        if(currLevel == 2)
        {
            SceneManager.LoadSceneAsync("Floor 3");
        }
        if(currLevel == 3)
        {
            SceneManager.LoadSceneAsync("Victory");
        }  
    }
}
