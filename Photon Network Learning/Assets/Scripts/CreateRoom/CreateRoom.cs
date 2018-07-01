﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {

	[SerializeField]
	private Text _roomName;
	private Text RoomName 
	{
		get {return _roomName;}
	}

	public void OnClick_CreateRoom()
	{
		if (PhotonNetwork.CreateRoom (RoomName.text)) 
		{
			print ("create room successfully sent.");
		} 
		else 
		{
			print ("create room failed to send");
		}
	}

	private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
	{
		print ("create room failed: " + codeAndMessage [1]);
	}

	private void OnCreatedRoom()
	{
		print ("room created successfully");
	}

}