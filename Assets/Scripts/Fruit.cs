using UnityEngine;
using UnityEngine.UI;


public class Fruit : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioSource collectFishAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collectFishAudio != null)
            {
                AudioSource.PlayClipAtPoint(collectFishAudio.clip, transform.position);
            }
            
            if (LevelStatsManager.Instance != null)
            {
                LevelStatsManager.Instance.fishCollected++;
            }

            Destroy(gameObject);
        }
    }

}
