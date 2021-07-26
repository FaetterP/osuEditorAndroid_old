using Assets.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.EditorOSU
{
    class AddSliderPoint : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        void OnMouseDown()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 MousePos = Input.mousePosition;
                MousePos = cam.ScreenToWorldPoint(MousePos);
                MousePos = gameObject.transform.parent.worldToLocalMatrix.MultiplyPoint(MousePos);
                MousePos = MathFuncs.UnityCoordsToOsu(MousePos);

                Click(MousePos);
            }
        }


        private void Click(Vector2 coords)
        {
            if (GlobalValues.sliderStatus != "add") { return; }
            (GlobalValues.Selected_Element as OsuSlider).points.Add(new SliderPoint((int)coords.x, (int)coords.y));
         //  (GlobalValues.Selected_Element as OsuSlider).length = (decimal)MathFuncs.GetLengthOfSlider(GlobalValues.Selected_Element as OsuSlider);
            (GlobalValues.Selected_Element as OsuSlider).UpdateSumTime();

            foreach (GameObject t in GameObject.FindGameObjectsWithTag("Note"))
            {
                ControllerApproachingDetails.DeleteTimestampFromScreen(t.GetComponent<OsuElement>().timestamp);
                Destroy(t);
            }
            GameObject.Find("SliderBeatDivisor").GetComponent<TimelineStepSlider>().UpdateNoteMarks();
            foreach (var t in FindObjectsOfType<NoteTimeMark>())
            {
                ControllerApproachingTimeMarks.DeleteNoteTimestampFromScreen(t.timestamp);
                Destroy(t.gameObject);
            }
        }


    }
}
