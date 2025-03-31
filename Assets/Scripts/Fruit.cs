using UnityEngine;
using UnityEngine.UI;


public class Fruit : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fruit has been collected! Check Fruit.cs to implement points stuff");
            if (LevelStatsManager.Instance != null)
            {
                LevelStatsManager.Instance.fruitsCollected++;
            }
            Destroy(gameObject);
        }
    }

}
