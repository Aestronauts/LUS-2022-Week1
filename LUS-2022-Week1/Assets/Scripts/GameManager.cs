using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[SerializeField] GameObject InGameMenu;
	[SerializeField] GameObject VotingMenu;
	[SerializeField] float GameTimer = 5f;

	bool Running = true;

	Texture2D MyScreenshotTexture;
	Texture2D ReceivedScreenshotTexture;

	Dictionary<Player, Texture2D> Screenshots;

    PhotonView view;
	ScreenshotHelper screenshotHelper;

    void Awake() {
        view = PhotonView.Get(this);
		screenshotHelper = GetComponent<ScreenshotHelper>();

		Running = true;
		VotingMenu.SetActive(false);
		InGameMenu.SetActive(true);
    }

    void Update() {
		GameTimer -= Time.deltaTime;

		if (GameTimer < 0f && Running)
		{
			Running = false;

			if (view.IsMine)
            {
				StartCoroutine(TakeScreenshot());
            }

			if (PhotonNetwork.IsMasterClient)
			{
				view.RPC("EndPlay", RpcTarget.All);
			}
		}

        //if (Input.GetKeyDown(KeyCode.Space)) { }
    }

    public IEnumerator TakeScreenshot()
    {
        screenshotHelper.TakeScreenshotWithoutUI();

		while (!screenshotHelper.finished)
		{
			yield return null;
		}

		MyScreenshotTexture = screenshotHelper.screenshotTexture;
        view.RPC("SendData", RpcTarget.AllBuffered, MyScreenshotTexture.EncodeToPNG(), PhotonNetwork.LocalPlayer);
	}

    public void EndPlay() {
		InGameMenu.SetActive(false);
		VotingMenu.SetActive(true);
	}

	[PunRPC]
    void SendData(byte[] receivedByte, Player Other)
    {
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
