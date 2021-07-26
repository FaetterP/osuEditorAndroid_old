using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.ProgrammSettings
{
    class ShowTip : MonoBehaviour
    {
        [SerializeField]private GameObject go;
        private bool isOpen = false;

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
            if (isOpen)
            {
                go.transform.position = new Vector3(0, 0, 0);
                isOpen = false;
            }
            else
            {
                go.transform.position = new Vector3(1000, 1000, 1000);
                isOpen = true;
            }

        }
    }
}
