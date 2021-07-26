using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class UIElseButton : MonoBehaviour
    {
        private bool isActive = false;

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    //Click();
                }
            }
        }

        void OnMouseDown()
        {
          //  Click();
        }
        private void Click()
        {
            if (isActive) { disActive(); }
            else { Active(); }
        }

        public void Active()
        {
            var k = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(k.r, k.g, k.b, (float)1);
            isActive = true;
        }
        public void disActive()
        {
            var kk = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(kk.r, kk.g, kk.b, (float)0.5);
            isActive = false;
        }
    }
}
