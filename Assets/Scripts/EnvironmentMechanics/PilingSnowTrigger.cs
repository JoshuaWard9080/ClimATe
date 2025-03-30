using UnityEngine;

public class PilingSnowTrigger : MonoBehaviour
{
    [SerializeField] private PilingSnow snow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            snow.StartRising();
            gameObject.SetActive(false); //disable the trigger so if the player goes through it again nothing happens, this is a one time trigger
        }
    }
}
