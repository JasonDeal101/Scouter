using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;

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
        }
    }
}