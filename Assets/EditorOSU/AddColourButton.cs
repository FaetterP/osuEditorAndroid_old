using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EditorOSU
{
    class AddColourButton : MonoBehaviour
    {
        [SerializeField] private ComboColourSelector selector;
        [SerializeField] private RemoveColourButton rem;
        private Image thisImage;
        private bool IsActive=true;
        void Awake()
        {
            thisImage = GetComponent<Image>();
        }
        void Start()
        {
            UpdateIsActive();
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
            if (IsActive)
            {
                GlobalValues.GlobalMap.ComboColors.Add(Color.white);
                selector.number_of_selected_color=GlobalValues.GlobalMap.ComboColors.Count - 1;

                UpdateIsActive();
                rem.UpdateIsActive();

                Color c = GlobalValues.GlobalMap.ComboColors[selector.number_of_selected_color];
                selector.SetSliders(c.r, c.g, c.b);
                selector.UpdateColor();
                selector.UpdateTextNum();
                GlobalValues.GlobalMap.UpdateComboColours();
            }
        }

        public void UpdateIsActive()
        {
            if (GlobalValues.GlobalMap.ComboColors.Count >= 8) { thisImage.color = Color.gray; IsActive = false; }
            else { thisImage.color = Color.white; IsActive = true; }
        }
    }
}
