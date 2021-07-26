using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class NextColourButton : MonoBehaviour
    {
        [SerializeField] private ComboColourSelector selector;

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

        private int i = 0;
        private void Click()
        {
            i++;
            if (i == GlobalValues.GlobalMap.ComboColors.Count) { i = 0; }
            selector.number_of_selected_color = i % GlobalValues.GlobalMap.ComboColors.Count;
            Color c = GlobalValues.GlobalMap.ComboColors[selector.number_of_selected_color];
            selector.SetSliders(c.r, c.g, c.b);
            selector.UpdateColor();
            selector.UpdateTextNum();
        }

    }
}
