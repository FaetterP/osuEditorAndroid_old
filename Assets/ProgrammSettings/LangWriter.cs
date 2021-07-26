using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.ProgrammSettings
{
    class LangWriter : MonoBehaviour
    {
        [SerializeField] private string en, ru;

        void Awake()
        {
            Text TextOnThisObj = GetComponent<Text>();
            switch (Lang.CurrentLanguage)
            {
                case Language.EN:
                    TextOnThisObj.text = en;
                    break;

                case Language.RU:
                    TextOnThisObj.text = ru;
                    break;
            }
        }
    }
}
