using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Element;

namespace Assets.EditorOSU
{
    class DisablerCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas activated_canvas;
        [SerializeField] private List<Canvas> disabled_canvases;

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
            activated_canvas.gameObject.SetActive(true);
            foreach(var t in disabled_canvases)
            {
                t.gameObject.SetActive(false);
            }
            foreach(var t in GameObject.FindGameObjectsWithTag("TimeMark"))
            {
                ControllerApproachingTimeMarks.DeleteTimestampFromScreen(t.GetComponent<TimeMark>().GetTimestamp());
                Destroy(t.gameObject);
            }
        }
    }
}
