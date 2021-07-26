using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class StorageSaverTimingPoint : MonoBehaviour
    {
        [SerializeField] private CreatorPrinterTimingPoints creator;
        [SerializeField] public InputField bpm, offset;
        [SerializeField] public Toggle kiai;
        [SerializeField] public Slider volume;

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    Click();
                }
            }
        }

        void OnMouseDown()
        {
            Click();
        }

        private void Click()
        {
            GlobalValues.selected_timing_point.bpm = double.Parse(bpm.text);
            GlobalValues.selected_timing_point.beatLength = (decimal)(60000.0 / GlobalValues.selected_timing_point.bpm);
            GlobalValues.selected_timing_point.time = int.Parse(offset.text);
            GlobalValues.selected_timing_point.kiai = kiai.isOn;
            GlobalValues.selected_timing_point.volume = (int)volume.value;

            GlobalValues.GlobalMap.Settings.UpdateTimingPoints();
            creator.UpdatePrnters();
        }
    }
}