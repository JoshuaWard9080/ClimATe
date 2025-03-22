using UnityEngine;
using TMPro;
using System.Collections;
using System.Diagnostics.Tracing;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] float timeBetweenCharacters;
    int i = 0;
    public string[] stringArray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndCheck();
    }

    void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            text1.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        text1.ForceMeshUpdate();
        int totalVisibleCharacters = text1.textInfo.characterCount;
        int counter = 0;


        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            text1.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke("EndCheck", timeBetweenCharacters);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
