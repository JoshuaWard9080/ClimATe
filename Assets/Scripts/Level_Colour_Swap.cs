using UnityEngine;
using TMPro;

public class Level_Colour_Swap : MonoBehaviour
{
    
    public int level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color colour = Color.white;
        
        if (level <= 0)
        {
            colour = new Color(180 / 255f, 226 / 255f, 255 / 255f);
        }
        else if (level <= 2)
        {
            colour = new Color(78/255f,183 / 255f, 72 / 255f);
        }
        else if (level <= 5)
        {
            colour = new Color(86 / 255f,51 / 255f,100 / 255f);
        }
        else if (level <= 8)
        {
            colour= new Color(89 / 255f, 144 / 255f, 204 / 255f);
        }
        else
        {
            colour = new Color(180 / 255f, 226 / 255f, 255 / 255f);
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
