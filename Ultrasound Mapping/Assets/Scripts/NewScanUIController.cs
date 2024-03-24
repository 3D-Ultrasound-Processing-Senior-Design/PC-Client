using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;
using System.IO;
using UnityEditor;
using System.Text;

// irf-question: do we need this? doesn't seem to be referenced anywhere
public class Scan
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}


public class NewScanUIController : MonoBehaviour
{

    public Button homeButton;
    public Button zeroButton;
    public Label connectText;
    public GameObject lpmsModel;
    public FloatField XAngle;
    public FloatField YAngle;
    public FloatField ZAngle;
    public Button saveButton;
    int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        connectText = root.rootVisualElement.Q<Label>("ConnectLabel");
        zeroButton = root.rootVisualElement.Q<Button>("ZeroButton");
        XAngle = root.rootVisualElement.Q<FloatField>("XAngle");
        YAngle = root.rootVisualElement.Q<FloatField>("YAngle");
        ZAngle = root.rootVisualElement.Q<FloatField>("ZAngle");
        saveButton = root.rootVisualElement.Q<Button>("SaveButton");
        //fileManagerObject = FindObjectOfType<LoadFileController>();
        homeButton.clicked += homeButtonPressed; // make button call function
        zeroButton.clicked += zeroButtonPressed;
        saveButton.clicked += saveButtonPressed;
        Debug.Log(count);
        count++;
    }

    void Update()
    {
        XAngle.value = (lpmsModel.transform.rotation.x) * 180;
        YAngle.value = (lpmsModel.transform.rotation.y) * 180;
        ZAngle.value = (lpmsModel.transform.rotation.z) * 180;
    }
    void homeButtonPressed(){
        //fileManagerObject.FileBrowser.HideDialog(true);
        //FileBrowser.HideDialog(true);
        Debug.Log("Home button pressed");
        SceneManager.LoadScene("MainMenuScene");

    }
    void saveButtonPressed(){
        Debug.Log("save button pressed");
        string title = "Save File";
        string directory = "/ ";
        string defaultName = "scan";
        string extension = ".csv";
        string path = EditorUtility.SaveFilePanelInProject("Save csv", defaultName, "csv",
            "Please enter a file name to save the scan to");
        string scanData = XAngle.value + "," + YAngle.value + "," + ZAngle.value;
        if (path.Length != 0)
        {
            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(scanData);
            }
            // As we are saving to the asset folder, tell Unity to scan for modified or new assets
            AssetDatabase.Refresh();
        }
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
