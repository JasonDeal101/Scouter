using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using System.IO;

namespace Scouter
{
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mVP;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mVP = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mVP.Adapter = new SamplePagerAdpter();

            mSlidingTabScrollView.ViewPager = mVP;
        }

        public class SamplePagerAdpter : PagerAdapter
        {
            List<string> files = new List<string>();

            public SamplePagerAdpter() : base()
            {
                files.Add("28511");
                files.Add("28512");
                files.Add("28513");
            }

            public override int Count
            {
                get
                {
                    return files.Count;
                }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                //Sets view equal to options_item
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.options_item, container, false);
                container.AddView(view);

                char[] titleChar = files[position].ToCharArray();
                char[] teamNumberChar = new char[5];
                char matchNumber;

                for (int i = 0; i <= titleChar.Length; i++)
                {
                    if (i < 4)
                    {
                        teamNumberChar[i] = titleChar[i];
                    }
                    else
                    {
                        //matchNumber = titleChar[i];
                        break;
                    }
                }

                matchNumber = titleChar[4];
                string teamNumber = new string(teamNumberChar);

                //Create objects for items on the options_item layout
                TextView title = view.FindViewById<TextView>(Resource.Id.fileName);
                TextView txtMatchNumber = view.FindViewById<TextView>(Resource.Id.matchNumber);
                Button email = view.FindViewById<Button>(Resource.Id.emailFile);
                Button saveRaw = view.FindViewById<Button>(Resource.Id.saveRawToSD);
                Button saveCSV = view.FindViewById<Button>(Resource.Id.saveCSVToSD);

                title.Text = "Team Number: " + teamNumber;
                txtMatchNumber.Text = "Match Number: " + matchNumber.ToString();
                email.Click += Email_Click;
                saveRaw.Click += SaveRaw_Click;
                saveCSV.Click += SaveCSV_Click;

                return view;
            }

            private void SaveCSV_Click(object sender, EventArgs e)
            {
                //Saves file as a .csv(comma seperated values) file on the external sd card
                throw new NotImplementedException();
            }

            private void SaveRaw_Click(object sender, EventArgs e)
            {
                //Saves the raw file to the external sd card
                throw new NotImplementedException();
            }

            private void Email_Click(object sender, EventArgs e)
            {
                //Emails the file to wherever
                throw new NotImplementedException();
            }

            public string GetHeaderTitle(int pos)
            {
                return files[pos];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }

            private string[] GetFiles()
            {
                //Receives all file names from the Documents Directory
                string[] files = Directory.GetFiles(Android.OS.Environment.DirectoryDocuments);
                return files;
            }

            private void WriteFile(string contents, string fileName)
            {
                var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
                var fullPath = Path.Combine(path.ToString() + fileName + ".csv");

                using (var streamWriter = new StreamWriter(fullPath, false))
                {
                    streamWriter.WriteLine(contents);
                }
            }
            
            private string ReadFile(string fileName)
            {
                var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
                var filePath = Path.Combine(path.ToString(), fileName);
                string text = File.ReadAllText(path.ToString());
                return text;
            }

            private string defenseFromCode(char code)
            {
                switch (code.ToString())
                {
                    case "0":
                        return "Portcullis";
                    case "1":
                        return "Cheval de Frise";
                    case "2":
                        return "Ramparts";
                    case "3":
                        return "Moat";
                    case "4":
                        return "Drawbridge";
                    case "5":
                        return "Sally Port";
                    case "6":
                        return "Rock Wall";
                    case "7":
                        return "Rough Terrain";
                    case "8":
                        return "Low Bar";
                }
                return null;
            }
            private string defense(char code)
            {
                switch (code.ToString())
                {
                    case "0":
                        return "Portcullis";
                    case "1":
                        return "Cheval de Frise";
                    case "2":
                        return "Ramparts";
                    case "3":
                        return "Moat";
                    case "4":
                        return "Drawbridge";
                    case "5":
                        return "Sally Port";
                    case "6":
                        return "Rock Wall";
                    case "7":
                        return "Rough Terrain";
                    case "8":
                        return "Low Bar";
                }
                return null;
            }

            private string FileToCSVFormat(string fileName, int mode)
            {
                string text = ReadFile(fileName);
                char[] content = text.ToCharArray();
                char[] file = fileName.ToCharArray();
                char[] teamNumber = new char[4];
                char[] matchNumber = new char[2];
                
                for(int i = 0; i < file.Length; i++)//Gets team number and match number from file name
                {
                    if(i < 4)
                    {
                        teamNumber[i] = file[i];
                    }
                    else
                    {
                        matchNumber[i - 4] = file[i];
                    }
                }
                string scoutName = "unknown";
                switch(content[24].ToString())
                {
                    case "a":
                        scoutName = null;
                        break;
                    case "b":
                        scoutName = null;
                        break;
                    case "c":
                        scoutName = null;
                        break;
                    case "d":
                        scoutName = null;
                        break;
                    case "e":
                        scoutName = null;
                        break;
                    case "f":
                        scoutName = null;
                        break;
                    case "g":
                        scoutName = null;
                        break;
                    case "h":
                        scoutName = null;
                        break;
                    case "i":
                        scoutName = null;
                        break;
                    case "j":
                        scoutName = null;
                        break;
                    case "k":
                        scoutName = null;
                        break;
                    case "l":
                        scoutName = null;
                        break;
                    case "m":
                        scoutName = null;
                        break;
                    case "n":
                        scoutName = null;
                        break;
                    case "o":
                        scoutName = null;
                        break;
                    case "p":
                        scoutName = null;
                        break;
                    case "q":
                        scoutName = null;
                        break;
                    case "r":
                        scoutName = null;
                        break;
                    case "s":
                        scoutName = null;
                        break;
                    case "t":
                        scoutName = null;
                        break;
                    case "u":
                        scoutName = null;
                        break;
                    case "v":
                        scoutName = null;
                        break;
                    case "w":
                        scoutName = null;
                        break;
                    case "x":
                        scoutName = null;
                        break;
                    case "y":
                        scoutName = null;
                        break;
                    case "z":
                        scoutName = null;
                        break;
                }
                string txtMatch = new string(matchNumber);
                string txtTeam = new string(teamNumber);
                string newCSVText = content[29].ToString() + "," + scoutName + "," + txtMatch +
                    "," + txtTeam + "," + content[27].ToString() + "," + content[18].ToString() + ","
                    + content[16].ToString() + "," + content[19].ToString() + "," + content[17].ToString()
                    + content[22].ToString() + "," + content[20].ToString() + "," + content[23].ToString() + ","
                    + content[21].ToString() + "," + content[24].ToString() + "," + content[25].ToString() + ","
                    + content[26].ToString() + "," + defense(content[0]) + "," + content[1].ToString() + "," +
                    content[2].ToString() + "," + content[3].ToString() + "," + defenseFromCode(content[4]) + ","
                    + content[5].ToString() + "," + content[6].ToString() + "," + content[7].ToString() + ","
                    + defenseFromCode(content[8]) + "," + content[9].ToString() + "," + content[10].ToString()
                    + content[11].ToString() + "," + defenseFromCode(content[12]) + "," + content[13].ToString() +
                    content[14].ToString() + content[15].ToString();
                return newCSVText;
            }
        }
    }
}