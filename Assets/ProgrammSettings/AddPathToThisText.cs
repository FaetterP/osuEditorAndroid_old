using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ProgrammSettings
{
    class AddPathToThisText : MonoBehaviour
    {
        private Text text;

        void Awake()
        {
            text = GetComponent<Text>();
        }

        void Start()
        {
            text.text += Application.persistentDataPath;
        }
    }
}
