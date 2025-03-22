using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Pilot : MonoBehaviour
{
    #region [ Input Action References ]
    [SerializeField] private InputActionReference _leftKeyDown;
    [SerializeField] private InputActionReference _rightKeyDown;
    #endregion

    #region [ Actions ]
    public Action<float> StabilisationUpdate;
    public Action<float> CountdownUpdate;
    public Action LeftButtonPressed;
    public Action RightButtonPressed;
    #endregion

    #region [ Properties ]
    private float Stabilisation
    {
        get => _stabilisation;
        set
        {
            _stabilisation = value;
            StabilisationUpdate?.Invoke(value);
        }
    }
    private float _stabilisation;
    private int _sign = 1;
    private float _destabilisationDisplacement = 0.25f;
    private float _stabilisationTimer = 0;
    private float Countdown
    {
        get => _countdown;
        set
        {
            _countdown = value;
            CountdownUpdate?.Invoke(value);
        }
    }
    private float _countdown;
    #endregion
    

    private void OnEnable() => RegisterCallbacks();
    private void OnDisable() => UnregisterCallbacks();

    private void Start()
    {
        SetInitialValues();
        StartCoroutine(UnstableBehaviourCoroutine());
    }

    private void SetInitialValues()
    {
        Stabilisation = Random.Range(-20f, 20f);
        _countdown = 10;
    }

    private void RegisterCallbacks()
    {
        _leftKeyDown.action.performed += OnLeftKeyPressed;
        _rightKeyDown.action.performed += OnRightKeyPressed;
    }

    private void UnregisterCallbacks()
    {
        _leftKeyDown.action.performed -= OnLeftKeyPressed;
        _rightKeyDown.action.performed -= OnRightKeyPressed;
    }

    private void OnLeftKeyPressed(InputAction.CallbackContext callbackContext)
    {
        Stabilisation = Mathf.Clamp(_stabilisation + 5f, -45, 45); 
        LeftButtonPressed?.Invoke();
    }
    
    private void OnRightKeyPressed(InputAction.CallbackContext callbackContext)
    {
        Stabilisation = Mathf.Clamp(_stabilisation - 5f, -45, 45); 
        RightButtonPressed?.Invoke();
    }

    private IEnumerator UnstableBehaviourCoroutine()
    {
        bool _stabilised = false;
        
        while (true)
        {
            if (Random.Range(0f, 1f) > 0.9f) _sign *= -1;
            Stabilisation = Mathf.Clamp(_stabilisation + (_destabilisationDisplacement * _sign), -45, 45);

            if (Stabilisation is < -20 or > 20) _stabilisationTimer = 0;
            else _stabilisationTimer += Time.deltaTime;

            if (_stabilisationTimer > 5f)
            {
                _stabilised = true;
                break;
            }

            if (Countdown <= 0)
            {
                _stabilised = (Stabilisation is > -20 and < 20);
                break;
            }

            Countdown -= Time.deltaTime;
            yield return null;
        }

        Debug.Log(_stabilised ? "Completed" : "Failed");

        Stabilisation = 0;
        UnregisterCallbacks();
    }
}
