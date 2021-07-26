using Assets.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.EditorOSU
{
    class ChangeHitSound : MonoBehaviour
    {
        [SerializeField] private int hit_num;
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
                if ((GlobalValues.Selected_Element as Note).hitsound[hit_num]) { (GlobalValues.Selected_Element as Note).hitsound[hit_num] = false; thisButton.disActive(); }
                else { (GlobalValues.Selected_Element as Note).hitsound[hit_num] = true;  thisButton.Active(); }
            }
            else if (GlobalValues.Selected_Element is OsuSlider)
            {

            }
        }
    }
}
