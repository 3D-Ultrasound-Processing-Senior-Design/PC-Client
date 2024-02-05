using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;


public class RecreateScanUIController : MonoBehaviour
{

    public Button homeButton;
    public LoadFileController fileManagerObject;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        //fileManagerObject = FindObjectOfType<LoadFileController>();
        homeButton.clicked += homeButtonPressed; // make button call function
    }

    // Update is called once per frame
    void homeButtonPressed(){
        //fileManagerObject.FileBrowser.HideDialog(true);
        FileBrowser.HideDialog(true);
        Debug.Log("Home button pressed");
        SceneManager.LoadScene("MainMenuScene");

    }
}
