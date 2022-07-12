using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourPunCallbacks {
	[SerializeField] TMP_InputField joinField; 
	[SerializeField] TextMeshProUGUI createLabel; 
	[SerializeField] Button playButton;

	bool canInteract = false;

	void Awake() {
		PhotonNetwork.ConnectUsingSettings();
	}

	public void JoinButton() {
		if (!canInteract) {
			createLabel.text = "Not connected";
			return;
		}

		string roomName = joinField.text;
		createLabel.text = roomName;

		JoinRoom(roomName);
	}

	public void CreateButton() {
		if (!canInteract) {
			createLabel.text = "Not connected";
			return;
		}

		int length = 5;
	    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	    string roomName = new string(Enumerable.Repeat(chars, length)
			.Select(s => s[Random.Range(0, s.Length)]).ToArray());

		createLabel.text = roomName;
		GUIUtility.systemCopyBuffer = roomName;

		CreateRoom(roomName);
	}

	public void PlayButton() {
		print("play button");
		PhotonView photonView = PhotonView.Get(this);
		photonView.RPC("PlayForAll", RpcTarget.All);
	}

	[PunRPC]
	void PlayForAll() {
		SceneManager.LoadScene("Main");
	}

	#region NETWORKING

	// TODO: JoinRandom?

	void JoinRoom(string roomName) {
		PhotonNetwork.JoinRoom(roomName);
	}

	void CreateRoom(string roomName) {
		PhotonNetwork.CreateRoom(roomName);
	}

	public override void OnConnectedToMaster() {
		print("Conneced to master");
		canInteract = true;
	}

	public override void OnPlayerEnteredRoom(Player other) {
		print("joined");
	}

	#endregion
}
