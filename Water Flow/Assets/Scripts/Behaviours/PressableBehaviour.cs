using System;
using System.Collections.Generic;
using UnityEngine;

public class PressableBehaviour : MonoBehaviour,
    IInputReceiver
{
    [SerializeField]
    private bool _startCheckingOnAwake = true;

    [SerializeField]
    private Collider _collider;

    public List<InputTransmitter> AttachedInputTransmitterList { get; set; }
    public Dictionary<Type, InputTransmitter.EventDelegate> Delegates { get; set; }
    public Dictionary<Delegate, InputTransmitter.EventDelegate> DelegateLookUp { get; set; }

    #region Events
    public Action<PressableBehaviour> OnPressed { get; set; }
    #endregion

    private void Awake()
    {
        if (_startCheckingOnAwake)
            StartCheckingPressed();
    }

    private void OnDestroy()
    {
        StopCheckingPressed();
    }

    public void StartCheckingPressed()
    {
        this.AddInputListener<Input_WI_OnFingerDown>(OnFingerDown);
    }

    public void StopCheckingPressed()
    {
        this.RemoveInputListener<Input_WI_OnFingerDown>(OnFingerDown);
    }

    private void OnFingerDown(Input_WI_OnFingerDown e)
    {
        if (!IsInputOnClickable())
            return;

        Pressed(e);
    }

    public bool IsInputOnClickable()
    {
        if (Utilities.IsObjectTouched(_collider, Camera.main))
        {
            Debug.Log(" Object touched");
            return true;
        }

        return false;
    }

    public void Pressed(Input_WI_OnFingerDown e)
    {
        OnPressed?.Invoke(this);
    }
}
