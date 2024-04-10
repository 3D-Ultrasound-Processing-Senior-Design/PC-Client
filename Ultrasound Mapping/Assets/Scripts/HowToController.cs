using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class HowToController : MonoBehaviour
{
    public TextMeshProUGUI instructions;
    public Image photo;
    public Sprite MainMenuSprite;
    private int num;
    void Start()
    {
        num = 0;
    }
    public void ButtonDemo()
    {
        switch(num)
        {
            case 0:
                photo.sprite = MainMenuSprite;
                instructions.GetComponent<TextMeshProUGUI>().text = "The main menu consists of multiple options,\n click next to continue";
                num++;
                break;
            case 1:
                instructions.GetComponent<TextMeshProUGUI>().text = "The first time you scan a location, you will click 'New Scan' \n click next to continue";
                num++;
                break;
            case 2:
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                num++;
                break;
            case 3:
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                num++;
                break;
            default:
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                num++;
                break;
        }


    }
}
