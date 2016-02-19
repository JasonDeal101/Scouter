package md5c8c64832fc43941a42b83cf9ec40e3e8;


public class EmailManager
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Scouter.FileManagment.EmailManager, Scouter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EmailManager.class, __md_methods);
	}


	public EmailManager () throws java.lang.Throwable
	{
		super ();
		if (getClass () == EmailManager.class)
			mono.android.TypeManager.Activate ("Scouter.FileManagment.EmailManager, Scouter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public EmailManager (java.lang.String p0, java.lang.String p1, java.lang.String p2, java.lang.String p3) throws java.lang.Throwable
	{
		super ();
		if (getClass () == EmailManager.class)
			mono.android.TypeManager.Activate ("Scouter.FileManagment.EmailManager, Scouter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
