using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{

	public Button CreateRoom;
	public Button JoinRoom;
	public InputField RoomNameField;
	public GameObject MenuGO;

	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
		CreateRoom.onClick.AddListener (() => {
			PhotonNetwork.CreateRoom (RoomNameField.text);
		});
		JoinRoom.onClick.AddListener (() => {
			PhotonNetwork.JoinRoom (RoomNameField.text);
		});
	}

}
