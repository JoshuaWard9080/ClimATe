using UnityEngine;

public class FlagWaver : MonoBehaviour
{
    [Header("Waving Animation")]
    public float frequency = 4f;
    public float amplitude = 0.2f;
    private Vector3 originalScale;

    [Header("Color Animation")]
    public Color colorA = Color.green;
    public Color colorB = Color.white;
    public float colorSpeed = 2f;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float wave = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localScale = new Vector3(originalScale.x + wave, originalScale.y, originalScale.z); //sine wave equation, hopefully makes the flag wave properly

        float lerpVal = Mathf.Sin(Time.time * colorSpeed) * 0.5f + 0.5f;
        spriteRenderer.color = Color.Lerp(colorA, colorB, lerpVal);
    }
}
