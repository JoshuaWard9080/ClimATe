using System.Collections;
using System.Threading;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] private float fallDelay; //how long it shakes before falling
    [SerializeField] private float fallSpeed; //how fast it falls
    [SerializeField] private float regenTime; //how long before it regenerates
    [SerializeField] private float size; //changes the scale of the icicle
    private bool isFalling = false;
    private bool canFall = true;
    private bool isRegenerating = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D col2D;
    private Vector3 originalPosition;
    private Vector3 originalSize;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.position;
        originalSize = transform.localScale;

        //setting the rb to kinematic so that it will not fall
        //this will be changed when the icicle should be able to fall
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        //add sway animation for juice?

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("OnTriggerEnter2D method called");
        
        //if the icicle can fall, is not regenerating, then shake and fall
        if (canFall && !isFalling && !isRegenerating && coll.CompareTag("Player"))
        {
            Debug.Log("StartCoroutine in OnTriggerEnter method called");
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        isFalling = true;

        Vector3 original = transform.position;
        float elapsed = 0f;

        while (elapsed < fallDelay)
        {
            float x = Mathf.Sin(Time.time * 40f) * 0.05f;
            transform.position = original + new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = original;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && collision.collider.CompareTag("Player"))
        {
            //make player lose health or die (not sure which one yet)
            //play animation of when player gets hurt


            spriteRenderer.enabled = false;

            //make icicle disappear
            col2D.enabled = false;

            //start coroutine to regrow
            //set icicle to not be able to fall again
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(Regrow());
        }

        if (isFalling && collision.collider.CompareTag("Ground"))
        {
            //disable collider
            spriteRenderer.enabled = false;

            //make icicle disappear
            col2D.enabled = false;

            //start coroutine to regrow
            //set icicle to not be able to fall again
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(Regrow());
        }
    }

    IEnumerator Regrow()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(regenTime);

        transform.position = originalPosition;
        transform.localScale = originalSize * size;

        spriteRenderer.enabled = true;
        col2D.enabled = true;

        isFalling = false;
        isRegenerating = false;
    }

    public void SetSize(float icicleSize)
    {
        size = icicleSize;
        transform.localScale = originalSize * size;
        Debug.Log("Icicle size set to: " + icicleSize);
    }

    public void CanFall(bool enableFall)
    {
        canFall = enableFall;
        Debug.Log("Icicle can fall: " + enableFall);
    }

    public void RegenerateSpeed(float regenerateSpeed)
    {
        regenTime = regenerateSpeed;
        Debug.Log("Icicle regenerate speed set to: " + regenerateSpeed);
    }
}
