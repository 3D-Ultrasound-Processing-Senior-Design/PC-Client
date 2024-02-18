using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Example Behaviour which applies the measured OpenZen sensor orientation to a 
 * Unity object.
 */
public class OpenZenMoveObject : MonoBehaviour
{
    ZenClientHandle_t mZenHandle = new ZenClientHandle_t();
    ZenSensorHandle_t mSensorHandle = new ZenSensorHandle_t();

    public enum  OpenZenIoTypes { SiUsb, Bluetooth };

    [Tooltip("IO Type which OpenZen should use to connect to the sensor.")]
    public OpenZenIoTypes OpenZenIoType = OpenZenIoTypes.SiUsb;
    [Tooltip("Idenfier which is used to connect to the sensor. The name depends on the IO type used and the configuration of the sensor.")]
    public string OpenZenIdentifier = "lpmscu2000573";

    // Use this for initialization
    void Start()
    {
        // create OpenZen
        OpenZen.ZenInit(mZenHandle);

        // Hint: to get the io type and identifer for all connected sensor,
        // you cant start the DiscoverSensorScene. The information of all 
        // found sensors is printed in the debug console of Unity after
        // the search is complete.

        print("Trying to connect to OpenZen Sensor on IO " + OpenZenIoType +
            " with sensor name " + OpenZenIdentifier);

        var sensorInitError = OpenZen.ZenObtainSensorByName(mZenHandle,
            OpenZenIoType.ToString(),
            OpenZenIdentifier,
            0,
            mSensorHandle);
        if (sensorInitError != ZenSensorInitError.ZenSensorInitError_None)
        {
            print("Error while connecting to sensor.");
        } else {
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
    }
    
    // Update is called once per frame
    void Update()
    {
        ZenEvent zenEvent = new ZenEvent();
        // Consume all new OpenZen events
        while (OpenZen.ZenPollNextEvent(mZenHandle, zenEvent))
        {
            // If component handle == 0, this is a OpenZen wide event,
            // like sensor search
            if (zenEvent.component.handle != 0
                && zenEvent.eventType == ZenEventType.ZenEventType_ImuData)
            {
                // read quaternion
                OpenZenFloatArray fq = OpenZenFloatArray.frompointer(zenEvent.data.imuData.q);

                // Unity converts the model to left-handed by flipping the direction of X.
                // The "LPMS World Frame" is +Z up and right-handed, Unity's is +Y up and left-handed.
                // We convert between the two by exchanging the global Y and Z axes.
                // The following calculation accounts for these two transformations.  With this,
                // the model will be displayed in the correct orientation and the Euler angles
                // printed by Unity will correspond to the sensor axes with the sign of X flipped.
                float invSqrt2 = 1 / Mathf.Sqrt(2.0f); // This ensures the result is normalized.
                float w = invSqrt2 * fq.getitem(0);  // OpenZen stores w first.
                float x = invSqrt2 * fq.getitem(1);
                float y = invSqrt2 * fq.getitem(2);
                float z = invSqrt2 * fq.getitem(3);
                transform.rotation = new Quaternion(y - z,  x - w, -w - x, y + z); // Unity order: xyzw
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
