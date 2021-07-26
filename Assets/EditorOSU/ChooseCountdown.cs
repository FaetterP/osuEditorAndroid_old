using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class ChooseCountdown : MonoBehaviour
    {
        private Toggle thisToggle;
        private ChooseCountdown thisCC;
        [SerializeField] private List<Toggle> others;

        void Awake()
        {
            thisToggle = GetComponent<Toggle>();
            thisCC = GetComponent<ChooseCountdown>();
            thisToggle.onValueChanged.AddListener(delegate { OffOthers(); });
        }

        public void Disable()
        {
            thisToggle.isOn = false;
            thisToggle.interactable = true;
            var c = thisToggle.colors;
            c.normalColor = Color.white;
        }

        public void Enable()
        {
            thisToggle.isOn = false;
            thisToggle.interactable = false;
            var c = thisToggle.colors;
            c.normalColor = Color.gray;
        }

        private void OffOthers()
        {
            if (thisToggle.isOn)
            {
                foreach (var t in others)
                {
                    t.isOn = false;
                }
            }
        }
    }
}
