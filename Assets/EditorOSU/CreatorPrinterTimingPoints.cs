using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.EditorOSU
{
    class CreatorPrinterTimingPoints : MonoBehaviour
    {
        [SerializeField]private PrinterTimingPoint created;
        void Start()
        {
            CreatePrinters();
        }

        private void CreatePrinters()
        {
            foreach(var t in GlobalValues.GlobalMap.Settings.TimePoints)
            {
                PrinterTimingPoint go = Instantiate(created, Vector2.zero, Quaternion.identity, transform);
                go.timing_point = t;
            }
        }

        public void UpdatePrnters()
        {
            foreach(var t in FindObjectsOfType<PrinterTimingPoint>())
            {
                Destroy(t.gameObject);
            }
            CreatePrinters();
        }
    }
}
