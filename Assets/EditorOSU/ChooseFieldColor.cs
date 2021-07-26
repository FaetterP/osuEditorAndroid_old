using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class ChooseFieldColor : MonoBehaviour
    {
        [SerializeField] private Slider r_slider, g_slider, b_slider;

        void Awake()
        {
            r_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
            g_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
            b_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
        }

        public void UpdateColor()
        {
            Color newCol = new Color(r_slider.value / 255, g_slider.value / 255, b_slider.value / 255);
            GlobalValues.FieldColor = newCol;
            GetComponent<Image>().color = newCol;
        }
    }
}
