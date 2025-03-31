using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{

    [SerializeField] private float fallDelay; //how long it shakes before falling
    [SerializeField] private float fallSpeed; //how fast it falls
    [SerializeField] private float regenTime; //how long before it regenerates
    [SerializeField] private float size = 1f; //changes the scale of the icicle
    [SerializeField] private bool canTrigger = true;
    [SerializeField] TemperatureManager temperatureManager;

    [Header("Audio")]
    [SerializeField] private AudioSource playerHittingIcicleAudio;
    [SerializeField] private AudioSource icicleHurtingPlayer;

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

    //getting block the icicle is attached too
    private GameObject supportingBlock;
    [SerializeField] private LayerMask blockLayer;


    void Start()
    {

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

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position + Vector3.up * 0.5f, 0.1f, blockLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Block"))
            {
                supportingBlock = hit.gameObject;
                Debug.Log("Supported by block: " + supportingBlock.name);
                break;
            }
        }

        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    void tempChangeToWarm()
    {
        CanFall(true);
        RegenerateSpeed(3f);
    }

    void tempChangeToCold()
    {
        CanFall(true);
        RegenerateSpeed(1f);
    }

    void tempChangeToFreezing()
    {
        CanFall(true);
        RegenerateSpeed(5f);
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

        yield return new WaitForSeconds(0.05f);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D method called");

        if (!isFalling && !isShaking && !isRegenerating && canTrigger && collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with icicle, starting shake and fall.");

            if (playerHittingIcicleAudio != null)
            {
                playerHittingIcicleAudio.Play();
            }

            if (LevelStatsManager.Instance != null)
            {
                LevelStatsManager.Instance.iciclesDestroyed++;
            }

            StartCoroutine(ShakeAndFall()); //call method to regrow the icicle
            return;
        }

        //if the icicle htis the player or the ground
        if (isFalling && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Block")))
        {
            Debug.Log("Icicle hit object: " + collision.collider.name + ", Tag: " + collision.collider.tag);

            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Icicle damaged the player, this is a skill issue ;)");

                LivesDisplay lives = FindObjectOfType<LivesDisplay>();

                if (icicleHurtingPlayer != null)
                {
                    icicleHurtingPlayer.Play();
                }

                if (lives != null)
                {
                    lives.TakeDamage();
                }
                else
                {
                    Debug.LogWarning("No LivesDisplay found in scene --> OnCollisionEnter2D in Icicle.cs");
                }
            }

            //TODO: Add sound for when the icicle hits the ground

            if (Vector2.Distance(transform.position, originalPosition) < 0.2f)
            {
                Debug.Log("Ignoring collision, the icicle hasn't fallen far enough yet.");
                return;
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

        //check if supporting block exists
        if (supportingBlock == null)
        {
            Debug.Log("Skipping regrowth, the block above has been destroyed.");
            yield break;
        }

        //reset the icicle's physics to not falling, no gravity, kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;

        Debug.Log("Regrow coroutine started");
        yield return new WaitForSeconds(regenTime);

        //check if supporting block was destroyed again, in case player destroyed it after the icicle falls but before it regrows
        if (supportingBlock == null)
        {
            Debug.Log("Skipping regrowth, the block above has been destroyed.");
            yield break;
        }

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
