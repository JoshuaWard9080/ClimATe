using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] private float fallDelay; //how long it shakes before falling
    [SerializeField] private float fallSpeed; //how fast it falls
    [SerializeField] private float regenTime; //how long before it regenerates
    [SerializeField] private float size; //changes the scale of the icicle
    [SerializeField] private float fallCoolDown; //the time before the icicle can fall again

    private float lastFallTime; //time the icicle last fell
    private bool isFalling = false;
    private bool canFall = true;
    private bool isRegenerating = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D mainCollider;
    private Collider2D triggerCollider;

    //original data from scene for the position
    private Vector3 originalPosition;
    private Vector3 originalSize;

    void Start()
    {
        Debug.Log("Start method called");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.isTrigger)
            {
                triggerCollider = col;
            }
            else
            {
                mainCollider = col;
            }
        }

        //save the starting position
        originalPosition = transform.position;
        originalSize = transform.localScale;
        transform.rotation = Quaternion.identity;

        //setting the rb to kinematic so that it will not fall
        //this will be changed when the icicle should be able to fall
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        //add sway animation to icicle for juice?

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("OnTriggerEnter2D method called");

        //if the icicle can fall, is not regenerating, and was hit by the player then shake and fall
        if (canFall && !isFalling && !isRegenerating && coll.CompareTag("Player"))
        {
            Debug.Log("RAHHHHHH pls work");

            if (Time.time - lastFallTime < fallCoolDown)
            {
                return;
            }

            //set the last fall time to now
            lastFallTime = Time.time;

            //start shaking and falling
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        Debug.Log("STARTING fall: setting Rigidbody to Dynamic");
        mainCollider.enabled = false;
        isFalling = true;

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

        mainCollider.enabled = true;
        yield return null;

        //allow gravity to let it fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D method called");

        if (isFalling && collision.collider.CompareTag("Player"))
        {
            //make player lose health or die (not sure which one yet)
            //play animation of when player gets hurt

            //hide sprite when it hits player
            spriteRenderer.enabled = false;
            mainCollider.enabled = false;

            //start coroutine to regrow
            //set icicle to not be able to fall again
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;

            isFalling = false;
            StartCoroutine(Regrow()); //call method to regrow the icicle
        }

        if (isFalling && collision.collider.CompareTag("Ground"))
        {
            //hide sprite when it hits the ground
            spriteRenderer.enabled = false;
            mainCollider.enabled = false;

            //start coroutine to regrow
            //set icicle to not be able to fall again
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;

            isFalling = false;
            StartCoroutine(Regrow());
        }
    }

    IEnumerator Regrow()
    {
        isRegenerating = true;

        //reset the icicle's physics to not falling, no gravity, kinematic
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.rotation = Quaternion.identity;

        Debug.Log("Regrow coroutine started");
        yield return new WaitForSeconds(regenTime);

        //set icicle back to original position
        transform.position = originalPosition;
        transform.localScale = originalSize * size;

        //show sprite again
        spriteRenderer.enabled = true;
        mainCollider.enabled = true;

        Debug.Log("Regrow complete: sprite & collider restored");

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
    
    public void SetFallCooldown(float cooldown)
    {
        fallCoolDown = cooldown;
    }    
}
