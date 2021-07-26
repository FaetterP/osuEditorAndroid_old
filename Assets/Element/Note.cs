using Assets.EditorOSU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class Note : OsuElement, ICloneable
    {
        public int x, y, ComboColorNum, sum_combo, number;
        public bool[] hitsound= new bool[3];
        public Sampleset sampleset, additions;

        private bool isFirst = true;

        private Image thisImage;
        [SerializeField] private Text num_text;

        private Color mouseOverColor = Color.blue;
        private Color originalColor = new Color();
        private bool dragging = false;
        private float distance;


        void Awake()
        {
            thisImage = GetComponent<Image>();
        }
        void Start()
        {
            UpdateColor();
        }

        void OnEnable()
        {
            if (isFirst) { isFirst = false; return; }
            ControllerApproachingDetails.DeleteTimestampFromScreen(timestamp);
            Destroy(gameObject);
        }

        public void UpdateColor()
        {
            Color c = GlobalValues.GlobalMap.ComboColors[ComboColorNum];
            thisImage.color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
            num_text.text = number.ToString();
        }

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
            if (dragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 rayPoint = ray.GetPoint(distance);
                transform.position = rayPoint;
            }
        }

        void OnMouseDown()
        {
            originalColor = GetComponent<Image>().color;
            GetComponent<Image>().color = mouseOverColor;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
            Click();
        }

        void OnMouseUp()
        {
            dragging = false;
            GetComponent<Image>().color = originalColor;
            Vector2 MousePos = Input.mousePosition;
            MousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(MousePos);
            MousePos = gameObject.transform.parent.worldToLocalMatrix.MultiplyPoint(MousePos);
            MousePos = MathFuncs.UnityCoordsToOsu(MousePos);
            (MathFuncs.GetElFromTimestamp(timestamp) as Note).x = (int)MousePos.x;
            (MathFuncs.GetElFromTimestamp(timestamp) as Note).y = (int)MousePos.y;
        }

        private void Click()
        {
            if (GlobalValues.Status == "select") 
            { 
                GlobalValues.Selected_Element = MathFuncs.GetElFromTimestamp(timestamp);

                if ((GlobalValues.Selected_Element as Note).sum_combo == 1) { GameObject.Find("NewCombo").GetComponent<UIElseButton>().disActive(); }
                else { GameObject.Find("NewCombo").GetComponent<UIElseButton>().Active(); }

                if ((GlobalValues.Selected_Element as Note).hitsound[0]) { GameObject.Find("Whistle").GetComponent<UIElseButton>().Active(); }
                else { GameObject.Find("Whistle").GetComponent<UIElseButton>().disActive(); }
                if ((GlobalValues.Selected_Element as Note).hitsound[1]) { GameObject.Find("Finish").GetComponent<UIElseButton>().Active(); }
                else { GameObject.Find("Finish").GetComponent<UIElseButton>().disActive(); }
                if ((GlobalValues.Selected_Element as Note).hitsound[2]) { GameObject.Find("Clap").GetComponent<UIElseButton>().Active(); }
                else { GameObject.Find("Clap").GetComponent<UIElseButton>().disActive(); }
            }
        }


        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
