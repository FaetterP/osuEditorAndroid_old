using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Element;
using Assets.Mapinfo;
using UnityEngine;

namespace Assets //   <-
{
    class Map
    {
        public List<OsuElement> AllElements= new List<OsuElement>();
        public Info Settings = new Info();
        public string path="";
        public List<Color> ComboColors=new List<Color>();
        public int offset=0, bpm=100;
        public List<TimeMark> TimeMarks = new List<TimeMark>();
        public List<NoteTimeMark> NoteTimeMarks = new List<NoteTimeMark>();

        public Map(string path)
        {
            this.path = path;
        }

        public void AddElement(OsuElement addedEl)
        {
            bool needRem = false;
            foreach(OsuElement t in AllElements)
            {
                if (t.timestamp == addedEl.timestamp) { needRem = true; break; }
            }
            if (needRem) { RemElement(addedEl.timestamp); }
            AllElements.Add(addedEl);
        }

        public void RemElement(int removedTimestamp)
        {
            int i = 0;
            foreach(OsuElement t in AllElements)
            {
                if (t.timestamp == removedTimestamp) { break; }
                i++;
            }
            AllElements.RemoveAt(i);
        }

        public List<OsuElement> GetAllElements()
        {
            return AllElements;
        }

        
        public void UpdateComboColours()
        {
            int color_num=0, number=1;
            foreach(OsuElement t in AllElements)
            {
                if (t is Note)
                {
                    int sum_color = (t as Note).sum_combo;
                    (t as Note).sum_combo = sum_color;
                    if (sum_color == 1) { (t as Note).ComboColorNum = color_num; number++; (t as Note).number = number; }
                    else if (sum_color == 5) { color_num++; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; (t as Note).ComboColorNum = color_num; number = 1; (t as Note).number = number; }
                    else { color_num += (sum_color / 16) + 1; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; (t as Note).ComboColorNum = color_num; number = 1; (t as Note).number = number; }
                }
                else if (t is OsuSlider)
                {
                    int sum_color = (t as OsuSlider).sum_combo;
                    (t as OsuSlider).sum_combo = sum_color;
                    if (sum_color == 2) { (t as OsuSlider).ComboColorNum = color_num; number++; (t as OsuSlider).number = number; }
                    else if (sum_color == 6) { color_num++; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; (t as OsuSlider).ComboColorNum = color_num; number = 1; (t as OsuSlider).number = number; }
                    else { color_num += (sum_color / 16) + 1; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; (t as OsuSlider).ComboColorNum = color_num; number = 1; (t as OsuSlider).number = number; }
                }
            }

        }


    }
}
