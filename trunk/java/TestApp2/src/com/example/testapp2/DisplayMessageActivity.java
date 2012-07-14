/*package com.example.testapp2;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.TextView;
import android.view.View;
import android.view.Menu;

public class DisplayMessageActivity extends Activity{

	   public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        
	        Intent intent = getIntent();
	        String message = intent.getStringExtra("MSG");
	        
	        new RetreiveFeedTask().execute("adf");

	        // Create the text view
	        TextView textView = new TextView(this);
	        textView.setTextSize(40);
	        textView.setText("some text");

	        setContentView(textView);		        
       
	    }
}


class RetreiveFeedTask extends AsyncTask<String, Void, String> 
{ 
	 
    private Exception exception; 
    private PrintWriter out;
    Socket socket = null;
    protected String doInBackground(String... urls) 
    { 
        try { 
	        //InetAddress serverAddr = null;
	        try {
	        //	serverAddr = InetAddress.getByName("localhost");
	        	
 
		        
	         //   Socket mySocket = new Socket(serverAddr, 666);
	        	if (socket == null)
	        		socket = new Socket("192.168.0.11", 4449);
	        	 // Socket mySocket = new Socket("10.0.2.2", 5001);
		        out = new PrintWriter(socket.getOutputStream(), true);
		        out.println("Sending msg1 from Android<END>");
		        out.println("Sending msg2 from Android<END>");
	        } catch (UnknownHostException e) {
	            e.printStackTrace();
	        } catch (IOException e) {
	            e.printStackTrace();
	        }
	             
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
*/
