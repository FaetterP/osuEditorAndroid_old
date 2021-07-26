using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class ControllerApproachingDetails : MonoBehaviour
    {
        [SerializeField]private AudioSource music;
        private static List<int> timesOnScreen = new List<int>();
        private int musicTime;
        [SerializeField]private GameObject parentCanvas;
        void Update()
        {
            musicTime = (int)(music.time * 1000);

            foreach(OsuElement t in GlobalValues.GlobalMap.GetAllElements())
            {
                if (t is Note)
                {
                    if (t.timestamp - musicTime <= GlobalValues.AR_in_ms && t.timestamp - musicTime >= 0 && !timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Add(t.timestamp);
                        Vector2 ret = MathFuncs.OsuCoordsToUnity(new Vector2((t as Note).x, (t as Note).y));
                        OsuElement go = Instantiate(t, ret, Quaternion.identity);
                        go.transform.SetParent(parentCanvas.transform, false);
                        go.timestamp = t.timestamp;
                        go.GetComponent<Note>().ComboColorNum = (t as Note).ComboColorNum;
                        go.GetComponent<Note>().number = (t as Note).number;
                    }
                    else if ((t.timestamp - musicTime < 0 || t.timestamp - musicTime > GlobalValues.AR_in_ms) && timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Remove(t.timestamp);
                    }
                }
                else if (t is OsuSlider)
                {

                    if (t.timestamp-GlobalValues.AR_in_ms <= musicTime && t.timestamp + (t as OsuSlider).sum_time*(t as OsuSlider).count_of_slides >= musicTime && !timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Add(t.timestamp);
                        Vector2 ret = MathFuncs.OsuCoordsToUnity(new Vector2((t as OsuSlider).x_start, (t as OsuSlider).y_start));
                        OsuElement go = Instantiate(t, new Vector3(ret.x, ret.y, 0), Quaternion.identity);
                        go.GetComponent<OsuSlider>().points = (t as OsuSlider).points;
                        go.transform.SetParent(parentCanvas.transform, false);
                        go.timestamp = t.timestamp;
                        go.GetComponent<OsuSlider>().sum_time = (t as OsuSlider).sum_time;
                        go.GetComponent<OsuSlider>().x_start = (t as OsuSlider).x_start;
                        go.GetComponent<OsuSlider>().y_start = (t as OsuSlider).y_start;
                        go.GetComponent<OsuSlider>().count_of_slides = (t as OsuSlider).count_of_slides;
                        go.GetComponent<OsuSlider>().ComboColorNum = (t as OsuSlider).ComboColorNum;
                        go.GetComponent<OsuSlider>().number = (t as OsuSlider).number;
                    }
                    else if ((t.timestamp-GlobalValues.AR_in_ms > musicTime || t.timestamp + (t as OsuSlider).sum_time * (t as OsuSlider).count_of_slides < musicTime) && timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Remove(t.timestamp);
                    }
                }
                else if (t is Spinner)
                {
                    if (t.timestamp <= musicTime  && (t as Spinner).time_end >= musicTime && !timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Add(t.timestamp);
                        Vector2 ret = MathFuncs.OsuCoordsToUnity(new Vector2(256, 192));
                        OsuElement go = Instantiate(t, new Vector3(ret.x, ret.y, 0), Quaternion.identity);
                        go.transform.SetParent(parentCanvas.transform, false);
                        go.timestamp = t.timestamp;
                        go.GetComponent<Spinner>().time_end = (t as Spinner).time_end;
                    }
                    else if ((t.timestamp > musicTime || (t as Spinner).time_end < musicTime) && timesOnScreen.Contains(t.timestamp))
                    {
                        timesOnScreen.Remove(t.timestamp);
                    }
                }

            }
        }

        public static void DeleteTimestampFromScreen(int timestamp)
        {
            timesOnScreen.Remove(timestamp);
        }
    }
}
