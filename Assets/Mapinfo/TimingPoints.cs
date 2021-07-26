using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mapinfo
{
    class TimingPoints : ICloneable, IComparable
    {
        public bool isParent;
        public int time, meter, sampleSet, sampleIndex, volume;
        public decimal beatLength;
        public bool uninherited, kiai;
        public double bpm, mult;

        public void UpdateValues()
        {                      
            if (isParent)
            {
                mult = 1;
                beatLength = Math.Abs(beatLength);
                bpm = 60000 / (double)beatLength;
            }
            else
            {
                mult = 100.0 / (double)Math.Abs(beatLength);
            }
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public int CompareTo(object o)
        {
            return time.CompareTo((o as TimingPoints).time);
        }
    }
}
