using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public static Action<bool> OnGetW;
    public static Action<bool> OnGetA;
    public static Action<bool> OnGetS;
    public static Action<bool> OnGetD;
    public static Action OnGetSpace;
    private bool movementEnabled = true;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void EnableMovementControls(bool isEnabled)
    {
        movementEnabled = isEnabled;
    }


    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                OnGetW?.Invoke(true);
            }
            else
            {
                OnGetW?.Invoke(false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                OnGetA?.Invoke(true);
            }
            else
            {
                OnGetA?.Invoke(false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                OnGetS?.Invoke(true);
            }
            else
            {
                OnGetS?.Invoke(false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                OnGetD?.Invoke(true);
            }
            else
            {
                OnGetD?.Invoke(false);
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnGetSpace?.Invoke();
        }
    }
}
