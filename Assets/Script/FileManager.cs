using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public int GetListLevels()
    {
        return Resources.LoadAll("map/").Length;
    }
}
