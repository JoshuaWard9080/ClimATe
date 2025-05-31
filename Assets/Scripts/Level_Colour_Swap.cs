using UnityEngine;
using TMPro;

public class Level_Colour_Swap : MonoBehaviour
{
    
    public int level;
    public TemperatureManager temperatureManager;

    Color bonusWarm = new Color(180 / 255f, 186 / 255f, 215 / 255f);
    Color bonusCold = new Color(180 / 255f, 226 / 255f, 255 / 255f);
    Color bonusFreezing = new Color(225 / 255f, 245 / 255f, 255 / 255f);

    Color firstWarm = new Color(78 / 255f, 143 / 255f, 32 / 255f);
    Color firstCold = new Color(78 / 255f, 183 / 255f, 72 / 255f);
    Color firstFreezing = new Color(118 / 255f, 223 / 255f, 112 / 255f);

    Color secondWarm = new Color(86 / 255f, 11 / 255f, 60 / 255f);
    Color secondCold = new Color(86 / 255f, 51 / 255f, 100 / 255f);
    Color secondFreezing = new Color(126 / 255f, 91 / 255f, 140 / 255f);

    Color thirdWarm = new Color(89 / 255f, 104 / 255f, 164 / 255f);
    Color thirdCold = new Color(89 / 255f, 144 / 255f, 204 / 255f);
    Color thirdFreezing = new Color(129 / 255f, 184 / 255f, 244 / 255f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureManager = FindFirstObjectByType<TemperatureManager>();
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);

        Color colour = Color.white;
        
        if (level <= 0)
        {
            colour = bonusCold;
        }
        else if (level <= 2)
        {
            colour = firstCold;
        }
        else if (level <= 5)
        {
            colour = secondCold;
        }
        else if (level <= 8)
        {
            colour= thirdCold;
        }
        else
        {
            colour = bonusCold;
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
    void tempChangeToCold()
    {
        Color colour = Color.white;
        if (level <= 0)
        {
            colour = bonusCold;
        }
        else if (level <= 2)
        {
            colour = firstCold;
        }
        else if (level <= 5)
        {
            colour = secondCold;
        }
        else if (level <= 8)
        {
            colour = thirdCold;
        }
        else
        {
            colour = bonusCold;
        }
        findChildren(this.transform, colour);
    }

    void tempChangeToWarm()
    {
        Color colour = Color.white;
        if (level <= 0)
        {
            colour = bonusWarm;
        }
        else if (level <= 2)
        {
            colour = firstWarm;
        }
        else if (level <= 5)
        {
            colour = secondWarm;
        }
        else if (level <= 8)
        {
            colour = thirdWarm;
        }
        else
        {
            colour = bonusWarm;
        }
        findChildren(this.transform, colour);
    }

    void tempChangeToFreezing()
    {
        Color colour = Color.white;
        if (level <= 0)
        {
            colour = bonusFreezing;
        }
        else if (level <= 2)
        {
            colour = firstFreezing;
        }
        else if (level <= 5)
        {
            colour = secondFreezing;
        }
        else if (level <= 8)
        {
            colour = thirdFreezing;
        }
        else
        {
            colour = bonusFreezing;
        }
        findChildren(this.transform, colour);
    }
}
