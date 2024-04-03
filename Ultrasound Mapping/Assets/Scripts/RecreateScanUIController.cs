using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;
using System.IO;
using UnityEditor;
using System.Text;

// to access CultureInfo
using System.Globalization;

public class RecreateScanUIController : MonoBehaviour
{
    public Quaternion zeroOffset = Quaternion.identity;
    public GameObject xEulerAngleObject;
        

    public Button homeButton;
    public Button loadButton;
    public Button zeroButton;
    public Button saveButton;

    // these three for the "grayed out" model.
    public FloatField targetX;
    public FloatField targetY;
    public FloatField targetZ;

    // irf-todo: add these to the UI (for the opaque model)
    public FloatField XAngle;
    public FloatField YAngle;
    public FloatField ZAngle;

    public Label connectText;

    public GameObject lpmsModel;
    public GameObject lpmsModel_grayed;
    public LoadFileController fileManagerObject;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        loadButton = root.rootVisualElement.Q<Button>("LoadButton");
        connectText = root.rootVisualElement.Q<Label>("ConnectLabel");
        zeroButton = root.rootVisualElement.Q<Button>("ZeroButton");
        saveButton = root.rootVisualElement.Q<Button>("SaveButton");

        targetX = root.rootVisualElement.Q<FloatField>("TargetX");
        targetY = root.rootVisualElement.Q<FloatField>("TargetY");
        targetZ = root.rootVisualElement.Q<FloatField>("TargetZ");

        XAngle = root.rootVisualElement.Q<FloatField>("actualX");
        YAngle = root.rootVisualElement.Q<FloatField>("actualY");
        ZAngle = root.rootVisualElement.Q<FloatField>("actualZ");

        lpmsModel = GameObject.Find("lpms-cu3");
        lpmsModel_grayed = GameObject.Find("lpms-cu3(grayed)");

        homeButton.clicked += homeButtonPressed; // make button call function
        loadButton.clicked += loadButtonPressed; // assign the appropriate callback function.
        zeroButton.clicked += zeroButtonPressed;
        saveButton.clicked += saveButtonPressed;
    }


    void Update()
    {
        // irf-todo: check that the opaque model works when you have the actual imu on hand.

        //Debug.Log("Euler Angles: " + lpmsModel.transform.eulerAngles);
        XAngle.value = (float)xEulerAngleObject.GetComponent<OpenZenDiscoverAndMoveObject>().xEulerAngle;
        //Debug.Log(" value imported : " + xEulerAngleObject.GetComponent<OpenZenDiscoverAndMoveObject>().xEulerAngle);
        //XAngle.value = lpmsModel.transform.eulerAngles.x; //lpmsModel.transform.eulerAngles.x;
        YAngle.value = (float)xEulerAngleObject.GetComponent<OpenZenDiscoverAndMoveObject>().yEulerAngle; // 
        /*if (YAngle.value > 180)
        {
            YAngle.value = YAngle.value - 360;
        }*/
        ZAngle.value = (float)xEulerAngleObject.GetComponent<OpenZenDiscoverAndMoveObject>().zEulerAngle; // lpmsModel.transform.eulerAngles.z;
        //YAngle.value = (lpmsModel.transform.rotation.y) * 180;
        //ZAngle.value = (lpmsModel.transform.rotation.z) * 180;

    }

    void homeButtonPressed(){
        FileBrowser.HideDialog(true);
        Debug.Log("Home button pressed");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void IMUConnected(){
        Debug.Log("re create scan imu connected function called...");
        Debug.Log("Set Text to COnnected");
        connectText.text = "IMU Connected";
        connectText.style.color = new StyleColor(Color.green);
        //connectText.text.color = Color.green;
    }
    void saveButtonPressed()
    {
        Debug.Log("save scan button pressed");

        StartCoroutine(ShowSaveDialogCoroutine());
    }
    IEnumerator ShowSaveDialogCoroutine()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, true, null, null, "Save", "Save");
        Debug.Log(FileBrowser.Success);
        Debug.Log("file saved successfully ");
        if (FileBrowser.Success)
            OnFilesSelectedSave(FileBrowser.Result);
    }
    void loadButtonPressed()
    {
        Debug.Log("load scan button pressed");
        
        // simple file manager co-routine
        StartCoroutine( ShowLoadDialogCoroutine() );
    }
    void zeroButtonPressed()
    {
        zeroOffset = lpmsModel.transform.rotation*zeroOffset;
        //Debug.Log("zero offset: " + zeroOffset);
    }

    IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: file, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load" );

		// Dialog is closed
		// Print whether the user has selected some files or cancelled the operation (FileBrowser.Success)
		Debug.Log( FileBrowser.Success );

        Debug.Log("file selected success");

		if( FileBrowser.Success )
        {
            Debug.Log("file selected success");
			OnFilesSelected( FileBrowser.Result ); // FileBrowser.Result is null, if FileBrowser.Success is false
        }
	}
	
	void OnFilesSelected( string[] filePaths )
	{
		// Print paths of the selected files
		for( int i = 0; i < filePaths.Length; i++ )
			Debug.Log( filePaths[i] );

		// Get the file path of the first selected file
		string filePath = filePaths[0];

		// Read the bytes of the first file via FileBrowserHelpers
		// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
		byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( filePath );

        // use a Stream Reader to read the contents of the file line-by-line
        StreamReader reader = null;
        reader = new StreamReader(File.OpenRead( filePath ));
        
        // apparently C# has a garbage collector so we don't need to delete this.
        List<float> listA = new List<float>();
        while (!reader.EndOfStream)
        {   
            var line = reader.ReadLine();
            var values = line.Split(',');
            foreach (var item in values)
            {
                listA.Add( float.Parse( item, CultureInfo.InvariantCulture.NumberFormat) );
            }
            foreach (var coloumn1 in listA)
            {
                targetX.value = listA[0];
                targetY.value = listA[1];
                targetZ.value = listA[2];
                Debug.Log( coloumn1 );
            }
        }

        // move the grayed out model to the correct orientation
        lpmsModel_grayed.transform.rotation = Quaternion.Euler(targetX.value, targetY.value, targetZ.value);

        // Or, copy the first file to persistentDataPath
		string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( filePath ) );
		FileBrowserHelpers.CopyFile( filePath, destinationPath );
	}
    void OnFilesSelectedSave(string[] filepaths)
    {

        // Print paths of the selected files
        for (int i = 0; i < filepaths.Length; i++)
            Debug.Log(filepaths[i]);

        // Get the file path of the first selected file
        string filePath = filepaths[0];

        string scanData = lpmsModel.transform.rotation.x.ToString() + ","+ lpmsModel.transform.rotation.y.ToString() +","+ lpmsModel.transform.rotation.z.ToString() +","+ lpmsModel.transform.rotation.w.ToString();

        if (filePath.Length != 0)
        {
            using (var stream = File.Open(filePath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(scanData);
            }
        }
    }
}
