using Assets.EditorOSU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class PlacerNotes : MonoBehaviour
    {
        [SerializeField] private Note note;
        [SerializeField] private Spinner spinner;
        [SerializeField] private OsuSlider slider;
        [SerializeField] private AudioSource music;
        [SerializeField] private Camera cam;
         void OnMouseDown()
         {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 MousePos = Input.mousePosition;
                MousePos= cam.ScreenToWorldPoint(MousePos);
                MousePos = gameObject.transform.parent.worldToLocalMatrix.MultiplyPoint(MousePos);
                MousePos = MathFuncs.UnityCoordsToOsu(MousePos);

                Click(MousePos);
            }
         }


        private void Click(Vector2 coords)
        {
            if (GlobalValues.Status == "note")
            {
                Note added = (Note)note.Clone();
                added.timestamp = MathFuncs.GetNearestTimeMark((int)(music.time * 1000));
                added.x = (int)coords.x;
                added.y = (int)coords.y;
                added.sum_combo = 5;
                added.hitsound = new bool[3];

                GlobalValues.GlobalMap.AddElement(added);
                GlobalValues.GlobalMap.AllElements.Sort();
                GlobalValues.GlobalMap.UpdateComboColours();
                foreach (GameObject t in GameObject.FindGameObjectsWithTag("Note"))
                {
                    ControllerApproachingDetails.DeleteTimestampFromScreen(t.GetComponent<Note>().timestamp);
                    Destroy(t);
                }
                GameObject.Find("SliderBeatDivisor").GetComponent<TimelineStepSlider>().UpdateNoteMarks();
                foreach (var t in FindObjectsOfType<NoteTimeMark>())
                {
                    ControllerApproachingTimeMarks.DeleteNoteTimestampFromScreen(t.timestamp);
                    Destroy(t.gameObject);
                }
                
            }
            else if (GlobalValues.Status == "spinner")
            {
                Spinner added = (Spinner)spinner.Clone();
                added.timestamp = MathFuncs.GetNearestTimeMark((int)(music.time * 1000));
                added.time_end = (int)(added.timestamp + 4 * Math.Abs(MathFuncs.GetNearestTimingPoint(added.timestamp).beatLength));

                GlobalValues.GlobalMap.AddElement(added);
                GlobalValues.GlobalMap.AllElements.Sort();
                GlobalValues.GlobalMap.UpdateComboColours();
                foreach (GameObject t in GameObject.FindGameObjectsWithTag("Note"))
                {
                    ControllerApproachingDetails.DeleteTimestampFromScreen(t.GetComponent<Note>().timestamp);
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
}
