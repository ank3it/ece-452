package com.example.testapp2;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.app.Activity;
import android.content.Intent;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.support.v4.app.NavUtils;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;

public class MainActivity extends Activity implements OnItemSelectedListener{

	
	public final Handler mHandler = new Handler();
	
	

	public String menuItems = "";
    public final Runnable mUpdateMenu = new Runnable() {
        public void run() {
        	createMenu(menuItems);
        }
    };
	
		
 //  MessagesTask mt = null;
	  
    @Override
    public void onCreate(Bundle savedInstanceState) {
    	//startup logic that happens only once for entire lifecycle of activity
    	
        super.onCreate(savedInstanceState);
        
        
        //set UI layout
        setContentView(R.layout.activity_main);
        
        
        SendMessageAsync a = new SendMessageAsync(this);
        a.message("1,getRestaurants,");
        
        
        a.execute("1,getRestaurants,");        
     
        //then onStart, onResume are called
    }

    @Override
    public void onStart() {
    	super.onStart();
    
    	//CreateMenu();
    }
    
    public void createMenu(String str)
    {    	
        Spinner s = (Spinner)this.findViewById(R.id.restaurant_spinner);
        
        ArrayAdapter<CharSequence> adapter = new ArrayAdapter<CharSequence>(this, android.R.layout.simple_spinner_item); 
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);  
		s.setAdapter(adapter);    
		adapter.add(str);
		//adapter.add("Restaurant 2");
		//adapter.add("Restaurant 3");
		
		s.setOnItemSelectedListener(this);    	
    }
    
    @Override
    public void onDestroy() {
    	super.onDestroy();
    	//called after onStop, onPause
    }
    
    @Override
    public void onPause() {
    	super.onPause();
    	//eg during dialog popup
    	//or user could be leaving thsi gets called first
    	//stop animations, other actions
    	//commit unsaved chagnes
    	//release system resources that mya effec battery life
    	
    }    
    
    @Override
    public void onResume() {
    	super.onResume();
    	//after start or after pause
    	//called whenever activity comes into foreground
    	//initialize components which are released in onPause()
    	
    	
    } 
    
    @Override
    public void onStop() {
    	super.onStop();
    	//eg user switches to another app
    	//eg new activity is started another activity
    	//eg user receives phone call
    	
    	//system might now call the app if needed w/o calling ondestroy
    	//save stuff to persistent storage to avoid loss of info
    	//Note: system keeps track of input in View objects
    	
    } 
    
    @Override
    public void onRestart() {
    	super.onRestart();
    	
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
    	super.onCreateOptionsMenu(menu);
        getMenuInflater().inflate(R.menu.activity_main, menu);
        return true;
    }

    public void openMenu(View view)
    {
    	//take the selection and pass it to the next activity
    	Intent intent = new Intent(this, MenuActivity.class);
    	
    	intent.putExtra("SOME_MSG", "THE MESSAGE");
    	//EditText editText = (EditText) findViewById(R.id.edit_message);
    	//String message = editText.getText().toString();
    	
    	startActivity(intent);
    }
    
    public void sendMessage(View view) {
    	//Intent intent = new Intent(this, DisplayMessageActivity.class);
    	

    	//	intent.putExtra("MSG", "message sent!");
    	
    	//startActivity(intent);
    	
    	/*
    	if (mt == null)
    	{
    		mt = new MessagesTask();   		    		
    	}
    	mt.execute("sfsf");       
    	*/
    }

	@Override
	public void onItemSelected(AdapterView<?> parent, View view, int pos,
			long id) {
		// TODO Auto-generated method stub
		String s = (String)parent.getItemAtPosition(pos);
		int i  = 3;
		
		 		
	}

	@Override
	public void onNothingSelected(AdapterView<?> arg0) {
		// TODO Auto-generated method stub
		
	}          
}
    	
    

class SendMessageAsync extends AsyncTask<String, Void, String> 
{ 
	 
    private Exception exception; 
    private PrintWriter out;
    Socket socket = null;
    
    String m = "";
    
    MainActivity act;
    public SendMessageAsync(MainActivity a)
    {
    	act = a;
    }
    public void message(String msg)
    {
    	m = msg;
    }
    
    
    protected String doInBackground(String... urls) 
    { 
    	String line = "";
        try { 
	        //InetAddress serverAddr = null;
	        try {
	        //	serverAddr = InetAddress.getByName("localhost");
	        	
 
		        
	         //   Socket mySocket = new Socket(serverAddr, 666);
	        	if (socket == null)
	        		socket = new Socket("172.21.26.182", 4449);
	        	 // Socket mySocket = new Socket("10.0.2.2", 5001);
		        out = new PrintWriter(socket.getOutputStream(), true);
		        
		       // String s = urls.toString(); //for some reason necessary
		        out.println(m);
		        
		        
		        BufferedReader in = null;
		        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
		        
		        
		        while(true){
		            try{
		              line = in.readLine();
		              break;
		              
		            } catch (IOException e) {
		              System.out.println("Read failed");
		              System.exit(-1);
		            }
		          }		        
	        } catch (UnknownHostException e) {
	            e.printStackTrace();
	        } catch (IOException e) {
	            e.printStackTrace();
	        }
	        act.menuItems = line;
	        act.mHandler.post(act.mUpdateMenu);
	        
	             
        } catch (Exception e) { 
            this.exception = e; 
      
        }
	//	return "BLAH";
		return null;	
    }    
 
    protected void onPostExecute() { 
        // TODO: check this.exception  
        // TODO: do something with the feed 
    } 
 } 

/*
class MessagesTask extends AsyncTask<String, Void, String> 
{ 
	 
    private Exception exception; 
    private PrintWriter out;
    Socket socket = null;
    protected String doInBackground(String... urls) 
    {
    	return null;
    }
    	 //InetAddress serverAddr = null;
    	
	        try {
	        //	serverAddr = InetAddress.getByName("localhost");
	        	
	
		        
	         //   Socket mySocket = new Socket(serverAddr, 666);
	        	if (socket == null)
	        	{
	        		socket = new Socket("192.168.0.11", 4449);
	        		out = new PrintWriter(socket.getOutputStream(), true);
	        		out.println("Created new andriod socket");
	        	}
	        	 // Socket mySocket = new Socket("10.0.2.2", 5001);
		        
		        out.println("Sending msg1 from Android<END>");
		        out.println("Sending msg2 from Android<END>");
	        } catch (UnknownHostException e) {
	            e.printStackTrace();
	        } catch (IOException e) {
	            e.printStackTrace();
	        }    
	        
    	} catch (Exception e) {
    		e.printStackTrace();
    	
    	return null;
    
    
    protected void onPostExecute() { 
        // TODO: check this.exception  
        // TODO: do something with the feed 
    }     
}*/
