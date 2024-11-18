using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCursorUnlocker : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
