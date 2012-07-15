package com.example.testapp2;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.SocketAddress;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

public class CommunicationTask extends AsyncTask<String, String, Void> {
	public static final String ACTION_DELIM = ";END;";
	public static final String SOCKET_TERMINATION_CMD = ";QUIT;";
	
	private Socket socket;
	private DataOutputStream oStream = null;
	private DataInputStream iStream = null;
	private Context mContext;
	
	public void setContext(Context context) {
		this.mContext = context;
	}

	@Override
	protected Void doInBackground(String... params) {
		try {
			Log.i("NetworkTask", "doInBackground: Creating socket...");
			Log.i("NetworkTask", "doInBackground: Using ip address=" + params[0] + " and port number=" + params[1]);
			
			SocketAddress socketAddress = new InetSocketAddress("169.254.207.133", 4449);
			socket = new Socket();
			socket.connect(socketAddress, 5000);
			
			Log.i(this.getClass().getName(), "--------- connected to server");
			
			if (socket.isConnected()) {
				oStream = new DataOutputStream(socket.getOutputStream());
				iStream = new DataInputStream(socket.getInputStream());
				
				Log.i("NetworkTask", "doInBackground: Connected to remote host, streams assigned");
				
				// Keep on reading from/to the socket till we receive the "QUIT" from server,
				String responseLine;
				while ((responseLine = iStream.readLine()) != null) {
					Log.i("NetworkTask", "Server Says: " + responseLine);
					publishProgress(responseLine);
				}
			}
		} catch (IOException e) {
			Log.i("NetworkTask", "doInBackground: " + e);
		} catch (Exception e) {
			Log.i("NetworkTask", "doInBackground: " + e);
		} finally {
			// Close open sockets and streams
			try {
				oStream.close();
				iStream.close();
				socket.close();
			} catch (IOException e) {
				Log.i("NetworkTask", "doInBackground: " + e);
			} catch (Exception e) {
				Log.i("NetworkTask", "doInBackground: " + e);
			}
		}
		return null;
	}
	
	public void SendData(String data) {
		Log.i(this.getClass().getName(), "---------> sendData()");
		try {
			if (socket.isConnected()) {
				oStream.writeBytes(data + "\n");			// \n is needed for this to work
				
				Log.i("NetworkTask", "SendData: Wrote received data to socket");
			} else {
				Log.i("NetworkTask", "SendData: Cannot send data. Socket is closed");
			}
		} catch (Exception e) {
			Log.i("NetworkTask", "SendData: Failure");
		}
	}
	
	@Override
	protected void onProgressUpdate(String... values) {		
		if (values[0].contains("REPLY_MENU")) {
			((MenuActivity) mContext).updateMenuList(values[0]);
		}
	}
	
	@Override
	protected void onCancelled() {
		Log.i("NetworkTask", "NetworkTask cancelled");
	}
	
	/**
	 * Given a string of parameters, extracts the values of the specified parameter
	 * @param str The string of parameters
	 * @param param The parameter for which the value needs to be extracted
	 * @return Value of given parameter in the string if it exists, else null 
	 */
	private String extractParameterValue(String str, String param) {
		int startingIndex = str.indexOf(':', str.indexOf(param));
		int endingIndex = str.indexOf(';', startingIndex);
		
		Log.i("NetworkTask", "extractParameterValue: result = " + str.substring(startingIndex + 1, endingIndex));
		
		return (startingIndex == -1 || endingIndex == -1) ? null : str.substring(startingIndex + 1, endingIndex);
	}
}
