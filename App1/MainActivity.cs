using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace Scouter
{
    [Activity(Label = "Scouter", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Sets the sliding tab fragment within the frame layout
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment STF = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, STF);
            transaction.Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //Inflates a custom action bar item
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        
    }
}

