using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class UpdateFieldColor : MonoBehaviour
    {
        private Image thisImage;

        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void OnEnable()
        {
            thisImage.color = GlobalValues.FieldColor;
        }
    }
}
