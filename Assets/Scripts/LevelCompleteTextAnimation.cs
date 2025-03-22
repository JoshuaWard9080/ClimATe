using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;

    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenTexts = 0.5f;
    [SerializeField] AudioSource typingAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         foreach (TextMeshProUGUI text in texts)
        {
            text.maxVisibleCharacters = 0;
        }

        StartCoroutine(AnimateTexts());
    }

    private IEnumerator AnimateTexts()
    {
        foreach (TextMeshProUGUI textMesh in texts)
        {
            yield return StartCoroutine(TypeText(textMesh));
            yield return new WaitForSeconds(timeBetweenTexts);
        }
    }

    private IEnumerator TypeText(TextMeshProUGUI textMesh)
    {
        string fullText = textMesh.text;
        textMesh.ForceMeshUpdate();

        int totalCharacters = textMesh.textInfo.characterCount;
        int visibleCharacters = 0;


        while (visibleCharacters <= totalCharacters)
        {
            textMesh.maxVisibleCharacters = visibleCharacters;
            typingAudio.Play();
            visibleCharacters++;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
