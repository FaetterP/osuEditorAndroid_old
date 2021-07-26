using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class CountdownEnabler : MonoBehaviour
    {
        [SerializeField] private List<Toggle> disabled_toogles;
        private Toggle thisToogle;

        void Awake()
        {
            thisToogle = GetComponent<Toggle>();
            thisToogle.onValueChanged.AddListener(delegate { TryDisable(); });
        }
        void Start()
        {
            TryDisable();
        }
    
        private void TryDisable()
        {
            if (thisToogle.isOn)
            {
                foreach(var t in disabled_toogles)
                {
                    t.GetComponent<ChooseCountdown>().Disable();
                }
            }
            else
            {
                foreach (var t in disabled_toogles)
                {
                    t.GetComponent<ChooseCountdown>().Enable();
                }
            }
        }
    }
}
