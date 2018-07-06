using System.Collections;
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
        PhotonNetwork.JoinRandomRoom();
    }

    private void OnJoinedRoom()
    {
        print("joined room " + PhotonNetwork.room.Name);
        if (PhotonNetwork.playerList.Length == 2)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    private void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        string randRoomName = Random.Range(10000, 999999999).ToString();

        if (PhotonNetwork.CreateRoom(randRoomName, roomOptions, TypedLobby.Default))
        {
            print("created room " + randRoomName);
        }
        else
        {
            print("create room failed to send");
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
