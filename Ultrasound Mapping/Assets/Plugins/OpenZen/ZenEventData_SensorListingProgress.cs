//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ZenEventData_SensorListingProgress : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ZenEventData_SensorListingProgress(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ZenEventData_SensorListingProgress obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ZenEventData_SensorListingProgress() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          OpenZenPINVOKE.delete_ZenEventData_SensorListingProgress(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public float progress {
    set {
      OpenZenPINVOKE.ZenEventData_SensorListingProgress_progress_set(swigCPtr, value);
    } 
    get {
      float ret = OpenZenPINVOKE.ZenEventData_SensorListingProgress_progress_get(swigCPtr);
      return ret;
    } 
  }

  public char complete {
    set {
      OpenZenPINVOKE.ZenEventData_SensorListingProgress_complete_set(swigCPtr, value);
    } 
    get {
      char ret = OpenZenPINVOKE.ZenEventData_SensorListingProgress_complete_get(swigCPtr);
      return ret;
    } 
  }

  public ZenEventData_SensorListingProgress() : this(OpenZenPINVOKE.new_ZenEventData_SensorListingProgress(), true) {
  }

}
