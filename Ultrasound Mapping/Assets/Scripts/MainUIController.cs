using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{

    public Button newScanButton;
    public Button recreateScanButton;
    public Button howToButton;
    public Button quitButton;
    //public Button homeButton;
    public Label textTest;
    // Start is called before the first frame update


    void Start()
    {
        Debug.Log("Starting UI Script...");
        var root = GetComponent<UIDocument>(); // Get UI document to reference

        
        newScanButton = root.rootVisualElement.Q<Button>("NewScan"); // setting the button to the var
        recreateScanButton = root.rootVisualElement.Q<Button>("RecreateScan"); // setting the button to the var
        howToButton = root.rootVisualElement.Q<Button>("HowTo");
        quitButton = root.rootVisualElement.Q<Button>("Quit");
        //homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the button to the var
        textTest = root.rootVisualElement.Q<Label>("TextTest"); // setting the text to the var

        newScanButton.clicked += NewScanButtonPressed; // make button call function
        recreateScanButton.clicked += RecreateScanButtonPressed; // make button call function
        howToButton.clicked += HowToButtonPressed;
        quitButton.clicked += QuitButtonPressed;

    }
    void QuitButtonPressed()
    {
        Debug.Log("quit button");
        Application.Quit();
    }
    void HowToButtonPressed() // when "How To" Button is pressed
    {
        Debug.Log("how to func");
        SceneManager.LoadScene("HowToScene");// load next scene
    }
    void NewScanButtonPressed(){ // when "New Scan" Button is pressed
        Debug.Log("new scan func");
        SceneManager.LoadScene("NewScanScene"); // load next scene
    }
    void RecreateScanButtonPressed(){ // when "Recreate Scene" Button is pressed
        Debug.Log("recreate scan func");
        SceneManager.LoadScene("RecreateScanScene"); // load next scene
    }
}
