using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class TimeMark : MonoBehaviour, ICloneable, IComparable
    {
        private GameObject timeMarkLine;
        private AudioSource music;
        private int time_in_ms;
        [SerializeField]public int timestamp;
        public int height;
        public Color color;

        void Awake()
        {
            music = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
            timeMarkLine = GameObject.Find("TimeMarksLine");
        }
        
        void Update()
        {
            time_in_ms = (int)(music.time * 1000);
            int? ttt = MathFuncs.GetMarkX(timestamp, (int)(timeMarkLine.transform.localPosition.x - timeMarkLine.GetComponent<RectTransform>().rect.width / 2), (int)(timeMarkLine.transform.localPosition.x + timeMarkLine.GetComponent<RectTransform>().rect.width / 2) - 100, time_in_ms - GlobalValues.AR_in_ms, time_in_ms + GlobalValues.AR_in_ms);

            if (!ttt.HasValue) { DestroyFromScreen(); Destroy(gameObject); return; }
            transform.localPosition = new Vector3(ttt.Value, timeMarkLine.transform.localPosition.y,0);
        }
        public int GetTimestamp()
        {
            return timestamp;
        }

        public void SetTimestamp(int time)
        {
            timestamp = time;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public int CompareTo(object o)
        {
                return timestamp.CompareTo((o as TimeMark).timestamp);
        }

        private void DestroyFromScreen()
        {
            ControllerApproachingTimeMarks.DeleteTimestampFromScreen(timestamp);
        }
    }
}
