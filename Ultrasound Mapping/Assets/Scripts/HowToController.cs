using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class HowToController : MonoBehaviour
{
    public TextMeshProUGUI instructions;
    public Image photo;
    public Sprite MainMenuSprite;
    public Button PreviousButton, HomeButton, NextButton;
    public int num;
    void Start()
    {
        num = 0;
        //PreviousButton.class{button-style};
        //PreviousButton.button-style:hover;
    }
    public void ButtonDemo()
    {
        num ++;
        switch(num)
        {
            case 0:
                instructions.GetComponent<TextMeshProUGUI>().text = "Welcome. The Goal of this program is to help you refind the orientation of your ultrasound probe when making multiple scans. Press the next button when you are ready to continue:";
                break; 
            case 1:
                photo.sprite = MainMenuSprite;
                instructions.GetComponent<TextMeshProUGUI>().text = "The main menu consists of multiple options,\n click next to continue";
                break;                
            case 2:
                instructions.GetComponent<TextMeshProUGUI>().text = "The first time you scan a location, you will click 'New Scan' \n click next to continue";
                break;
            case 3:
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                break;
            default:
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                break;
        }
    

    }
    public void ButtonPrevious()
    {
        num--;
        if(num < 0)
        {
            num = 0;
        }
        string instCapture = instructions.GetComponent<TextMeshProUGUI>().text;
        switch(instCapture)
        {
            case "The main menu consists of multiple options,\n click next to continue":
                instructions.GetComponent<TextMeshProUGUI>().text = "Welcome. The Goal of this program is to help you refind the orientation of your ultrasound probe when making multiple scans. Press the next button when you are ready to continue:";
                break; 

            case "The first time you scan a location, you will click 'New Scan' \n click next to continue":
                photo.sprite = MainMenuSprite;
                instructions.GetComponent<TextMeshProUGUI>().text = "The main menu consists of multiple options,\n click next to continue";
                break;                
            case "Place Holder,\n click next to continue":
                instructions.GetComponent<TextMeshProUGUI>().text = "The first time you scan a location, you will click 'New Scan' \n click next to continue";
                break;
            case "":
                instructions.GetComponent<TextMeshProUGUI>().text = "Place Holder,\n click next to continue";
                break;
            default:
                instructions.GetComponent<TextMeshProUGUI>().text = "Welcome. The Goal of this program is to help you refind the orientation of your ultrasound probe when making multiple scans. Press the next button when you are ready to continue:";
                break;
        }
    }


    public void ButtonHome()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
