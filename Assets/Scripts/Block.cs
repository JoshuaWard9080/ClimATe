using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource blockDestroyedAudio;

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

            if (blockDestroyedAudio != null && blockDestroyedAudio.clip != null)
            {
                AudioSource.PlayClipAtPoint(blockDestroyedAudio.clip, transform.position);
            }
            else
            {
                Debug.Log("Audio is null for destroying a block");
            }

            onHitByPlayer();
        }
    }
    void onHitByPlayer()
    {
        if (LevelStatsManager.Instance != null)
        {
            LevelStatsManager.Instance.blocksDestroyed++;
            Debug.Log("Block destroyed, incrementing blocksDestroyed count");
        }
        Destroy(gameObject);
    }
}
