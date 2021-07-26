using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mapinfo
{
    class Info
    {
        public Difficulty DifficultyInfo=new Difficulty();
        public General GeneralInfo=new General();
        public Metadata MetadataInfo=new Metadata();
        public EditorSettings EditorInfo=new EditorSettings();
        public Events EventsInfo = new Events();
        public List<TimingPoints> TimePoints = new List<TimingPoints>();

        public void UpdateTimingPoints()
        {
            TimePoints.Sort();
            if (TimePoints[0].isParent == false) 
            { 
                TimePoints[0].isParent = true;
                TimePoints[0].UpdateValues(); 
            }
        }
    }
}
