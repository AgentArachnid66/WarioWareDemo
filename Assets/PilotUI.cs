using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PilotUI : MonoBehaviour
{
    #region [ UXML ]
    private VisualElement _root;
    private VisualElement _controllerVisualElement;
    private VisualElement _unstableCloudsVisualElement;
    private VisualElement _pointer;
    private Label _countdownLabel;
    #endregion

    #region [ Properties ]
    private Pilot _pilot;
    private float _controllerRotation;
    #endregion

    
    
    private void Awake()
    {
        _pilot = FindObjectOfType<Pilot>();
        
        _root = GetComponent<UIDocument>().rootVisualElement;
        _controllerVisualElement = _root.Q<VisualElement>("Controller");
        _unstableCloudsVisualElement = _root.Q<VisualElement>("UnstableClouds");
        _pointer = _root.Q<VisualElement>("Pointer");
        _countdownLabel = _root.Q<Label>("CountdownLabel");
    }

    private void OnEnable()
    {
        _pilot.StabilisationUpdate += OnStabilityValueChanged;
        _pilot.LeftButtonPressed += OnLeftButtonPressed;
        _pilot.RightButtonPressed += OnRightButtonPressed;
        _pilot.CountdownUpdate += OnCountdownUpdate;
    }

    private void OnStabilityValueChanged(float value) => _pointer.transform.rotation = Quaternion.Euler(0, 0, value);
    private void OnLeftButtonPressed() => _controllerRotation = Mathf.Clamp(_controllerRotation + 10, -45, 45);
    private void OnRightButtonPressed() => _controllerRotation = Mathf.Clamp(_controllerRotation - 10, -45, 45);
    private void OnCountdownUpdate(float value) => _countdownLabel.text = $"{Mathf.CeilToInt(value)}";
    
    private void Update()
    {
        _controllerRotation = Mathf.Lerp(0, _controllerRotation, 0.95f);
            
        _controllerVisualElement.transform.rotation = Quaternion.Euler(0, 0, _controllerRotation);
        _unstableCloudsVisualElement.transform.rotation = Quaternion.Euler(0, 0, 360 * Time.timeSinceLevelLoad);
    }
}
