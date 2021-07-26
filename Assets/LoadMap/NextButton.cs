using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Assets.Element;
using Assets.Mapinfo;

namespace Assets.LoadMap
{
    class NextButton : MonoBehaviour
    {
        [SerializeField] private Text textOnScreen;
        [SerializeField] private Text currentText;
        [SerializeField] private Note note;
        [SerializeField] private Spinner spinner;
        [SerializeField] private OsuSlider slider;
        private List<string> ans=new List<string>();
        private int numberOfBeatmap, LoadStatus = 0;

        void Start()
        {
            DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
            foreach (var t in di.GetDirectories())
            {
                // Debug.Log(t.FullName);
                if (new DirectoryInfo(t.FullName).GetFiles("*.osu").Any(x => x.Extension == ".osu"))
                {
                    ans.Add(t.FullName);
                    textOnScreen.text +=ans.Count+") "+ t.Name + "\n";
                }
            }
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
            
            if (LoadStatus == 0)
            {
                try { numberOfBeatmap = int.Parse(currentText.text); }
                catch { return; }
                textOnScreen.text = "";
                string path = ans[numberOfBeatmap - 1];
                GlobalValues.GlobalMap.path = path + "/";
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (var t in di.GetFiles())
                {
                    if (t.Name.EndsWith(".osu"))
                    {
                        textOnScreen.text += t.Name + "\n";
                    }
                }
                LoadStatus++;
                return;
            }

            if (LoadStatus == 1)
            {
                string fullname = "";
                bool once = true;
                List<string> list = new List<string>();
                string[] strarr = textOnScreen.text.Split('\n');
                foreach(string t in strarr)
                {
                    string[] arr = t.Split(' ');
                    string added = arr[arr.Length - 1];
                    if (once)
                    {
                        foreach (string tt in arr)
                        {
                            if (tt != added) { fullname += tt + " "; }
                        }once = false;
                    }
                    try{ added = added.Remove(added.Length - 4); }
                    catch { continue; }
                    list.Add(added);
                }
                if(list.Contains("[" + currentText.text + "]"))
                {
                   GlobalValues.currenfFilePath=GlobalValues.GlobalMap.path+fullname+"["+currentText.text+"].osu";
                }
                LoadDataFromFile();
                SceneManager.LoadScene(4);
                return;
            }
        }

