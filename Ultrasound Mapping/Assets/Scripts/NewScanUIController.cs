using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;


public class NewScanUIController : MonoBehaviour
{

    public Button homeButton;
    public Button zeroButton;
    public Label connectText;
    public GameObject lpmsModel;
    int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        connectText = root.rootVisualElement.Q<Label>("ConnectLabel");
        zeroButton = root.rootVisualElement.Q<Button>("ZeroButton");
        //fileManagerObject = FindObjectOfType<LoadFileController>();
        homeButton.clicked += homeButtonPressed; // make button call function
        zeroButton.clicked += zeroButtonPressed;
        Debug.Log(count);
        count++;
    }

 
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
        connectText.style.color = new StyleColor(Color.green);
        //connectText.text.color = Color.green;
    }

    void zeroButtonPressed()
    {
        Debug.Log("Zero button pressed");
        if (lpmsModel != null) // check if object has been assigned
        {
            // Rotate the targetObject around the Y-axis
            lpmsModel.transform.rotation = Quaternion.Euler(90f, -90f, 0f);
        }
        else
        {
            // if not assigned object
            Debug.Log("LPMS Object not assigned");
        }
    }

}
