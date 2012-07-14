package com.example.testapp2;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
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


public class MainActivity extends Activity implements OnItemSelectedListener{
	
	public final Handler mHandler = new Handler();
	public String menuItems = "";
    public final Runnable mUpdateMenu = new Runnable() {
    	//this is done because the socket stuff is on a separate thread from the UI
    	//menu/UI has to update dynamically when the results from the socket thread come back
    	//socket thread will invoke this method
        public void run() {
        	createMenu(menuItems);
        }
    };
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
    	//startup logic that happens only once for entire lifecycle of activity
    	
        super.onCreate(savedInstanceState);
        
        
        //set UI layout
        setContentView(R.layout.activity_main);
        
        
        SendMessageAsync a = new SendMessageAsync(this);
       
	    a.message("1,getRestaurants,"); //hack, see comments in method
	    a.execute(); //will send msg to server in separate thread and wait for reply     
     
        //then onStart, onResume are called
    }

    @Override
    public void onStart() {
    	super.onStart();

    }
    
    public void createMenu(String str)
    {    	
        Spinner s = (Spinner)this.findViewById(R.id.restaurant_spinner);
        
        ArrayAdapter<CharSequence> adapter = new ArrayAdapter<CharSequence>(this, android.R.layout.simple_spinner_item); 
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);  
		s.setAdapter(adapter);    
		adapter.add(str);
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
    
    public void onContinueClicked(View view)
    {
    	//switches activities for now
    	Intent intent = new Intent(this, MenuActivity.class);
    	
    	intent.putExtra("SOME_MSG", "THE MESSAGE"); //msg passed to other activity
    	//EditText editText = (EditText) findViewById(R.id.edit_message);
    	//String message = editText.getText().toString();
    	
    	startActivity(intent);
    }
    
	@Override
	public void onItemSelected(AdapterView<?> parent, View view, int pos,
			long id) {
		//can get and store the selected item here
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
    private PrintWriter out;
    private BufferedReader in = null;
    Socket socket = null;
    
    String m = "";
    
    MainActivity act;
    public SendMessageAsync(MainActivity a)
    {
    	//save activity so we can dynamically populate its menu based on results from server
    	//this is needed because the socket stuff is on a separate thread from the UI
    	act = a;
    }
    public void message(String msg)
    {
    	//hack...for some reason I cant figure out when i pass string directly to doInbackgroung
    	//it gets read in c# as java.lang.String instead of the actual avlue
    	//this provides a workaround..
    	m = msg;
    }
    
    protected String doInBackground(String... urls) 
    {     	
        try {
        	String line = "";
	        try 
	        {
	        	InetAddress serverAddr = InetAddress.getByName("172.21.26.182");
	        
	        	if (socket == null)
	        	{
	        		socket = new Socket(serverAddr, 4449);
	        		out = new PrintWriter(socket.getOutputStream(), true);
	        		in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
	        	}
		              	
	        	//write message to socket
		        out.println(m);
		        
		        //read message reply
		        while(true)
		        {
		            try
		            {
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
	        
	        //populates menu with retrieved items
	        act.menuItems = line;
	        act.mHandler.post(act.mUpdateMenu);	                    
        } 
        catch (Exception e) 
        { 
        	System.out.println(e.toString());            
        	e.printStackTrace();
        }

		return null;	
    }    
 
    protected void onPostExecute() { 
        // TODO: check this.exception  
        // TODO: do something with the feed 
    } 
 } 