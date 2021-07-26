using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class SliderPart : MonoBehaviour
    {
        public OsuSlider slider;

        void Start()
        {
            Color c = GlobalValues.GlobalMap.ComboColors[slider.ComboColorNum];
            GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
        }

        void OnEnable()
        {
            Color c = new Color();
            try
            {
                c = GlobalValues.GlobalMap.ComboColors[slider.ComboColorNum];
            }
            catch { return; }
            GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
            
        }

        private Color mouseOverColor = Color.blue;
        private Color originalColor = Color.yellow;
        private bool dragging = false;
        private float distance;


        void OnMouseDown()
        {
            originalColor = GetComponent<Image>().color;
            GetComponent<Image>().color = mouseOverColor;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }

        void OnMouseUp()
        {
            dragging = false;
            GetComponent<Image>().color = originalColor;

            Vector2 MousePos = Input.mousePosition, from=new Vector2((MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).x_start, (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).y_start);
            MousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(MousePos);
            MousePos = gameObject.transform.parent.transform.parent.worldToLocalMatrix.MultiplyPoint(MousePos);
            MousePos = MathFuncs.UnityCoordsToOsu(MousePos);
            (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).x_start = (int)MousePos.x;
            (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).y_start = (int)MousePos.y;
            for(int i=0;i< (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).points.Count; i++)
            {
                (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).points[i].x += (int)(-from.x + MousePos.x);
                (MathFuncs.GetElFromTimestamp(slider.timestamp) as OsuSlider).points[i].y += (int)(-from.y + MousePos.y);
            }
        }

        void Update()
        {
            if (dragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 rayPoint = ray.GetPoint(distance);
                transform.parent.transform.position = rayPoint;
            }
        }
    }
}
