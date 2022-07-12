using UnityEngine;
using Photon.Pun;

public class LobsterVideoPlayer : MonoBehaviour {
    PhotonView view;

    void Awake() {
        view = PhotonView.Get(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K)) {
            view.RPC("PlayForAll", RpcTarget.All);
		    view.RPC("SetTimeForAll", RpcTarget.All, 0f);
		}
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
