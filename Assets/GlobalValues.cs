using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Mapinfo;
using Assets.ProgrammSettings;
using Assets.Element;
using UnityEngine;

namespace Assets
{
    class GlobalValues
    {
        public static Map GlobalMap=new Map("");
        public static string nameOfDiff;
        public static string currenfFilePath;
        public static int AR_in_ms;
        public static Color FieldColor = Color.white;
        public static OsuElement Selected_Element;
        public static string Status, sliderStatus;
        public static TimingPoints selected_timing_point;
    }
}
