using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{

    public Button recreateScanButton;
    public Button howToButton;
    public Button quitButton;
    public Label textTest;
    // Start is called before the first frame update


    void Start()
    {
        Debug.Log("Starting UI Script...");
        var root = GetComponent<UIDocument>(); // Get UI document to reference

        
        recreateScanButton = root.rootVisualElement.Q<Button>("RecreateScan"); // setting the button to the var
        howToButton = root.rootVisualElement.Q<Button>("HowTo");
        quitButton = root.rootVisualElement.Q<Button>("Quit");
        textTest = root.rootVisualElement.Q<Label>("TextTest"); // setting the text to the var

        //newScanButton.clicked += NewScanButtonPressed; // make button call function
        recreateScanButton.clicked += RecreateScanButtonPressed; // make button call function
        howToButton.clicked += HowToButtonPressed;
        quitButton.clicked += QuitButtonPressed;
    }

    void RecreateScanButtonPressed(){ // when "Recreate Scene" Button is pressed
        Debug.Log("recreate scan func");
        SceneManager.LoadScene("RecreateScanScene"); // load next scene
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
}
