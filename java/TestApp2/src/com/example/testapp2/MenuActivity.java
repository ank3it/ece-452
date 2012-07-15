package com.example.testapp2;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.SimpleAdapter;
import android.widget.TextView;

public class MenuActivity extends ListActivity {
	private String resturantName;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		// Extract resturant name from intent
		Intent intent = getIntent();
		resturantName = intent.getStringExtra("resturantName");
		
		// Send request to server
		String message = "REQUEST_MENU:" + resturantName;
		MyApplication.communicationTask.setContext(this);
		MyApplication.communicationTask.SendData(message);
		
//		List<HashMap<String, String>> fillMaps = new ArrayList<HashMap<String, String>>();
//		for (int i = 0; i < 5; i++) {
//			HashMap<String, String> map = new HashMap<String, String>();
//			map.put("name", "subway melt");
//			map.put("price", "12.99");
//			fillMaps.add(map);
//		}
//		
//		String[] from = new String[] {"name", "price"};
//		int[] to = new int[] {R.id.menuItemName, R.id.menuItemPrice};
//		
//		SimpleAdapter adapter = new SimpleAdapter(this, fillMaps, R.layout.menu_list_item, from, to);
//		setListAdapter(adapter);
		getListView().setOnItemClickListener(listItemClickListener);
	}
	
	private OnItemClickListener listItemClickListener = new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {
			TextView tv = (TextView) view.findViewById(R.id.menuItemName);
			String menuItemName = tv.getText().toString();
			tv = (TextView) view.findViewById(R.id.menuItemPrice);
			double menuItemPrice = Double.parseDouble(tv.getText().toString());
			
			Intent intent = new Intent(parent.getContext(), OrderActivity.class);
			intent.putExtra("resturantName", resturantName);
			intent.putExtra("menuItemName", menuItemName);
			intent.putExtra("menuItemPrice", menuItemPrice);
			startActivity(intent);
		}
	};
	
	public void updateMenuList(String response) {
		response = response.substring(11);
		
		List<HashMap<String, String>> data = new ArrayList<HashMap<String, String>>();
		String[] splitString = response.split(",");
		for (int i = 0; i < splitString.length; i += 2) {
			HashMap<String, String> map = new HashMap<String, String>();
			map.put("name", splitString[i]);
			map.put("price", splitString[i + 1]);
			data.add(map);
		}
		
		String[] from = new String[] {"name", "price"};
		int[] to = new int[] {R.id.menuItemName, R.id.menuItemPrice};
		SimpleAdapter adapter = new SimpleAdapter(this, data, R.layout.menu_list_item, from, to);
		setListAdapter(adapter);
		
		Log.i(this.getClass().getName(), "-------------- updated menu list");
	}
	
//	public class MenuItem {
//		public String name;
//		public double price;
//		
//		public MenuItem(String name, double price) {
//			this.name = name;
//			this.price = price;
//		}
//	}
}
