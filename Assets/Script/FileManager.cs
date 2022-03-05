using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public string[] GetListLevels()
    {
        return Directory.GetFiles("Assets/Resources/map", "*.csv");
    }
}
