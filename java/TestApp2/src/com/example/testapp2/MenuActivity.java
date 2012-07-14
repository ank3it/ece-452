package com.example.testapp2;


import java.io.IOException;
import java.io.PrintWriter;
import java.net.*;
import java.util.List;
import java.util.Map;
import java.util.Vector;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ListActivity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.SimpleAdapter;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.view.View;
import android.view.Menu;
import android.widget.TableRow.LayoutParams;

public class MenuActivity extends Activity{

	   public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        
	        this.setContentView(R.layout.activity_menu);

	
	        TableLayout tl = (TableLayout)findViewById(R.id.myTableLayout);
	               
	     
	               TableRow tr = new TableRow(this);
	               tr.setLayoutParams(new LayoutParams(
	                              LayoutParams.FILL_PARENT,
	                              LayoutParams.WRAP_CONTENT));
	                    /* Create a Button to be the row-content. */
	                    Button b = new Button(this);
	                    b.setText("blah");
	                    b.setLayoutParams(new LayoutParams(
	                              LayoutParams.FILL_PARENT,
	                              LayoutParams.WRAP_CONTENT));
	                    /* Add Button to row. */
	                    tr.addView(b);
	          /* Add row to TableLayout. */
	          tl.addView(tr,new TableLayout.LayoutParams(
	                    LayoutParams.FILL_PARENT,
	                    LayoutParams.WRAP_CONTENT));
	          
	          
	        Intent intent = getIntent();
	        String message = intent.getStringExtra("SOME_MSG");
	        /*
	        Map<String, String> map;
	        map.put("A", "val1");
	        List<Map<String, String>> l ;
	        l.add(map);
	        
	        
	    	String[] values = new String[] { "Android", "iPhone", "WindowsMobile",
					"Blackberry", "WebOS", "Ubuntu", "Windows7", "Max OS X",
					"Linux", "OS/2" };
	    	
	    	SimpleAdapter a = new SimpleAdapter(this, null, 0, values, null);
			ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
					android.R.layout.simple_list_item_2, values, sadf);
			setListAdapter(adapter);
	        
	        */
	        
	        
	        /*
	        new RetreiveFeedTask().execute("adf");

	        // Create the text view
	        TextView textView = new TextView(this);
	        textView.setTextSize(40);
	        textView.setText("some text");

	        setContentView(textView);		        */
       
	    }
}



