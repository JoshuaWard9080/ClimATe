using UnityEngine;

public class Block : MonoBehaviour
{
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
            && collision.gameObject.transform.position.y < this.transform.position.y){
            onHitByPlayer();
        }
    }
    void onHitByPlayer()
    {
        Destroy(gameObject);
    }
}
