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
    class EmailManager
    {
        string mFilePath;
        string mEmail;
        Intent mMail = new Intent(Android.Content.Intent.ActionSend);
        public EmailManager(string filePath, string email)
        {
            mFilePath = filePath;
            mEmail = email;
            
            
        }
    }
}