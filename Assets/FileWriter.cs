using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets
{
    class FileWriter : MonoBehaviour
    {
        [SerializeField]private Text Title, TitleU, Artist, ArtistU, Creator, Source, DifficultyName, Tags, HD, OD, CS, AR;


        void OnMouseDown()
        {
            WriteSettingToMap();
            WriteToFile(GlobalValues.GlobalMap);
            SceneManager.LoadScene(0);
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    WriteSettingToMap();
                    WriteToFile(GlobalValues.GlobalMap);
                    SceneManager.LoadScene(0);
                }
            }
        }

        public void WriteToFile(Map map)
        {
            string fileName = map.Settings.MetadataInfo.ArtistU + " - " + map.Settings.MetadataInfo.TileU + " (" + map.Settings.MetadataInfo.Creator + ") [" + map.Settings.MetadataInfo.DiffName + "].osu";
            string path = GlobalValues.GlobalMap.path + "/"+fileName;
            Debug.Log(GlobalValues.GlobalMap.path + "/" + fileName);
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(GetNewFile(map));
            sw.Close();
        }

        private string GetNewFile(Map map)
        {

            string ret= "osu file format v14\n\n[General]\nAudioFilename: audio.mp3\nAudioLeadIn: ";
            ret += map.Settings.GeneralInfo.AudioLeadIn + "\nPreviewTime:";
            ret += map.Settings.GeneralInfo.PreviewTime + "\nCountdown: ";
            ret += map.Settings.GeneralInfo.Countdown ? "1" : "0";
            ret += "\nSampleSet: ";
            ret += map.Settings.GeneralInfo.SampleSet + "\nStackLeniency: ";
            ret += map.Settings.GeneralInfo.StackLeniency + "\nMode: ";
            ret += map.Settings.GeneralInfo.Mode + "\nLetterboxInBreaks: ";
            ret += map.Settings.GeneralInfo.LetterboxInBreaks ? "1" : "0";
            ret += "\nWidescreenStoryboard: ";
            ret += map.Settings.GeneralInfo.WidescreenStoryboard ? "1" : "0";

            ret += "\n\n[Editor]\nDistanceSpacing: ";
            ret += Math.Round(map.Settings.EditorInfo.DistanceSpacing, 1)+ "\nBeatDivisor: ";
            ret += map.Settings.EditorInfo.BeatDivisor + "\nGridSize: 4\nTimelineZoom: 1";

            ret += "\n\n[Metadata]\nTitle:";
            ret += map.Settings.MetadataInfo.Title + "\nTitleUnicode:";
            ret += map.Settings.MetadataInfo.TileU + "\nArtist:";
            ret += map.Settings.MetadataInfo.Artist + "\nArtistUnicode:";
            ret += map.Settings.MetadataInfo.ArtistU + "\nCreator:";
            ret += map.Settings.MetadataInfo.Creator + "\nVersion:";
            ret += map.Settings.MetadataInfo.DiffName + "\nSource:";
            ret += map.Settings.MetadataInfo.Source + "\nTags:";
            ret += map.Settings.MetadataInfo.Tags + "\nBeatmapID:-1\nBeatmapSetID:-1";

            ret += "\n\n[Difficulty]\nHPDrainRate:";
            ret += map.Settings.DifficultyInfo.HD + "\nCircleSize:";
            ret += map.Settings.DifficultyInfo.CS + "\nOverallDifficulty:";
            ret += map.Settings.DifficultyInfo.OD + "\nApproachRate:";
            ret += map.Settings.DifficultyInfo.AR + "\nSliderMultiplier:";
            ret += map.Settings.DifficultyInfo.SM + "\nSliderTickRate:";
            ret += map.Settings.DifficultyInfo.ST;

            ret += "\n\n[Events]\n//Background and Video events\n0,0,\"background.jpg\",0,0";

            return ret;
        }
        private void WriteSettingToMap()
        {
            GlobalValues.GlobalMap.Settings.MetadataInfo.Title = Title.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.TileU = TitleU.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.Artist = Artist.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.ArtistU = ArtistU.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.Creator = Creator.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.Source = Source.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.Tags = Tags.text;
            GlobalValues.GlobalMap.Settings.MetadataInfo.DiffName = DifficultyName.text;

            GlobalValues.GlobalMap.Settings.DifficultyInfo.HD = float.Parse(HD.text);
            GlobalValues.GlobalMap.Settings.DifficultyInfo.OD = float.Parse(OD.text);
            GlobalValues.GlobalMap.Settings.DifficultyInfo.CS = float.Parse(CS.text);
            GlobalValues.GlobalMap.Settings.DifficultyInfo.AR = float.Parse(AR.text);
        }

    }
}
