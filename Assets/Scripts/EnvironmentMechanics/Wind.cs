using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windStrength; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private float windSpeed; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private Vector2 windDirection;
    [SerializeField] private Rigidbody2D playerRB;

    [SerializeField] private float drag = 10.0f;

    public void SetWindStrength(float newWindStrength)
    {
        windStrength = newWindStrength;
        Debug.Log("Wind strength set to: " + newWindStrength);
    }

    public void SetWindSpeed(float newWindSpeed)
    {
        windSpeed = newWindSpeed;
        Debug.Log("Wind speed set to: " + newWindSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D objectRB = collision.GetComponent<Rigidbody2D>();
        objectRB.AddForce(new Vector2(-(objectRB.linearVelocity.x * drag), 0));

    }
}
