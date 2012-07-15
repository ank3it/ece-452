package com.example.testapp2;

import android.app.Application;

public class MyApplication extends Application {
	public static CommunicationTask communicationTask;
	
	@Override
	public void onCreate() {
		communicationTask = new CommunicationTask();
		communicationTask.execute("169.254.207.133", "4449");
	}
}
