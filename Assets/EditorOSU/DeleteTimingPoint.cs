using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.EditorOSU
{
    class DeleteTimingPoint : MonoBehaviour
    {
        [SerializeField] private PrinterTimingPoint printer;
        private CreatorPrinterTimingPoints creator;

         void Awake()
        {
            creator = FindObjectOfType<CreatorPrinterTimingPoints>();
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
            GlobalValues.GlobalMap.Settings.TimePoints.Remove(printer.timing_point);
            GlobalValues.GlobalMap.Settings.UpdateTimingPoints();
            creator.UpdatePrnters();
        }
    }
}
