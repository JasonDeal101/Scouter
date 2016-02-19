using System;

namespace Scouter
{
	public class ScoutUtility
	{
		public ScoutUtility ()
		{
		}

		public string[] GetFiles()
		{
            var Path = Android.OS.Environment.GetExternalStoragePublicDirectory("/CVS");
            string[] files = Path.
        }
	}
}

