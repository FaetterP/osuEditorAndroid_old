using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Diagnostics;
using Assets.Mapinfo;

namespace Assets.EditorOSU
{
    class ClickNewBPM : MonoBehaviour
    {
        [SerializeField] AudioSource music;
        [SerializeField] Canvas disabled_canvas, enabled_canvas;
        private int count = 10;
        private TimingPoints added=new TimingPoints();

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
        Stopwatch stopWatch=new Stopwatch();
        List<long> lengths = new List<long>();
        private void Click()
        {
            if (count == 10) { stopWatch.Start(); added.time = (int)(music.time * 1000); music.Play(); }
            else
            {
                stopWatch.Stop();
                lengths.Add(stopWatch.ElapsedMilliseconds);
                UnityEngine.Debug.Log(stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
                stopWatch.Start();
            }
            if (count == 0)
            {
                stopWatch.Stop();
                long sr_len = 0;
                foreach( var t in lengths)
                {
                    sr_len += t;
                }
                double sr_bpm_len= 1.0 * sr_len / lengths.Count;
                added.bpm = 60000.0 / sr_bpm_len;
                added.beatLength = (decimal)(60000.0 / added.bpm);
                added.isParent = true;
                added.mult = 1;
                added.kiai = false;
                added.volume = 100;
                GlobalValues.GlobalMap.Settings.TimePoints.Add(added);
                GlobalValues.GlobalMap.Settings.TimePoints.Sort();
                enabled_canvas.gameObject.SetActive(true);
                disabled_canvas.gameObject.SetActive(false);
            }
            count--;
        }
    }
}
