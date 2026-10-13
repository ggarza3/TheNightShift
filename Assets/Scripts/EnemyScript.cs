using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector2 dir;
    private Transform target;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2f;
        target = GameObject.Find("JanitorSkeleton").transform;
        dir = target.position - transform.position;
        dir = dir/dir.magnitude;

        //dir = new Vector2(dir.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = (Vector2) transform.position + dir * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + dir.x * speed * Time.deltaTime, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            if(collision.gameObject.tag.Equals("Mop")){
                Debug.Log("hit mop!");
                Destroy(gameObject);
            }
        }
    }
}
