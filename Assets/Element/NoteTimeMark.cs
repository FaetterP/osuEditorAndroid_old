using Assets.EditorOSU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class NoteTimeMark : MonoBehaviour
    {
        [NonSerialized] public OsuElement thisNote;
        [SerializeField] private Text num_text;
        private bool isfirst=true;

        void Start()
        {
            if (thisNote is Note)
            {
                Color c = GlobalValues.GlobalMap.ComboColors[(thisNote as Note).ComboColorNum];
                GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
                num_text.text = (thisNote as Note).number.ToString();
            }
            if (thisNote is OsuSlider)
            {
                Color c = GlobalValues.GlobalMap.ComboColors[(thisNote as OsuSlider).ComboColorNum];
                GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
                num_text.text = (thisNote as OsuSlider).number.ToString();
            }
        }

        void OnEnable()
        {
            if (isfirst) { isfirst = false; return; }
            if (thisNote is Note)
            {
                Color c = GlobalValues.GlobalMap.ComboColors[(thisNote as Note).ComboColorNum];
                GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
                num_text.text = (thisNote as Note).number.ToString();
                Debug.Log((thisNote as Note).number.ToString());
            }
            if (thisNote is OsuSlider)
            {
                Color c = GlobalValues.GlobalMap.ComboColors[(thisNote as OsuSlider).ComboColorNum];
                GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
                num_text.text = (thisNote as OsuSlider).number.ToString();
            }
        }
        public object Clone()
        {
            return MemberwiseClone();
        }


         private void DestroyFromScreen()
        {
            ControllerApproachingTimeMarks.DeleteNoteTimestampFromScreen(timestamp);
        }

        private GameObject timeMarkLine;
        private AudioSource music;
        private int time_in_ms;
        [SerializeField] public int timestamp;
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
            transform.localPosition = new Vector3(ttt.Value, timeMarkLine.transform.localPosition.y, 0);

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    Click();
                }
            }



        }
        public int GetTimestamp()
        {
            return timestamp;
        }

        public void SetTimestamp(int time)
        {
            timestamp = time;
        }

        public int CompareTo(object o)
        {
            return timestamp.CompareTo((o as TimeMark).timestamp);
        }



        void OnMouseDown()
        {
            Click();
        }
        private void Click()
        {
            if (GlobalValues.Status == "select")
            {
                foreach (var t in GameObject.FindGameObjectsWithTag("SliderPoint")) { Destroy(t.gameObject); }

                GlobalValues.Selected_Element = MathFuncs.GetElFromTimestamp(thisNote.timestamp);
                if (GlobalValues.Selected_Element is Note)
                {
                    if ((GlobalValues.Selected_Element as Note).sum_combo == 1)
                    { GameObject.Find("NewCombo").GetComponent<UIElseButton>().disActive(); }
                    else { GameObject.Find("NewCombo").GetComponent<UIElseButton>().Active(); }

                    if ((GlobalValues.Selected_Element as Note).hitsound[0]) { GameObject.Find("Whistle").GetComponent<UIElseButton>().Active(); }
                    else { GameObject.Find("Whistle").GetComponent<UIElseButton>().disActive(); }
                    if ((GlobalValues.Selected_Element as Note).hitsound[1]) { GameObject.Find("Finish").GetComponent<UIElseButton>().Active(); }
                    else { GameObject.Find("Finish").GetComponent<UIElseButton>().disActive(); }
                    if ((GlobalValues.Selected_Element as Note).hitsound[2]) { GameObject.Find("Clap").GetComponent<UIElseButton>().Active(); }
                    else { GameObject.Find("Clap").GetComponent<UIElseButton>().disActive(); }

                   FindObjectOfType<CanvasHandler>().canvas.gameObject.SetActive(false);
                }

                if (GlobalValues.Selected_Element is OsuSlider)
                {
                    if ((GlobalValues.Selected_Element as OsuSlider).sum_combo == 1)
                    { GameObject.Find("NewCombo").GetComponent<UIElseButton>().disActive(); }
                    else { GameObject.Find("NewCombo").GetComponent<UIElseButton>().Active(); }
                    FindObjectOfType<CanvasHandler>().canvas.gameObject.SetActive(true);
                    (GlobalValues.Selected_Element as OsuSlider).PrintSliderPointsUI();
                }
            }
        }
    }
}
