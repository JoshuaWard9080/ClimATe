using UnityEngine;

public class Block : MonoBehaviour
{

    public GameObject breakEffect;

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

        if (breakEffect != null)
        {
            GameObject effectInstance = Instantiate(breakEffect, transform.position, Quaternion.identity);

            // Find the Renderer 
            Renderer blockRenderer = GetComponentInChildren<Renderer>();
            if (blockRenderer != null)
            {
                Color blockColor = blockRenderer.material.color;

                // Apply color to the particle system
                ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var mainModule = ps.main;
                    mainModule.startColor = blockColor;
                }
            }

            Destroy(effectInstance, 2f);
        }

        if (LevelStatsManager.Instance != null)
        {
            LevelStatsManager.Instance.blocksDestroyed++;
            Debug.Log("Block destroyed, incrementing blocksDestroyed count");
        }
        Destroy(gameObject);
    }
}
