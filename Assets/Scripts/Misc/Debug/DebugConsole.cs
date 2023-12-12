using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    private void Awake()
    {
        Debug.developerConsoleEnabled = true;
        Debug.developerConsoleVisible = true;
    }
}
