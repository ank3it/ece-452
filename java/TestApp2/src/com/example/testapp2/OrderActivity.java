package com.example.testapp2;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class OrderActivity extends Activity {
	private String resturantName;
	private String menuItemName;
	private double menuItemPrice;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		// Get extras
		Intent intent = getIntent();
		resturantName = intent.getStringExtra("resturantName");
		menuItemName = intent.getStringExtra("menuItemName");
		menuItemPrice = intent.getDoubleExtra("menuItemPrice", 0);
		
		setContentView(R.layout.activity_order);
		
		Button orderButton = (Button) findViewById(R.id.orderButton);
		orderButton.setOnClickListener(buttonClickListener);
	}

	private OnClickListener buttonClickListener = new OnClickListener() {
		@Override
		public void onClick(View v) {
			String name = ((TextView) findViewById(R.id.nameField)).getText().toString();
			String address = ((TextView) findViewById(R.id.addressField)).getText().toString();
			String email = ((TextView) findViewById(R.id.emailField)).getText().toString();
			String phone = ((TextView) findViewById(R.id.phoneField)).getText().toString();
			String quantity = ((TextView) findViewById(R.id.quantityField)).getText().toString();
			
			StringBuilder sb = new StringBuilder();
			sb.append("PLACE_ORDER:");
			sb.append(name);
			sb.append(",");
			sb.append(address);
			sb.append(",");
			sb.append(phone);
			sb.append(",");
			sb.append(resturantName);
			sb.append(",");
			sb.append(menuItemName);
			sb.append(",");
			sb.append(quantity);
			
			MyApplication.communicationTask.setContext(v.getContext());
			MyApplication.communicationTask.SendData(sb.toString());
		}
	};
}
