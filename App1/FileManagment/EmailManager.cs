using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Scouter.FileManagment
{
    class EmailManager : Activity
    {
        Intent mMail = new Intent(Intent.ActionSend);
        
        public EmailManager(string filePath, string email, string teamNum, string matchNum)
        {
            mMail.PutExtra(Intent.ExtraEmail, email);//Sets Adressee
            mMail.PutExtra(Intent.ExtraSubject, "Team: " + teamNum + " Match: " + matchNum);//Sets subject
            mMail.SetType("message/rfc822");//Opens a mail application
            StartActivity(mMail);//Starts the mail intent
        }

    }
}