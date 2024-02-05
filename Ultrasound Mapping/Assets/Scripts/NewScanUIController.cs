using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;


public class NewScanUIController : MonoBehaviour
{

    public Button homeButton;
    public Label connectText;
    int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        connectText = root.rootVisualElement.Q<Label>("ConnectLabel");
        //fileManagerObject = FindObjectOfType<LoadFileController>();
        homeButton.clicked += homeButtonPressed; // make button call function
        Debug.Log(count);
        count++;
    }

    // Update is called once per frame
    void homeButtonPressed(){
        //fileManagerObject.FileBrowser.HideDialog(true);
        //FileBrowser.HideDialog(true);
        Debug.Log("Home button pressed");
        SceneManager.LoadScene("MainMenuScene");

    }
    public void IMUConnected(){
        Debug.Log("IMU CONNECTED FUNC CALLED");
        Debug.Log("Set Text to COnnected");
        connectText.text = "IMU Connected";
        //connectText.text.color = Color.green;
    }
}
