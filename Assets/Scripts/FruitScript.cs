using UnityEngine;

public class FruitScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float initialHeight;
    float theta;
    void Start()
    {
        initialHeight = this.gameObject.transform.position.y+0.4f;
        theta = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, initialHeight + (Mathf.Sin(theta) / 3), this.gameObject.transform.position.z);
        theta += 0.01f;
    }
}
