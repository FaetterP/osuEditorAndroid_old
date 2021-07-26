using Assets.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Mapinfo;


namespace Assets.EditorOSU
{
	class TimelineStepSlider : MonoBehaviour
	{
		private Slider thisSlider;
		[SerializeField] private AudioSource music;
		[SerializeField] private TimeMark mark;
		[SerializeField] private NoteTimeMark note_mark, slider_mark, spinner_mark;
		[SerializeField] private Text text;
		private List<TimingPoints> ParentTimingPoints = new List<TimingPoints>();
		private bool isFirst = true;

		void Awake()
		{
			thisSlider = GetComponent<Slider>();
			thisSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
			thisSlider.onValueChanged.AddListener(delegate { UpdateNoteMarks(); });
		}

		void Start()
		{
			UpdateParentsTimingpoints();
			thisSlider.value = GlobalValues.GlobalMap.Settings.EditorInfo.BeatDivisor;
			ValueChangeCheck();
			UpdateNoteMarks();
		}

		public void OnEnable()
		{
			if (isFirst) { isFirst = false; return; }
			UpdateParentsTimingpoints();
			thisSlider.value = GlobalValues.GlobalMap.Settings.EditorInfo.BeatDivisor;
			ValueChangeCheck();
			UpdateNoteMarks();
			foreach (var t in FindObjectsOfType<OsuElement>())
			{
				ControllerApproachingDetails.DeleteTimestampFromScreen(t.timestamp);
				Destroy(t.gameObject);
			}
		}

		public void UpdateParentsTimingpoints()
		{
			ParentTimingPoints.Clear();
			foreach (var t in GlobalValues.GlobalMap.Settings.TimePoints)
			{
				if (t.isParent) { ParentTimingPoints.Add(t); }
			}
		}

