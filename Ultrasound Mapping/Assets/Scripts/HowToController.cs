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
    //public TMP_text 
    
    public int num;
    public const string inst1 = "Welcome. The Goal of this program is to help you refind the orientation of your ultrasound probe when making multiple scans. Press the Next button when you are ready to continue";
    public const string inst2 = "Begin by attaching the IMU onto the ultrasound Probe. Place the probe where you plan on setting the origin of the IMU, and plug the IMU into the computer using a USB cable. Press the Next button when you are ready to continue";
    public const string inst3 = "To begin a scan one would select the 'Scan' button. Press the Next button when you are ready to continue";
    public const string inst4 = "This screen will take a few seconds to recognize that the IMU is connected. Once it recognizes the connection the model of the ultrasound probe will begin to move as you move it in space. There is also an indicator on the right side that will indicate when the IMU is connected";
    public const string inst5 = "To begin taking New Scan, the first step is to zero out the IMU. Place the probe where you would like to set the origin of the orientation. It is important to remember where this position is for if you need to re-zero the probe before replicating the scan. Press the ‘Zero’ button";
    public const string inst6 = "Now you may take the ultrasound scan as you normally would. When you have the image you want on the ultrasound machine, press the ‘Save Scan’ button. This will save the orientation of the probe at the time the button is clicked.";
    public const string inst7 = "This button will bring up a file explorer, where you can set the name of the .csv file that you save the orientation in. Name the file what you would like to, and then click save";
    public const string inst8 = "When you are ready to recreate the scan, place the probe in the position and location in which you zeroed the IMU on your initial scan. Press the ‘Zero’ button.";
    public const string inst9 = "To load the orientation data from a previous scan, press the ‘Load Scan’ Button. This will pull up a File Explorer in which you can select the .csv file with the orientation data you are trying to replicate.";
    public const string inst10 = "A grayed version of the ultrasound probe will appear at the orientation it was when the scan you are replicating was taken. Place the ultrasound probe on the patient's body where the scan you are trying to replicate was taken. In this position you can change the orientation of the ultrasound probe and the changes will be seen by the none grayed probe on the screen.";
    public const string inst11 = "You can use the models to overlay the two images of the grayed and un grayed probe to match the previous orientation. You can also look at the angles of rotation around the x,y, and z axis. As you change the orientation of the probe the current angles will display those changes. You can compare your current angles to your previous angles to replicate the scan. Once the angles or probe models match you can take the image on the ultrasound machine as you normally would";

    //public TextMeshProUGUI.text inst1,inst2,inst3;
    void Start()
    {
        num = 0;

    }
    public void ButtonDemo()
    {
        num ++;
        string instCapture = instructions.GetComponent<TextMeshProUGUI>().text;
        switch(instCapture)
        {
            case inst1:
                instructions.GetComponent<TextMeshProUGUI>().text = inst2;
                break;  
            case inst2:
                photo.sprite = MainMenuSprite;
                instructions.GetComponent<TextMeshProUGUI>().text = inst3;
                break;                
            case inst3:
                instructions.GetComponent<TextMeshProUGUI>().text = inst4;
                break;
            case inst4:
                instructions.GetComponent<TextMeshProUGUI>().text = inst5;
                break;
            case inst5:
                instructions.GetComponent<TextMeshProUGUI>().text = inst6;
                break;
            case inst6:
                instructions.GetComponent<TextMeshProUGUI>().text = inst7;
                break;
            case inst7:
                instructions.GetComponent<TextMeshProUGUI>().text = inst8;
                break;
            case inst8:
                instructions.GetComponent<TextMeshProUGUI>().text = inst9;
                break;
            case inst9:
                instructions.GetComponent<TextMeshProUGUI>().text = inst10;
                break;
            case inst10:
                instructions.GetComponent<TextMeshProUGUI>().text = inst11;
                break;
            default:
                instructions.GetComponent<TextMeshProUGUI>().text = inst1;
                break;
        }
    

    }
    public void ButtonPrevious()
    {

        string instCapture = instructions.GetComponent<TextMeshProUGUI>().text;
        switch(instCapture)
        {
            case inst2:
                instructions.GetComponent<TextMeshProUGUI>().text = inst1;
                break; 

            case inst3:
                photo.sprite = MainMenuSprite;
                instructions.GetComponent<TextMeshProUGUI>().text = inst2;
                break;                
            case inst4:
                instructions.GetComponent<TextMeshProUGUI>().text = inst3;
                break;
            case inst5:
                instructions.GetComponent<TextMeshProUGUI>().text = inst4;
                break;
            case inst6:
                instructions.GetComponent<TextMeshProUGUI>().text = inst5;
                break;
            case inst7:
                instructions.GetComponent<TextMeshProUGUI>().text = inst6;
                break;
            case inst8:
                instructions.GetComponent<TextMeshProUGUI>().text = inst7;
                break;
            case inst9:
                instructions.GetComponent<TextMeshProUGUI>().text = inst8;
                break;
            case inst10:
                instructions.GetComponent<TextMeshProUGUI>().text = inst9;
                break;
            case inst11:
                instructions.GetComponent<TextMeshProUGUI>().text = inst10;
                break;
            default:
                instructions.GetComponent<TextMeshProUGUI>().text = inst1;
                break;
        }
    }


    public void ButtonHome()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
