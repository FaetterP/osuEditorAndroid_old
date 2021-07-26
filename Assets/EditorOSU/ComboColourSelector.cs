using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class ComboColourSelector : MonoBehaviour
    {
        [SerializeField] private Slider r_slider, g_slider, b_slider;
        [SerializeField] private Text num;
        private Image thisImage;
        public int number_of_selected_color=0;

        void Awake()
        {
            thisImage = GetComponent<Image>();
            r_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
            g_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
            b_slider.onValueChanged.AddListener(delegate { UpdateColor(); });
        }


        void Start()
        {
            Color c = GlobalValues.GlobalMap.ComboColors[number_of_selected_color];
            SetSliders(c.r, c.g, c.b);
            UpdateColor();
            UpdateTextNum();
        }

        public void UpdateColor()
        {
            Color newCol = new Color(r_slider.value/255, g_slider.value/255, b_slider.value/255);
            thisImage.color = newCol;
            GlobalValues.GlobalMap.ComboColors[number_of_selected_color] = new Color(r_slider.value, g_slider.value, b_slider.value);
        }

        public void SetSliders(float r, float g, float b)
        {
            r_slider.value = r;
            g_slider.value = g;
            b_slider.value = b;
        }
        public void UpdateTextNum()
        {
            num.text = (number_of_selected_color+1).ToString();
        }

    }
}
