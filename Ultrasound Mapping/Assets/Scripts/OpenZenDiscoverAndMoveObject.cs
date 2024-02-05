using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Example Behaviour which applies the measured OpenZen sensor orientation to a 
 * Unity object.
 */
public class OpenZenDiscoverAndMoveObject : MonoBehaviour
{
    struct SensorListResult
    {
        public string IoType;
        public string Identifier;
        public uint BaudRate;
    }
    NewScanUIController uiGameObject; // declare object from new scan UI
    Dropdown mDropdownSensorSelect;
    Button mConnectButton;
    GameObject mObjConnectButton;
    GameObject mConnectCanvas;
    GameObject mErrorConnect;
    Text mTextTitle;
    public bool isConnected = false;

    ZenClientHandle_t mZenHandle = new ZenClientHandle_t();
    List<SensorListResult> mFoundSensors = new List<SensorListResult>();
    ZenSensorHandle_t mSensorHandle;

    // Use this for initialization
    void Start()
    {
        uiGameObject = FindObjectOfType<NewScanUIController>();
        // create OpenZen and start asynchronous sensor discovery
        OpenZen.ZenInit(mZenHandle);
        OpenZen.ZenListSensorsAsync(mZenHandle);

        // mDropdownSensorSelect = GameObject.Find("dropdownSensorSelect").GetComponent<Dropdown>();
        // mObjConnectButton = GameObject.Find("buttonConnectSensor");
        // mErrorConnect = GameObject.Find("txtErrorConnect");
        // mConnectButton = mObjConnectButton.GetComponent<Button>();
        // mConnectCanvas = GameObject.Find("DiscoverDialog");
        // mTextTitle = GameObject.Find("txtTitle").GetComponent<Text>();

        // mObjConnectButton.SetActive(false);
        // mErrorConnect.SetActive(false);
        // mConnectButton.onClick.AddListener(onConnectButtonClicked);
    }

    void onConnectButtonClicked()
    {
        Debug.Log("BUTTON CLICKED");
        //int selectedItem = mDropdownSensorSelect.value;
        var sensorConnectTo = mFoundSensors[0];

        mSensorHandle = new ZenSensorHandle_t();
        // connect to the first available sensor in the list of found sensors
        print("Trying to connect to sensor " + sensorConnectTo.Identifier);

        // obtain selected sensor which will start the data streaming from 
        // that sensor
        var sensorInitError = OpenZen.ZenObtainSensorByName(mZenHandle,
            sensorConnectTo.IoType,
            sensorConnectTo.Identifier,
            sensorConnectTo.BaudRate,
            mSensorHandle);
        if (sensorInitError == ZenSensorInitError.ZenSensorInitError_None)
        {
            print("Succesfully connected to sensor");
            uiGameObject.IMUConnected();
            //mConnectCanvas.SetActive(false);
            //mErrorConnect.SetActive(false);

            ZenComponentHandle_t mComponent = new ZenComponentHandle_t();
            OpenZen.ZenSensorComponentsByNumber(mZenHandle, mSensorHandle, OpenZen.g_zenSensorType_Imu, 0, mComponent);

            // enable sensor streaming, normally on by default anyways
            OpenZen.ZenSensorComponentSetBoolProperty(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_StreamData, true);

            // set the sampling rate to 100 Hz
            OpenZen.ZenSensorComponentSetInt32Property(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_SamplingRate, 100);

            // filter mode using accelerometer & gyroscope & magnetometer
            OpenZen.ZenSensorComponentSetInt32Property(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_FilterMode, 2);

            // Ensure the Orientation data is streamed out
            OpenZen.ZenSensorComponentSetBoolProperty(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_OutputQuat, true);

            print("Sensor configuration complete");
        }
        else
        {
            //mErrorConnect.SetActive(true);
            mSensorHandle = null;
            print("Cannot connect to sensor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // run as long as there are new OpenZen events to process
        while (true)
        {
            ZenEvent zenEvent = new ZenEvent();
            // read all events which are waiting for us
            // use the rotation from the newest IMU event
            if (!OpenZen.ZenPollNextEvent(mZenHandle, zenEvent))
                break;

            // if compontent handle = 0, this is a OpenZen wide event,
            // like sensor search
            if (zenEvent.component.handle == 0)
            {
                // if the handle is on, its not a sensor event but a system wide 
                // event
                switch (zenEvent.eventType)
                {
                    case ZenEventType.ZenEventType_SensorFound:
                        Debug.Log("ZenSensorEvent_SensorFound, sensor name: " + zenEvent.data.sensorFound.name + 
                            " identifier: " + zenEvent.data.sensorFound.identifier + 
                            " IoType : " + zenEvent.data.sensorFound.ioType);
                        print("ZenSensorEvent_SensorFound, sensor name: " + zenEvent.data.sensorFound.name + 
                            " identifier: " + zenEvent.data.sensorFound.identifier + 
                            " IoType : " + zenEvent.data.sensorFound.ioType);

                        // store all found sensors in a local list
                        SensorListResult localDesc = new SensorListResult();
                        localDesc.Identifier = zenEvent.data.sensorFound.identifier;
                        localDesc.IoType = zenEvent.data.sensorFound.ioType;
                        localDesc.BaudRate = zenEvent.data.sensorFound.baudRate;
                        mFoundSensors.Add(localDesc);

                        // add the found sensor to the dropdown selection menu
                        List<string> dropOptions = new List<string>();
                        //dropOptions.Add(zenEvent.data.sensorFound.name);
                        //mDropdownSensorSelect.AddOptions(dropOptions);
                        Debug.Log("CALL BUTTON CLICKED PSEUDO");
                        onConnectButtonClicked();
                        break;
                    case ZenEventType.ZenEventType_SensorListingProgress:
                        if (zenEvent.data.sensorListingProgress.complete > 0)
                        {
                            //mTextTitle.text = "Please select Sensor";
                            //mObjConnectButton.SetActive(true);
                        }
                        break;
                }
            }
            else
            {
                switch (zenEvent.eventType)
                {
                    case ZenEventType.ZenEventType_ImuData:
                        // read acceleration
                        OpenZenFloatArray fa = OpenZenFloatArray.frompointer(zenEvent.data.imuData.a);
                        // read euler angles
                        OpenZenFloatArray fr = OpenZenFloatArray.frompointer(zenEvent.data.imuData.r);
                        // read quaternion
                        OpenZenFloatArray fq = OpenZenFloatArray.frompointer(zenEvent.data.imuData.q);

                        // Unity Quaternion constructor has order x,y,z,w
                        // Furthermore, y and z axis need to be flipped to 
                        // convert between the LPMS and Unity coordinate system
                        Quaternion sensorOrientation = new Quaternion(fq.getitem(1),
                                                                    -fq.getitem(2),
                                                                    -fq.getitem(3),
                                                                    fq.getitem(0));
                        transform.rotation = sensorOrientation;
                        break;
                }
            }
        }
    }

    void OnDestroy()
    {
        if (mSensorHandle != null)
        {
            OpenZen.ZenReleaseSensor(mZenHandle, mSensorHandle);
        }
        OpenZen.ZenShutdown(mZenHandle);
    }

}