		public void ValueChangeCheck_DONT_USE()
		{
			if (thisSlider.value == 7) { thisSlider.value = 8; }
			//		if (thisSlider.value == 9) { thisSlider.value = 10; }
			//		if (thisSlider.value == 11) { thisSlider.value = 12; }
			if (thisSlider.value == 9 || thisSlider.value == 10 || thisSlider.value == 11) { thisSlider.value = 12; }
			if (thisSlider.value == 13 || thisSlider.value == 14 || thisSlider.value == 15) { thisSlider.value = 16; }
			text.text = "1/" + thisSlider.value;

			GlobalValues.GlobalMap.TimeMarks.Clear();
			int length = (int)(music.clip.length * 1000);
			for (int i = 0; i * 60000 / (GlobalValues.GlobalMap.bpm * thisSlider.value) < length; i++)
			{
				TimeMark go = (TimeMark)mark.Clone();
				go.SetTimestamp((int)(i * 60000 / (GlobalValues.GlobalMap.bpm * thisSlider.value) + GlobalValues.GlobalMap.offset));
				switch ((int)thisSlider.value)
				{
					case 1:
						go.color = Color.white;
						go.height = 100;
						break;
					case 2:
						if (i % 2 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;
					case 3:
						if (i % 3 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else
						{
							go.color = Color.magenta;
							go.height = 50;
						}
						break;
					case 4:
						if (i % 4 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else if (i % 4 == 1 || i % 4 == 3)
						{
							go.color = Color.blue;
							go.height = 50;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;
					case 5:
						if (i % 5 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else
						{
							go.color = Color.yellow;
							go.height = 50;
						}
						break;
					case 6:
						if (i % 6 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else if (i % 6 == 1 || i % 6 == 2 || i % 6 == 4 || i % 6 == 5)
						{
							go.color = Color.magenta;
							go.height = 50;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;
					case 8:
						if (i % 8 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else if (i % 8 == 1 || i % 8 == 3 || i % 8 == 5 || i % 8 == 7)
						{
							go.color = Color.yellow;
							go.height = 50;
						}
						else if (i % 8 == 2 || i % 8 == 6)
						{
							go.color = Color.blue;
							go.height = 50;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;
					case 12:
						if (i % 12 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else if (i % 12 == 1 || i % 12 == 5 || i % 12 == 7 || i % 12 == 11)
						{
							go.color = Color.gray;
							go.height = 50;
						}
						else if (i % 12 == 2 || i % 12 == 4 || i % 12 == 8 || i % 12 == 10)
						{
							go.color = Color.magenta;
							go.height = 50;
						}
						else if (i % 12 == 3 || i % 12 == 9)
						{
							go.color = Color.blue;
							go.height = 50;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;
					case 16:
						if (i % 16 == 0)
						{
							go.color = Color.white;
							go.height = 100;
						}
						else if (i % 16 == 1 || i % 16 == 3 || i % 16 == 5 || i % 16 == 7 || i % 16 == 9 || i % 16 == 11 || i % 16 == 13 || i % 16 == 15)
						{
							go.color = Color.gray;
							go.height = 50;
						}
						else if (i % 16 == 2 || i % 16 == 6 || i % 16 == 10 || i % 16 == 14)
						{
							go.color = Color.magenta;
							go.height = 50;
						}
						else if (i % 16 == 4 || i % 16 == 12)
						{
							go.color = Color.blue;
							go.height = 50;
						}
						else
						{
							go.color = Color.red;
							go.height = 50;
						}
						break;

				}
				GlobalValues.GlobalMap.TimeMarks.Add(go);
			}
			foreach (GameObject t in GameObject.FindGameObjectsWithTag("TimeMark"))
			{
				ControllerApproachingTimeMarks.DeleteTimestampFromScreen(t.GetComponent<TimeMark>().GetTimestamp());
				Destroy(t);
			}
		}

		public void ValueChangeCheck()
		{
			if (thisSlider.value == 10 || thisSlider.value == 11) { thisSlider.value = 12; }
			if (thisSlider.value == 13 || thisSlider.value == 14 || thisSlider.value == 15) { thisSlider.value = 16; }
			GlobalValues.GlobalMap.Settings.EditorInfo.BeatDivisor = (int)thisSlider.value;
			text.text = "1/" + thisSlider.value;

			GlobalValues.GlobalMap.TimeMarks.Clear();
			for(int i = 0; i < ParentTimingPoints.Count; i++)
			{
				var currTP = ParentTimingPoints[i];
				TimeMark newBPMMark = (TimeMark)mark.Clone();
				newBPMMark.SetTimestamp((currTP.time));
				newBPMMark.height = 100;
				newBPMMark.color = Color.cyan;
				GlobalValues.GlobalMap.TimeMarks.Add(newBPMMark);


				if (i != ParentTimingPoints.Count - 1)
				{
					
					var nextTP = ParentTimingPoints[i + 1];
					for (int i0 = 1; i0 * Math.Abs(currTP.beatLength) + currTP.time < nextTP.time; i0++)
					{
						TimeMark go = (TimeMark)mark.Clone();
						go.SetTimestamp((int)(i0 * currTP.beatLength + currTP.time));
						go.height = 100;
						go.color = Color.white;
						GlobalValues.GlobalMap.TimeMarks.Add(go);
					}
				}
				else
				{
					for (int i0 = 1; i0 * currTP.beatLength + currTP.time < (int)(music.clip.length*1000); i0++)
					{
						TimeMark go = (TimeMark)mark.Clone();
						go.SetTimestamp((int)(i0 * currTP.beatLength + currTP.time));
						go.height = 100;
						go.color = Color.white;
						GlobalValues.GlobalMap.TimeMarks.Add(go);
					}
				}
			}
			switch ((int)thisSlider.value)
			{
				case 2:
					Divide(2, 50, Color.red);
					break;
				case 3:
					Divide(3, 50, Color.magenta);
					break;
				case 4:
					Divide(2, 50, Color.red);
					Divide(2, 50, Color.blue);
					break;
				case 5:
					Divide(5, 50, Color.yellow);
					break;
				case 6:
					Divide(2, 50, Color.red);
					Divide(3, 50, Color.magenta);
					break;
				case 7:
					Divide(7, 50, Color.yellow);
					break;
				case 8:
					Divide(2, 50, Color.red);
					Divide(2, 50, Color.blue);
					Divide(2, 50, Color.yellow);
					break;
				case 9:
					Divide(3, 50, Color.magenta);
					Divide(3, 50, Color.yellow);
					break;
				case 12:
					Divide(2, 50, Color.red);
					Divide(3, 50, Color.magenta);
					Divide(2, 50, Color.blue);
					break;
				case 16:
					Divide(2, 50, Color.red);
					Divide(2, 50, Color.blue);
					Divide(2, 50, Color.yellow);
					Divide(2, 50, Color.grey);
					break;
			}


			foreach (GameObject t in GameObject.FindGameObjectsWithTag("TimeMark"))
			{
				ControllerApproachingTimeMarks.DeleteTimestampFromScreen(t.GetComponent<TimeMark>().GetTimestamp());
				Destroy(t);
			}
		}

		private void Divide(int count, int heght, Color color)
		{
			List<TimeMark> added = new List<TimeMark>();
			for(int i = 0; i < GlobalValues.GlobalMap.TimeMarks.Count - 1; i++)
			{
				TimeMark curr = GlobalValues.GlobalMap.TimeMarks[i], next = GlobalValues.GlobalMap.TimeMarks[i+1];
				int razn = next.GetTimestamp() - curr.GetTimestamp();
				double h = 1.0*razn / count;
				for (int i0 = 1; i0 < count; i0++) 
				{
					TimeMark go = (TimeMark)mark.Clone();
					go.SetTimestamp(curr.GetTimestamp() + (int)(i0 * h));
					go.color = color;
					go.height = heght;
					added.Add(go);
				}
			}

			GlobalValues.GlobalMap.TimeMarks.AddRange(added);
			GlobalValues.GlobalMap.TimeMarks.Sort();
		}

		public void UpdateNoteMarks()
		{
			GlobalValues.GlobalMap.NoteTimeMarks.Clear();
			foreach (var t in GlobalValues.GlobalMap.AllElements)
			{
				if (t is Note)
				{
					NoteTimeMark added = (NoteTimeMark)note_mark.Clone();
					added.thisNote = (Note)(t as Note).Clone();
					added.SetTimestamp(added.thisNote.timestamp);
					GlobalValues.GlobalMap.NoteTimeMarks.Add(added);
				}
				else if (t is OsuSlider)
				{
					NoteTimeMark added = (NoteTimeMark)slider_mark.Clone();
					added.thisNote = (OsuSlider)(t as OsuSlider).Clone();
					added.SetTimestamp(added.thisNote.timestamp);
					GlobalValues.GlobalMap.NoteTimeMarks.Add(added);
					for (int i=1;i<=(t as OsuSlider).count_of_slides; i++)
					{
						added = (NoteTimeMark)slider_mark.Clone();
						added.thisNote = (OsuSlider)(t as OsuSlider).Clone();
						added.SetTimestamp((t.timestamp)+(int)((t as OsuSlider).sum_time*i));
						GlobalValues.GlobalMap.NoteTimeMarks.Add(added);
					}
				}
				else if (t is Spinner)
				{
					NoteTimeMark added = (NoteTimeMark)spinner_mark.Clone();
					added.thisNote = (Spinner)(t as Spinner).Clone();
					added.SetTimestamp(added.thisNote.timestamp);
					GlobalValues.GlobalMap.NoteTimeMarks.Add(added);

					added = (NoteTimeMark)spinner_mark.Clone();
					added.thisNote = (Spinner)(t as Spinner).Clone();
					added.SetTimestamp((t as Spinner).time_end);
					GlobalValues.GlobalMap.NoteTimeMarks.Add(added);
				}
			}
		}
	}
}
