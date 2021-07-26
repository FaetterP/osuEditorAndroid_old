using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace Assets
{
    class CreateNewMap : MonoBehaviour
    {
        void Start()
        {
            string path = Application.persistentDataPath;
            DirectoryInfo df = new DirectoryInfo(path);
            GlobalValues.GlobalMap = new Map(path + "/beatmap-" + df.GetDirectories().Length + "/");
            Directory.CreateDirectory(path + "/beatmap-" + df.GetDirectories().Length + "/");
        }
    }
}
