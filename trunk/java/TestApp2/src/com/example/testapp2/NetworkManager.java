/*package com.example.testapp2;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

import android.os.AsyncTask;

public class NetworkManager 
{
	final String sIP = "172.21.27.138";
	final int iPort = 4449;
	
	private Socket socket = null;
	
	
	public void sendMessage()
	{
		
	}
	
	
	public void closeConnection()
	{
		if (socket != null && !socket.isClosed())
		{
			try {
				socket.close();
				socket = null;
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

}


class SendMessageAsync2 extends AsyncTask<String, Void, String> 
{ 
	 
    private Exception exception; 
    private PrintWriter out;
    Socket socket = null;
    
    String m = "";
    
    MainActivity act;
    public SendMessageAsync2(MainActivity a)
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
	        	if (socket == null){
	        		socket = new Socket("172.21.26.182", 4449);
	        	}
	        	
	        	
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
 */
