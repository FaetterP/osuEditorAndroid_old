using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Element;

namespace Assets.EditorOSU
{
    class ChangeNoteColour : MonoBehaviour
    {
        private UIElseButton thisButton;

        void Awake()
        {
            thisButton = GetComponent<UIElseButton>();
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
        }

        void OnMouseDown()
        {
            Click();
        }

        private void Click()
        {
            if (GlobalValues.Status != "select") { Debug.Log("not select"); return; }
            if (GlobalValues.Selected_Element is Note)
            {
                int sum_color = (GlobalValues.Selected_Element as Note).sum_combo;
                if (sum_color == 1) { sum_color = 5; }
                else { sum_color += 16; }
                if (sum_color > 5 + 16 * (GlobalValues.GlobalMap.ComboColors.Count - 2)) { sum_color = 1; }
                (GlobalValues.Selected_Element as Note).sum_combo= sum_color;
                GlobalValues.GlobalMap.UpdateComboColours();
                if (sum_color == 1) { thisButton.disActive(); }
                else { thisButton.Active(); }               
            }
            else if (GlobalValues.Selected_Element is OsuSlider)
            {
                int sum_color = (GlobalValues.Selected_Element as OsuSlider).sum_combo;
                if (sum_color == 2) { sum_color = 6; }
                else { sum_color += 16; }
                if (sum_color > 6 + 16 * (GlobalValues.GlobalMap.ComboColors.Count - 2)) { sum_color = 2; }
                (GlobalValues.Selected_Element as OsuSlider).sum_combo = sum_color;
                GlobalValues.GlobalMap.UpdateComboColours();
                if (sum_color == 2) { thisButton.disActive(); }
                else { thisButton.Active(); }
            }

            foreach(var t in FindObjectsOfType<OsuElement>())
            {
                ControllerApproachingDetails.DeleteTimestampFromScreen(t.timestamp);
                Destroy(t.gameObject);
            }

            GameObject.Find("SliderBeatDivisor").GetComponent<TimelineStepSlider>().UpdateNoteMarks();
            foreach(var t in FindObjectsOfType<NoteTimeMark>())
            {
                ControllerApproachingTimeMarks.DeleteNoteTimestampFromScreen(t.timestamp);
                Destroy(t.gameObject);
            }
        }
    }
}
