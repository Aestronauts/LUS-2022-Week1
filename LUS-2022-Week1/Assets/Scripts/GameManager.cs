using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
	[SerializeField] GameObject InGameMenu;
	[SerializeField] GameObject VotingPanel;
	[SerializeField] float GameTimer = 60f;
	[SerializeField] TextMeshProUGUI TimerLabel;

	public static GameManager Instance;

	public bool Running = true;

	Texture2D MyScreenshotTexture;
	Texture2D ReceivedScreenshotTexture;

	public Dictionary<Player, Texture2D> Screenshots = new Dictionary<Player, Texture2D>();

    PhotonView view;
	ScreenshotHelper screenshotHelper;

    void Awake() {
		Instance = this;

        view = PhotonView.Get(this);
		screenshotHelper = GetComponent<ScreenshotHelper>();

		Running = true;
		VotingPanel.SetActive(false);
		InGameMenu.SetActive(true);
    }

    void Update() {
		GameTimer -= Time.deltaTime;

		if (Running)
		{
			TimerLabel.text = GameTimer.ToString("F2");
		}
		else
		{
			TimerLabel.text = "0.00";
		}

		if (GameTimer < 0f && Running)
		{
			Running = false;

			StartCoroutine(TakeScreenshot());
			if (view.IsMine)
            {
            }

			if (PhotonNetwork.IsMasterClient)
			{
				view.RPC("EndPlay", RpcTarget.All);
			}
		}
    }

    public IEnumerator TakeScreenshot()
    {
        screenshotHelper.TakeScreenshotWithoutUI();

		while (!screenshotHelper.finished)
		{
			yield return null;
		}


		MyScreenshotTexture = screenshotHelper.screenshotTexture;
		print(MyScreenshotTexture.EncodeToPNG().Length);
        view.RPC("SendData", RpcTarget.All, MyScreenshotTexture.EncodeToPNG(), PhotonNetwork.LocalPlayer);
	}

	[PunRPC]
    public void EndPlay() {
		InGameMenu.SetActive(false);
		VotingPanel.SetActive(true);
		VotingPanel.GetComponent<VotingMenu>().ShowVotingCards();
	}

	[PunRPC]
    void SendData(byte[] receivedByte, Player Other)
    {
		print(Other.NickName);

		Texture2D NewTexture  = new Texture2D(1, 1);
        NewTexture.LoadImage(receivedByte);

		if (Screenshots.ContainsKey(Other))
		{
			Screenshots[Other] = NewTexture;
		}
		else
		{
			Screenshots.Add(Other, NewTexture);
		}
    }
}
