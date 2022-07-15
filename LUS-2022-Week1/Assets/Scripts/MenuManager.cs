using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviourPunCallbacks {
	[SerializeField] TwitchChat twitch;

	[SerializeField] TMP_InputField nameField; 
	[SerializeField] TMP_InputField joinField; 
	[SerializeField] TMP_InputField TwitchChannelField; 
	[SerializeField] TMP_InputField TwitchAuthTokenField; 

	[SerializeField] GameObject TitleMenu;
	[SerializeField] GameObject LobbyMenu;
	[SerializeField] GameObject ConnectingAlert;

	[SerializeField] GameObject TemplateUser;
	[SerializeField] GameObject UserScrollWindow;

	[SerializeField] TextMeshProUGUI RoomLabel;

	List<Player> Players = new List<Player>();
	List<GameObject> PlayerEntries = new List<GameObject>();

	void Awake() {
		TitleMenu.SetActive(false);
		LobbyMenu.SetActive(false);
		ConnectingAlert.SetActive(true);
		PhotonNetwork.ConnectUsingSettings();

		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public void JoinButton() {
		string roomName = joinField.text;
		RoomLabel.text = roomName;
		PhotonNetwork.LocalPlayer.NickName = nameField.text;
		JoinRoom(roomName);
	}

	public void CreateButton() {
		int length = 5;
	    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	    string roomName = new string(Enumerable.Repeat(chars, length)
			.Select(s => s[Random.Range(0, s.Length)]).ToArray());

		RoomLabel.text = roomName;
		PhotonNetwork.LocalPlayer.NickName = nameField.text;

		GUIUtility.systemCopyBuffer = roomName;
		CreateRoom(roomName);
	}

	public void PlayButton() {
		PhotonView photonView = PhotonView.Get(this);
		photonView.RPC("PlayForAll", RpcTarget.All);
	}

	public void SubmitButton() {
		twitch.username = TwitchChannelField.text;
		twitch.channelName = TwitchChannelField.text;
		twitch.password = TwitchAuthTokenField.text;
		twitch.Connect();
	}

	[PunRPC]
	void PlayForAll() {
		if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("MultiPlayerVersion");
	}

	void ReloadLobbyEntries()
	{
		foreach (GameObject go in PlayerEntries)
		{
			Destroy(go);
		}

		PlayerEntries.Clear();

		foreach (Player player in PhotonNetwork.PlayerList)
		{
			GameObject playerEntry = Instantiate(TemplateUser, UserScrollWindow.transform);
			playerEntry.GetComponent<TextMeshProUGUI>().text = player.NickName;
			playerEntry.SetActive(true);
			PlayerEntries.Add(playerEntry);
		}
	}

	#region PUN CALLBACKS

	void JoinRoom(string roomName) {
		PhotonNetwork.JoinRoom(roomName);
	}

	void CreateRoom(string roomName) {
		PhotonNetwork.CreateRoom(roomName);
	}

	public override void OnConnectedToMaster() {
		ConnectingAlert.SetActive(false);
		TitleMenu.SetActive(true);
	}

	public override void OnJoinedRoom() {
		TitleMenu.SetActive(false);
		LobbyMenu.SetActive(true);

		ReloadLobbyEntries();
	}

	public override void OnPlayerEnteredRoom(Player other) {
		ReloadLobbyEntries();
	}

	#endregion
}
