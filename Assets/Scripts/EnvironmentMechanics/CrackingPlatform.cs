using UnityEngine;

public class CrackingPlatform : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float crackSpeed;
    [SerializeField] private float meltSpeed;
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color crackedColor = Color.yellow;
    [SerializeField] private Color brokenColor = Color.red;
    [SerializeField] TemperatureManager temperatureManager;


    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;
    public bool isPlayerOn = false;
    private float timer = 0f;
    private bool isBroken = false;


    void Start()
    {
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);

        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
        spriteRenderer.color = baseColor;
    }

    void Update()
    {
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }
        
        animator.SetBool("isPlayerOn", isPlayerOn);
        animator.SetFloat("AnimSpeed", crackSpeed);

        if (isBroken || !isPlayerOn)
        {
            return;
        }

        timer += Time.deltaTime * (1 + meltSpeed);

        if (timer >= crackSpeed)
        {
            BreakPlatform();
        }
        else if (timer >= crackSpeed / 2f)
        {
            spriteRenderer.color = crackedColor;
        }
    }

    void tempChangeToWarm()
    {
        SetCrackSpeed(3f);
        SetMeltSpeed(3f);
    }

    void tempChangeToCold()
    {
        SetCrackSpeed(1f);
        SetMeltSpeed(0f);
    }

    void tempChangeToFreezing()
    {
        SetCrackSpeed(3f);
        SetMeltSpeed(0f);
    }

    private void BreakPlatform()
    {
        isBroken = true;
        spriteRenderer.color = brokenColor;
        col2D.enabled = false;

        Invoke(nameof(DisableRenderer), 0.25f); 

        Debug.Log("Platform has broken.");
    }

    private void DisableRenderer()
    {
        spriteRenderer.enabled = false;
    }

    public void SetCrackSpeed(float newCrackSpeed)
    {
        crackSpeed = newCrackSpeed;
        Debug.Log("CrackingPlatform crack speed set to: " + newCrackSpeed);
    }

    public void SetMeltSpeed(float newMeltSpeed)
    {
        meltSpeed = newMeltSpeed;
        Debug.Log("CrackingPlatform melt speed set to: " + newMeltSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOn = true;
            Debug.Log("Player stepped on cracking platform.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOn = false;
            timer = 0f;
            Debug.Log("Player stepped off cracking platform.");
        }
    }
}