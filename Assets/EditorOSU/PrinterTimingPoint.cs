using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Element;
using Assets.Mapinfo;

namespace Assets.EditorOSU
{
    class PrinterTimingPoint : MonoBehaviour
    {
        [SerializeField] private Text bpm,offsset;
        [SerializeField] Sprite kiai_on, kiai_off;
        [SerializeField] private Image kiai, point;
        [NonSerialized] public TimingPoints timing_point;

        void Start()
        {
            if (timing_point.isParent) { point.color = Color.red; }
            else { point.color = Color.green; }

            if (timing_point.isParent) { bpm.text = timing_point.bpm.ToString(); }
            else { bpm.text = "x" + timing_point.mult.ToString(); }
            offsset.text = MathFuncs.ConvertTimestampToSring(timing_point.time);

            if (timing_point.kiai) { kiai.sprite = kiai_on; }
            else { kiai.sprite = kiai_off; }
        }
    }
}
