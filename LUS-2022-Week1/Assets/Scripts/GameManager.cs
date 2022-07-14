﻿using Photon.Pun;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[SerializeField] RawImage image;

    PhotonView view;
	ScreenshotHelper screenshotHelper;

    void Awake() {
        view = PhotonView.Get(this);
		screenshotHelper = GetComponent<ScreenshotHelper>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			StartCoroutine(TakeScreenshot());
        }

		//view.RPC("PlayForAll", RpcTarget.All);
		//view.RPC("SetTimeForAll", RpcTarget.All, 0f);
    }

    public IEnumerator TakeScreenshot()
    {
        screenshotHelper.TakeScreenshotWithoutUI();

		while (!screenshotHelper.finished)
		{
			yield return null;
		}

		image.texture = screenshotHelper.screenshotTexture;
	}

    public void InitializeAndPlay() {
        if (PhotonNetwork.IsMasterClient) {
		    view.RPC("SetTimeForAll", RpcTarget.All, 0);
		    view.RPC("PlayForAll", RpcTarget.All);
		}
	}

	public void PlayVideo() {
		view.RPC("PlayForAll", RpcTarget.All);
	}

    public void PauseVideo() {
		view.RPC("PauseForAll", RpcTarget.All);
    }

	#region NETWORKING
    
	[PunRPC]
	void PlayForAll() {
        // do stuff
	}

	#endregion
}