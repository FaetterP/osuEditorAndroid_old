using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mapinfo
{
    
    class UpdateValues : MonoBehaviour
    {
        private Text thisText;
        [SerializeField] private Slider slider;

        void Awake()
        {
            thisText = GetComponent<Text>();
        }

        void Update()
        {
            thisText.text = System.Math.Round(slider.value,2).ToString();
        }
    }
}
