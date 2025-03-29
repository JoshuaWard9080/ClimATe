using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] private float fallDelay; //how long it shakes before falling
    [SerializeField] private float fallSpeed; //how fast it falls
    [SerializeField] private float regenTime; //how long before it regenerates
    [SerializeField] private float size = 1f; //changes the scale of the icicle
    [SerializeField] private bool canTrigger = true;

    private bool isFalling = false;
    private bool canFall = true;
    private bool isRegenerating = false;
    private bool isShaking = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D mainCollider;

    //original data from scene for the position
    private Vector3 originalPosition;
    private Vector3 originalSize;

    void Start()
    {
        Debug.Log("Start method called");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();

        //save the starting position
        originalPosition = transform.position;
        originalSize = transform.localScale;
        transform.localScale = originalSize * size;
        transform.rotation = Quaternion.identity;

        //setting the rb to kinematic so that it will not fall
        //this will be changed when the icicle should be able to fall
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        //add sway animation to icicle for juice?

    }

    IEnumerator ShakeAndFall()
    {
        Debug.Log("Starting fall: setting Rigidbody to Dynamic");

        isShaking = true;
        canTrigger = false;

        Vector3 original = transform.position;
        float elapsed = 0f;

        //shake for the amount of time in fallDelay
        while (elapsed < fallDelay)
        {
            //shaking
            float x = Mathf.Sin(Time.time * 40f) * 0.05f;
            transform.position = original + new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //reset to original position after it shakes but before it starts falling
        transform.position = original;

        isShaking = false;
        isFalling = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D method called");

        if (!isFalling && !isShaking && !isRegenerating && canTrigger && collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with icicle, starting shake and fall.");
            StartCoroutine(ShakeAndFall()); //call method to regrow the icicle
            return;
        }

        //if the icicle htis the player or the ground
        if (isFalling && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground")))
        {
            if (collision.collider.CompareTag("Player"))
            {
                //TODO: make player lose health and play lose health animation if there is one
            }

            //start coroutine to regrow
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.rotation = Quaternion.identity;

            //hide sprite when it is hit
            spriteRenderer.enabled = false;
            mainCollider.enabled = false;

            isFalling = false;
            StartCoroutine(Regrow());
        }
    }

    IEnumerator Regrow()
    {
        isRegenerating = true;
        canTrigger = false;

        //reset the icicle's physics to not falling, no gravity, kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;

        Debug.Log("Regrow coroutine started");
        yield return new WaitForSeconds(regenTime);

        //set icicle back to original position
        transform.position = originalPosition;
        transform.localScale = originalSize * size;
        transform.rotation = Quaternion.identity;

        Debug.Log("Regrow complete: sprite & collider restored");

        yield return new WaitForSeconds(0.1f);

        isFalling = false;
        isRegenerating = false;   

        mainCollider.enabled = true;
        spriteRenderer.enabled = true;

        canTrigger = true;     
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
