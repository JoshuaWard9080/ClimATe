using UnityEngine;

public class Icicle : MonoBehaviour
{
    public void SetSize(float icicleSize)
    {
        Debug.Log("Icicle size set to: " + icicleSize);
    }

    public void CanFall(bool canFall)
    {
        Debug.Log("Icicle can fall: " + canFall);
    }

    public void RegenerateSpeed(float regenerateSpeed)
    {
        Debug.Log("Icicle regenerate speed set to: " + regenerateSpeed);
    }
}
