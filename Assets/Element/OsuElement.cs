using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Element
{
    class OsuElement : MonoBehaviour, IComparable
    {
        public int timestamp;
        public int CompareTo(object o)
        {
            return timestamp.CompareTo((o as OsuElement).timestamp);
        }
    }
}
