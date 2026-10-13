using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public float rate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("makeEnemy", 0, rate);
    }

    void makeEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
