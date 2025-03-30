using UnityEngine;

public class Block : MonoBehaviour
{

    private float xBound = 0.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" 
            && collision.gameObject.transform.position.y < this.transform.position.y
            && collision.gameObject.transform.position.x - this.transform.position.x < xBound
            && this.transform.position.x - collision.gameObject.transform.position.x < xBound){
            onHitByPlayer();
        }
    }
    void onHitByPlayer()
    {
        Destroy(gameObject);
    }
}
