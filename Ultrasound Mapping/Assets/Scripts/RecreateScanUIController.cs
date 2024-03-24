using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

// to access CultureInfo
using System.Globalization;

public class RecreateScanUIController : MonoBehaviour
{

    public Button homeButton;
    public Button loadButton;
    public FloatField targetX;
    public FloatField targetY;
    public FloatField targetZ;

    // irf-todo: add these to the UI
    public FloatField XAngle;
    public FloatField YAngle;
    public FloatField ZAngle;

    // public GameObject lpmsModel;
    public GameObject lpmsModel_grayed;
    public LoadFileController fileManagerObject;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>(); // Get UI document to reference
        homeButton = root.rootVisualElement.Q<Button>("HomeButton"); // setting the text to the var
        loadButton = root.rootVisualElement.Q<Button>("LoadButton");
        targetX = root.rootVisualElement.Q<FloatField>("TargetX");
        targetY = root.rootVisualElement.Q<FloatField>("TargetY");
        targetZ = root.rootVisualElement.Q<FloatField>("TargetZ");

        lpmsModel_grayed = GameObject.Find("lpms-cu3(grayed)");

        // fileManagerObject = FindObjectOfType<LoadFileController>();
        homeButton.clicked += homeButtonPressed; // make button call function
        loadButton.clicked += loadButtonPressed; // assign the appropriate callback function.

    }


    void Update()
    {
        // XAngle.value = (lpmsModel.transform.rotation.x) * 180;
        // YAngle.value = (lpmsModel.transform.rotation.y) * 180;
        // ZAngle.value = (lpmsModel.transform.rotation.z) * 180;
    }

    void homeButtonPressed(){
        //fileManagerObject.FileBrowser.HideDialog(true);
        FileBrowser.HideDialog(true);
        Debug.Log("Home button pressed");
        SceneManager.LoadScene("MainMenuScene");
    }

    void loadButtonPressed()
    {
        Debug.Log("load scan button pressed");
        // string paths = "/";
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //                            () => { Debug.Log( "Canceled" ); },
        //                            FileBrowser.PickMode.Files, false, null, null, "Select File", "Select" );
        StartCoroutine( ShowLoadDialogCoroutine() );
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
}