        private void LoadDataFromFile()
        {
            double parent_bpm = -1;
            int status = 0, color_num=0;
            int number = 1;
            string[] lines = File.ReadAllLines(GlobalValues.currenfFilePath);

            foreach(string t in lines)
            {
                if (t == "[General]") { status = 1; continue; }
                if (t == "[Editor]") { status = 2; continue; }
                if (t == "[Metadata]") { status = 3; continue; }
                if (t == "[Difficulty]") { status = 4; continue; }
                if (t == "[Events]") { status = 5; continue; }
                if (t == "[TimingPoints]") { status = 6; continue; }
                if (t == "[Colours]") { status = 7; continue; }
                if (t == "[HitObjects]") { status = 8; continue; }

                if (status == 1)
                {
                    if (t.StartsWith("AudioFilename:"))
                    {
                        string[] tt = t.Split(':',' ');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.AudioName = tt[tt.Length-1];
                    }
                    if (t.StartsWith("AudioLeadIn:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.AudioLeadIn = int.Parse(tt[1]);
                    }
                    if (t.StartsWith("PreviewTime:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.PreviewTime = int.Parse(tt[1]);
                    }
                    if (t.StartsWith("Countdown:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.Countdown = int.Parse(tt[1]) == 1 ? true : false;
                    }
                    if (t.StartsWith("SampleSet:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.SampleSet = tt[tt.Length-1];
                    }
                    if (t.StartsWith("StackLeniency:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.StackLeniency = float.Parse(tt[1]);
                    }
                    if (t.StartsWith("Mode:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.Mode = int.Parse(tt[tt.Length-1]);
                    }
                    if (t.StartsWith("LetterboxInBreaks:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.LetterboxInBreaks = int.Parse(tt[1]) == 1 ? true : false;
                    }
                    if (t.StartsWith("WidescreenStoryboard:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.GeneralInfo.WidescreenStoryboard = int.Parse(tt[1]) == 1 ? true : false;
                    }
                }//+
                else if (status == 2)
                {
                    if (t.StartsWith("DistanceSpacing:"))
                    {
                        string[] tt = t.Split(':',' ');
                        GlobalValues.GlobalMap.Settings.EditorInfo.DistanceSpacing = (int)double.Parse(tt[tt.Length-1]);
                    }
                    if (t.StartsWith("BeatDivisor:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.EditorInfo.BeatDivisor = (int)double.Parse(tt[1]);
                    }
                    if (t.StartsWith("GridSize:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.EditorInfo.GridSize = (int)double.Parse(tt[1]);
                    }
                    if (t.StartsWith("TimelineZoom:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.EditorInfo.TimelineZoom = (int)double.Parse(tt[1]);
                    }
                }//+
                else if (status == 3)
                {
                    if (t.StartsWith("Title:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.Title = tt[1];
                    }
                    if (t.StartsWith("TitleUnicode:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.TileU = tt[1];
                    }
                    if (t.StartsWith("Artist:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.Artist = tt[1];
                    }
                    if (t.StartsWith("ArtistUnicode:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.ArtistU = tt[1];
                    }
                    if (t.StartsWith("Creator:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.Creator = tt[1];
                    }
                    if (t.StartsWith("Version:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.DiffName = tt[1];
                    }
                    if (t.StartsWith("Source:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.Source = tt[1];
                    }
                    if (t.StartsWith("Tags:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.Tags = tt[1];
                    }
                    if (t.StartsWith("BeatmapID:"))
                    {
                        string[] tt = t.Split(':', ' ');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.ID_of_map = int.Parse(tt[tt.Length - 1]);
                    }
                    if (t.StartsWith("BeatmapSetID:"))
                    {
                        string[] tt = t.Split(':', ' ');
                        GlobalValues.GlobalMap.Settings.MetadataInfo.ID_of_set = int.Parse(tt[tt.Length-1]);
                    }
                }//+
                else if (status == 4)
                {
                    if (t.StartsWith("HPDrainRate:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.HD = double.Parse(tt[1]);
                    }
                    if (t.StartsWith("CircleSize"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.CS = double.Parse(tt[1]);
                    }
                    if (t.StartsWith("OverallDifficulty:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.OD = double.Parse(tt[1]);
                    }
                    if (t.StartsWith("ApproachRate:"))
                    {
                        string[] tt = t.Split(':');
                        double ar = double.Parse(tt[1]);
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.AR = ar;

                        if (ar == 5) { GlobalValues.AR_in_ms = 1200; }
                        if (ar < 5) { GlobalValues.AR_in_ms = (int)(1200 + 600 * (5 - ar) / 5); }
                        if (ar > 5) { GlobalValues.AR_in_ms = (int)(1200 - 750 * (ar - 5) / 5); }
                    }
                    if (t.StartsWith("SliderMultiplier:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.SM = decimal.Parse(tt[1]);
                    }
                    if (t.StartsWith("SliderTickRate:"))
                    {
                        string[] tt = t.Split(':');
                        GlobalValues.GlobalMap.Settings.DifficultyInfo.ST = int.Parse(tt[1]);
                    }
                }//+
                else if (status == 5)
                {
                        string[] tt = t.Split(',');
                    if (tt.Length == 5)
                    {
                       GlobalValues.GlobalMap.Settings.EventsInfo.name_of_bg=tt[2].Remove(tt[2].Length - 1, 1).Remove(0, 1);
                    }

                }//background +
                else if (status == 6)
                {
                    
                    string[] tt = t.Split(',');
                    if (tt.Length == 8)
                    {
                        TimingPoints added = new TimingPoints();
                        added.time = int.Parse(tt[0]);
                        added.beatLength = decimal.Parse(tt[1]);
                        added.meter = int.Parse(tt[2]);
                        added.sampleSet = int.Parse(tt[3]);
                        added.sampleIndex = int.Parse(tt[4]);
                        added.volume = int.Parse(tt[5]);
                        added.uninherited = int.Parse(tt[6]) == 0 ? false : true;
                        added.kiai = int.Parse(tt[7]) == 0 ? false : true;

                        
                        if (added.beatLength < 0) { added.isParent = false; added.bpm = parent_bpm; added.UpdateValues(); }
                        else { added.isParent = true; added.UpdateValues(); parent_bpm = added.bpm;  }

                        GlobalValues.GlobalMap.Settings.TimePoints.Add(added);
                    }
                }//+
                else if (status == 7)
                {
                    if (t.StartsWith("Combo"))
                    {
                        string[] tt = t.Split(':', ' ', ',');
                        int b = int.Parse(tt[tt.Length - 1]);
                        int g = int.Parse(tt[tt.Length - 2]);
                        int r = int.Parse(tt[tt.Length - 3]);
                        GlobalValues.GlobalMap.ComboColors.Add(new Color(r, g, b));
                    }
                }//+
                else if (status == 8)
                {
                    if (GlobalValues.GlobalMap.ComboColors.Count == 0) { GlobalValues.GlobalMap.ComboColors.Add(new Color(186, 239, 255)); GlobalValues.GlobalMap.ComboColors.Add(new Color(241, 186, 255)); }
                    else if (GlobalValues.GlobalMap.ComboColors.Count == 1) { GlobalValues.GlobalMap.ComboColors.Add(new Color(186, 239, 255)); }
                    if (t.Split(',').Length == 6)//note
                    {
                        string[] tt = t.Split(':', ' ', ',');
                        Note addedNote = (Note)note.Clone();
                        addedNote.timestamp = int.Parse(tt[2]);
                        (addedNote as Note).x = int.Parse(tt[0]);
                        (addedNote as Note).y = int.Parse(tt[1]);

                        int sum_color = int.Parse(tt[3]);
                        (addedNote as Note).sum_combo = sum_color;
                        if (sum_color == 1) { addedNote.ComboColorNum = color_num; number++; addedNote.number = number; }
                        else if (sum_color == 5) { color_num++; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; addedNote.ComboColorNum = color_num; number = 1; addedNote.number = number; }
                        else { color_num += (sum_color / 16)+1; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; addedNote.ComboColorNum = color_num; number = 1; addedNote.number = number; }

                        addedNote.hitsound = new bool[3];

                        GlobalValues.GlobalMap.AddElement(addedNote);
                    }

                    else if (t.Split(',').Length == 7)//spinner
                    {
                        string[] tt = t.Split(':', ' ', ',');
                        Spinner addedNote = (Spinner)spinner.Clone();
                        addedNote.timestamp = int.Parse(tt[2]);
                        addedNote.time_end = int.Parse(tt[5]);
                        GlobalValues.GlobalMap.AddElement(addedNote);
                    }
                    else if(t.Contains('|'))//slider
                    {
                        string[] tt = t.Split(',');
                        OsuSlider addedSlider = (OsuSlider)slider.Clone();
                        addedSlider.points=new List<SliderPoint>();
                        addedSlider.x_start = int.Parse(tt[0]);
                        addedSlider.y_start = int.Parse(tt[1]);
                        addedSlider.timestamp = int.Parse(tt[2]);

                        string[] coords = tt[5].Split('|');
                        string[] newcoords=new string[coords.Length-1];
                        Array.Copy(coords, 1, newcoords, 0, coords.Length - 1);
                        int x, y,x0=int.MaxValue,y0=int.MaxValue;
                        foreach(var c in newcoords)
                        {
                            string[] xy = c.Split(':');
                            x = int.Parse(xy[0]);
                            y = int.Parse(xy[1]);
                            if (x != x0 || y != y0)
                            {
                                addedSlider.AddPoint(x, y);
                                x0 = x;y0 = y;
                            }
                            else
                            {
                                addedSlider.points[addedSlider.points.Count - 1].SwitchLocked();
                            }
                            
                        }
                        addedSlider.count_of_slides = int.Parse(tt[6]);
                        addedSlider.length = decimal.Parse(tt[7]);
                        addedSlider.UpdateSumTime();

                        int sum_color = int.Parse(tt[3]);
                        addedSlider.sum_combo = sum_color;
                        if (sum_color == 2) { addedSlider.ComboColorNum = color_num; number++; addedSlider.number = number; }
                        else if (sum_color == 6) { color_num++; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; addedSlider.ComboColorNum = color_num; number=1; addedSlider.number = number; }
                        else { color_num += (sum_color / 16) +1; color_num = color_num % GlobalValues.GlobalMap.ComboColors.Count; addedSlider.ComboColorNum = color_num; number = 1; addedSlider.number = number; }

                        GlobalValues.GlobalMap.AddElement(addedSlider);
                    }
                }//+
            }
        }
    }
}
