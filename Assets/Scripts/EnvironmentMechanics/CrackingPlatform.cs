using UnityEngine;

public class CrackingPlatform : MonoBehaviour
{
    public void SetCrackSpeed(float crackSpeed)
    {
        Debug.Log("CrackingPlatform crack speed set to: " + crackSpeed);
    }

    public void SetMeltSpeed(float meltSpeed)
    {
        Debug.Log("CrackingPlatform melt speed set to: " + meltSpeed);
    }
}
