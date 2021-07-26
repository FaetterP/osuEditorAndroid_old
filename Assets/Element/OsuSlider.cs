using Assets.EditorOSU;
using Assets.Mapinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class OsuSlider : OsuElement, ICloneable
    {
        private AudioSource music;
        public int x_start, y_start, ComboColorNum, sum_combo, number;
        public List<SliderPoint> points;
        public decimal sum_time,length;
        [SerializeField] private GameObject sliderBall;
        private bool onScreen = false;
        private GameObject ball;
        public int count_of_slides;
        public List<Vector2> sliders_points = new List<Vector2>();
        [SerializeField] private SliderPointUI pointui;

        private bool isFirst = true;

        public void AddPoint(int x, int y)
        {
            points.Add(new SliderPoint(x,y));
        }
        public List<SliderPoint> GetPoints()
        {
            return points;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }


        private int curr_slide=0;
        void Update()
        { 
            if (music.time * 1000 < timestamp- GlobalValues.AR_in_ms || music.time * 1000 > (int)(timestamp + sum_time*count_of_slides)) { Destroy(gameObject); }
            if (timestamp <= (int)(music.time * 1000) && timestamp + sum_time*count_of_slides >= (int)(music.time * 1000))
            {
                if (onScreen == false) { ball = Instantiate(sliderBall, new Vector2(10000, 0), Quaternion.identity); ball.transform.SetParent(transform, false); onScreen = true; }
                int razn = (int)(((music.time * 1000) - timestamp)%(float)sum_time);
                double h = (double)sum_time / sliders_points.Count;
                if (curr_slide != (int)((music.time * 1000 - timestamp) / (double)sum_time)) { curr_slide = (int)((music.time * 1000 - timestamp) / (double)sum_time); }
                int index;
                if (curr_slide % 2 == 0)
                {
                    index = (int)((double)sum_time / h) - (int)(razn / h) - 1;
                }
                else
                {
                    index = (int)(razn / h) - 1;
                }
                //Debug.Log(sliders_points.Count);
                if (index < 0) { index = 0; }
                Vector2 v = sliders_points[index];
                Vector2 nv = MathFuncs.OsuCoordsToUnity(v);
                ball.transform.localPosition = new Vector2(nv.x - transform.localPosition.x, nv.y - transform.localPosition.y);

            }
            else if (ball != null)
            {
                onScreen = false;
                Destroy(ball.gameObject);
            }
        }

        void Awake()
        {
            music = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        }

        void Start()
        {
            if (timestamp == GlobalValues.Selected_Element.timestamp)
            {
                PrintSliderPointsUI();
            }
       //     length = (decimal)MathFuncs.GetLengthOfSlider(this);
            UpdateSumTime();
        }


        public void UpdateSumTime()
        {
            TimingPoints near = MathFuncs.GetNearestTimingPoint(timestamp);
            sum_time = MathFuncs.OsuPixelsToTime((double)length, near.mult,(decimal)(60000.0/near.bpm));
        }

        void OnEnable()
        {
            if (isFirst) { isFirst = false; return; }
            ControllerApproachingDetails.DeleteTimestampFromScreen(timestamp);
            Destroy(gameObject);
        }

        public void PrintSliderPointsUI()
        {
            foreach(var t in points)
            {
                Vector2 coords = new Vector2(t.x, t.y);

                SliderPointUI p=Instantiate(pointui);
                p.point = t;
                p.transform.SetParent(GameObject.Find("MainCanvas").transform);
                p.transform.localPosition = MathFuncs.OsuCoordsToUnity(coords);
                p.transform.localScale = new Vector3(1, 1, 1);
            }

        }
    }
}
