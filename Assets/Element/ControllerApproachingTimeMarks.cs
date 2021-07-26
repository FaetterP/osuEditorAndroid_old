using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class ControllerApproachingTimeMarks : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        private static List<int> timesOnScreen = new List<int>(), timesOnScreenNote = new List<int>();
        private int musicTime;
        [SerializeField] private GameObject parentCanvas;
        void Update()
        {
            musicTime = (int)(music.time * 1000);

            foreach (TimeMark t in GlobalValues.GlobalMap.TimeMarks)
            {
                {
                    if (t.GetTimestamp() - musicTime <= GlobalValues.AR_in_ms && t.GetTimestamp() - musicTime >= -GlobalValues.AR_in_ms && !timesOnScreen.Contains(t.GetTimestamp()))
                    {
                        timesOnScreen.Add(t.GetTimestamp());
                        TimeMark go = Instantiate(t, new Vector3(10000, 0, 0), Quaternion.identity, parentCanvas.transform);
                        go.SetTimestamp(t.GetTimestamp());
                        go.transform.localScale = new Vector2(1, (float)(t.height) / 100);
                        go.GetComponent<Image>().color = t.color;
                    }
                }
            }

            foreach (var t in GlobalValues.GlobalMap.NoteTimeMarks)
            {
                if (t.GetTimestamp() - musicTime <= GlobalValues.AR_in_ms && t.GetTimestamp() - musicTime >= -GlobalValues.AR_in_ms && !timesOnScreenNote.Contains(t.GetTimestamp()))
                {
                    timesOnScreenNote.Add(t.GetTimestamp());
                    NoteTimeMark go = Instantiate(t, new Vector3(10000, 0, 0), Quaternion.identity, parentCanvas.transform);
                    go.SetTimestamp(t.GetTimestamp());
                    go.GetComponent<NoteTimeMark>().thisNote = (t as NoteTimeMark).thisNote;
                }
            }
        }

        public static void DeleteTimestampFromScreen(int timestamp)
        {
            timesOnScreen.Remove(timestamp);
        }
        public static void DeleteNoteTimestampFromScreen(int timestamp)
        {
            timesOnScreenNote.Remove(timestamp);
        }
    }
}
