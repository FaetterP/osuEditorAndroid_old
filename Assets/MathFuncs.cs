using Assets.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Mapinfo;

namespace Assets
{
    class MathFuncs
    {
        private static int comb(int n, int k)
        {
            if (k == 0) return 1;
            if (n == 0) return 0;
            return comb(n - 1, k - 1) + comb(n - 1, k);
        }

        private static int[] GetTrPascal(int count)
        {
            int[] ret = new int[count];

            for (int i = 0; i < count; i++)
            {
                ret[i] = comb(count - 1, i);
            }
            
            return ret;
        }

        public static double[][] GetInterPointsBeze(int[][] points, int n)
        {
            double[][] ret = new double[2][];
            ret[0] = new double[n]; ret[1] = new double[n];
            int pascalsTrLength = points[0].Length;
            int[] TrPascal = GetTrPascal(pascalsTrLength);
            double h = (double)1 / n;

            for (int i = 0; i < n; i++)
            {
                double t = (i + 1) * h;

                for (int j = 0; j < pascalsTrLength; j++)
                {

                    ret[0][i] += Math.Pow(1 - t, j) * Math.Pow(t, pascalsTrLength - j - 1) * points[0][j] * TrPascal[j];
                    ret[1][i] += Math.Pow(1 - t, j) * Math.Pow(t, pascalsTrLength - j - 1) * points[1][j] * TrPascal[j];
                }
            }
            return ret;
        }

        public static Vector2 OsuCoordsToUnity(Vector2 osuCoords)
        {
            Vector2 ret = new Vector2();
            ret.x = (osuCoords.x * 3 / 2) - 384;
            ret.y = (osuCoords.y * 3 / -2) + 288;
            return ret;
        }
        public static Vector2 UnityCoordsToOsu(Vector2 unityCoords)
        {
            Vector2 ret = new Vector2();
            ret.x = (unityCoords.x + 384) * 2 / 3;
            ret.y = (unityCoords.y - 288) * 2 / -3;
            return ret;
        }

        public static int? GetMarkX(int timestamp, int XLeft, int XRigth, int timeLeft, int timeRight)
        {
            if (timestamp <= timeLeft || timestamp >= timeRight) { return null; }
            double ret = XLeft + (XRigth - XLeft+100) * ((double)(timestamp - timeLeft) / (timeRight - timeLeft));
            return (int)ret;
        }

        public static int GetNearestTimeMark(int timestamp)
        {
            int razn=int.MaxValue;
            TimeMark ret=null;
            foreach (TimeMark t in GlobalValues.GlobalMap.TimeMarks)
            {
                int razn2 = Math.Abs(t.GetTimestamp() - timestamp);
                if (razn > razn2) { razn = razn2; ret = t; }
                else { break; }
            }
            return ret.GetTimestamp();
        }

        public static string ConvertTimestampToSring(int timestamp)
        {
            string ret = "";
            ret += timestamp / 60000 + ":";
            ret += (timestamp % 60000) / 1000 + ":";
            if (((timestamp % 60000) % 1000).ToString().Length == 1) { ret += "00" + (timestamp % 60000) % 1000; }
            else if (((timestamp % 60000) % 1000).ToString().Length == 2) { ret += "0" + (timestamp % 60000) % 1000; }
            else { ret += (timestamp % 60000) % 1000; }
            return ret;
        }

        public static decimal OsuPixelsToTime(double length, double multiplier, decimal beat_length)
        {
            double ret;
          //  ret = (length * (double)beat_length) / (300.0 * multiplier);
            ret = (length* (double)beat_length) / (multiplier*100.0*(double)GlobalValues.GlobalMap.Settings.DifficultyInfo.SM);
            return (decimal)ret;
        }
        public static decimal TimeToOsuPixels(double time, double multiplier)//not use
        {
            decimal ret=0;
           // ret = (decimal)(time * (3.0 / 5) * multiplier);
            return ret;
        }
        public static double GetLengthOfSlider(OsuSlider slider)
        {
            //Debug.Log(slider.);
            double ret = 0;
            for(int i = 0; i < slider.sliders_points.Count-1; i++)
            {
                Vector2 vec1 = new Vector2(slider.sliders_points[i].x, slider.sliders_points[i].y);
                vec1 = UnityCoordsToOsu(vec1);
                Vector2 vec2 = new Vector2(slider.sliders_points[i+1].x, slider.sliders_points[i+1].y);
                vec2 = UnityCoordsToOsu(vec2);
                ret += Math.Sqrt(Math.Pow(vec1.x-vec2.x, 2) + Math.Pow(vec1.y-vec2.y, 2));
            }
            
            return ret;
        }

        public static List<Vector2> GetSlidersPoints(List<SliderPoint> list, int count)
        {
            double h = 1.0 * count / list.Count;
            List<int> counts_foreach = new List<int>();
            List<Vector2> ret = new List<Vector2>();
            List<List<SliderPoint>> prom = new List<List<SliderPoint>>();
            prom.Add(new List<SliderPoint>());
            int AAAA = 0;
            foreach (var t in list)
            {
                AAAA++;
                if (t.GetIsLocked())
                {
                    prom[prom.Count - 1].Add(t);
                    prom.Add(new List<SliderPoint>());
                    prom[prom.Count - 1].Add(t);
                }
                else
                {
                    prom[prom.Count - 1].Add(t);
                }
            }

            foreach(var t in prom)
            {
                counts_foreach.Add((int)(t.Count * h));
            }

            for (int i = prom.Count-1; i >=0; i--)
            {
                List<Vector2> added = new List<Vector2>();
                int[][] for_inter = new int[2][];
                for_inter[0] = new int[prom[i].Count];
                for_inter[1] = new int[prom[i].Count];
                for (int i0 = 0; i0 < prom[i].Count; i0++)
                {
                    for_inter[0][i0] = prom[i][i0].x;
                    for_inter[1][i0] = prom[i][i0].y;
                }
                double[][] beze = GetInterPointsBeze(for_inter, counts_foreach[i]);
                for (int i0 = 0; i0 < beze[0].Length; i0++)
                {
                    added.Add(new Vector2((float)beze[0][i0], (float)beze[1][i0]));
                }
                ret.AddRange(added);
            }

            return ret;
        }

        public static TimingPoints GetNearestTimingPoint(int timestamp)
        {
            TimingPoints ret = GlobalValues.GlobalMap.Settings.TimePoints[0];
            foreach(var t in GlobalValues.GlobalMap.Settings.TimePoints)
            {
                if (timestamp >= t.time) { ret = t; }
                else { break; }
            }
            return (TimingPoints)ret.Clone();
        }

        public static TimingPoints GetNearestParentTimingPoint_Left(int timestamp)
        {
            TimingPoints ret = GlobalValues.GlobalMap.Settings.TimePoints[0];

            if (timestamp <= ret.time) { return (TimingPoints)ret.Clone(); }
            foreach(var t in GlobalValues.GlobalMap.Settings.TimePoints)
            {
                if (t.isParent)
                {
                    
                    if (timestamp > t.time) { ret = t; }
                    else { Debug.Log(t.time); break; }
                }
            }
            return (TimingPoints)ret.Clone();
        }

        public static OsuElement GetElFromTimestamp(int timestamp)
        {
            foreach(var t in GlobalValues.GlobalMap.AllElements)
            {
                if (t.timestamp == timestamp) { return t; }
            }
            return null;
        }
    }
}