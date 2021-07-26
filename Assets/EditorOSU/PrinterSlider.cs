using Assets.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class PrinterSlider : MonoBehaviour
    {
        public List<Vector2> res;
        private OsuSlider slider;
        [SerializeField] private GameObject main_point,rev;
        [SerializeField] private SliderPart slider_point;
        [SerializeField] private Text text;


        void Start()
        {
            slider = GetComponent<OsuSlider>();
            List<SliderPoint> for_inter = new List<SliderPoint>();
            for_inter.Add(new SliderPoint(slider.x_start,slider.y_start));
            for_inter.AddRange(slider.GetPoints());
            // slider.UpdateSumTime();
            //res = MathFuncs.GetSlidersPoints(for_inter, (int)slider.sum_time / 2);
            res = MathFuncs.GetSlidersPoints(for_inter, 1000);
            Debug.Log(slider.length);
            slider.length = (decimal)MathFuncs.GetLengthOfSlider(slider);
         //   slider.UpdateSumTime();
            Debug.Log(slider.length);

            GetComponent<OsuSlider>().sliders_points = res;
            for (int i=0; i < res.Count; i++)
            {
                Vector2 coords = res[i];
                Vector2 newCoords = MathFuncs.OsuCoordsToUnity(coords);
                SliderPart go = Instantiate(slider_point, new Vector3(newCoords.x - slider.transform.localPosition.x, newCoords.y - slider.transform.localPosition.y, slider.transform.localPosition.z - 1), Quaternion.identity);


                go.slider = slider;
                go.transform.SetParent(slider.transform, false);
            }

            PrintAdditionPoint(main_point, slider.transform.localPosition,0);
            PrintAdditionPoint(main_point, MathFuncs.OsuCoordsToUnity(res[0]),0);
            if (slider.count_of_slides > 1) 
            {
                Vector2 vec = new Vector2(res[0].x - res[1].x, res[0].y - res[1].y);
                int minus = vec.y < 0 ? -1 : 1;
                float angle = 180- minus * Vector2.Angle(vec, new Vector2(1,0));
                PrintAdditionPoint(rev, MathFuncs.OsuCoordsToUnity(res[0]),angle); 
            }
            if (slider.count_of_slides > 2) 
            {
                Vector2 vec = new Vector2(slider.x_start - res[res.Count-2].x, slider.y_start - res[res.Count-2].y);
                int minus = vec.y < 0 ? -1 : 1;
                float angle = 180 - minus * Vector2.Angle(vec, new Vector2(1, 0));
                PrintAdditionPoint(rev, slider.transform.localPosition,angle); 
            }

            Text t = Instantiate(text, slider.gameObject.transform.position, Quaternion.identity, transform);
            t.color = Color.black;
            t.text = slider.number.ToString();
        }

        private void PrintAdditionPoint(GameObject point, Vector2 coords, float angle)
        {
            GameObject go = Instantiate(point, new Vector3(coords.x - slider.transform.localPosition.x, coords.y - slider.transform.localPosition.y, slider.transform.localPosition.z - 1), Quaternion.Euler(0,0,angle));
            go.transform.SetParent(slider.transform, false);
        }


    }
}
