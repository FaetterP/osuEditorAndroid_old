using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class SelectTimingPoint : MonoBehaviour
    {
        [SerializeField] private PrinterTimingPoint printer;
        private StorageSaverTimingPoint modifier;

        void Awake()
        {
            modifier = FindObjectOfType<StorageSaverTimingPoint>();
        }

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
            GlobalValues.selected_timing_point = printer.timing_point;
            modifier.bpm.text = printer.timing_point.bpm.ToString();
            modifier.volume.value = printer.timing_point.volume;
            modifier.offset.text = printer.timing_point.time.ToString();
            modifier.kiai.isOn = printer.timing_point.kiai;
        }
    }
}
