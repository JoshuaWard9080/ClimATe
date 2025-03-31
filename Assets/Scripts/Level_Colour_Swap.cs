using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Level_Colour_Swap : MonoBehaviour
{
    
    public int level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color colour = Color.white;
        if (level <= 2)
        {
            colour = Color.green;
        }
        else if (level <= 5)
        {
            colour = Color.gray;
        }
        else
        {
            colour= Color.blue;
        }
        findChildren(this.transform,colour);
        //    int children = transform.childCount;
        //    for (int i = 0; i < children; ++i)
        //        print("For loop: " + transform.GetChild(i));

    }

    public void findChildren(Transform transform, Color colour)
    {
        int numOfChildren = transform.childCount;
        if (numOfChildren > 0)
        {
            for (int i = 0; i < numOfChildren; ++i)
            {
                findChildren(transform.GetChild(i),colour);
                
            }
        }
        else
        {
            //Debug.Log("Leaf child: " + transform.gameObject + " TM Pro: " + transform.gameObject.GetComponent<TextMeshProUGUI>());
            if (transform.gameObject.GetComponent<TextMeshProUGUI>() != null)
            {
                //Debug.Log("text not null at: " + transform.gameObject + " , " + transform.gameObject.GetComponent<TextMeshProUGUI>().text);
                changeNumber(transform.gameObject.GetComponent<TextMeshProUGUI>());
                colourNumber(transform.gameObject.GetComponent<TextMeshProUGUI>(),colour);
            }
            changeColour(transform, colour);
        }
        
    }

    void changeNumber(TextMeshProUGUI text)
    {
        text.SetText(level.ToString());
    }
    void colourNumber(TextMeshProUGUI text, Color colour)
    {
        text.color = colour;
    }

    void changeColour(Transform transform,Color colour)
    {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null && transform.gameObject.tag != "IgnoreFloorColour") 
        { 
            renderer.material.color = colour; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
