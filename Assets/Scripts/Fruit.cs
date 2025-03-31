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

}
