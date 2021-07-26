using Assets.Mapinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.EditorOSU
{
    class AddNewNotParentTimingPoint : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private CreatorPrinterTimingPoints creator;
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
            TimingPoints added = new TimingPoints(), ret = MathFuncs.GetNearestParentTimingPoint_Left((int)(music.time*1000));
            added.time = ret.time+1;
            added.isParent = false;
            added.beatLength = -100;
            added.bpm = ret.bpm;
            added.kiai = false;
            added.mult = 1;
            GlobalValues.GlobalMap.Settings.TimePoints.Add(added);
            GlobalValues.GlobalMap.Settings.UpdateTimingPoints();
            creator.UpdatePrnters();
        }
    }
}
