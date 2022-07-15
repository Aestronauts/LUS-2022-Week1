using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

using Photon.Realtime;

public class VotingMenu : MonoBehaviour
{
    public static int MyVotes = 1;
    public static VotingCard VotedCard;

    [SerializeField] GameObject CardGrid;
    [SerializeField] GameObject votingCard;

    List<VotingCard> VotingCards = new List<VotingCard>();

    bool cardsDisplayed = false;

    int LastCount = 0;

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
            if (!GameManager.Instance.Running)
            {
                if (GameManager.Instance.Screenshots.Count != LastCount)
                {
                    ClearVotingCards();
                    ShowVotingCards();

                    LastCount = GameManager.Instance.Screenshots.Count;
                }
            }
    }

    public void ClearVotingCards() {
        foreach (Transform child in CardGrid.transform) {
            GameObject.Destroy(child.gameObject);
        }
        VotingCards.Clear();
    }

    public void ShowVotingCards()
    {
        foreach(KeyValuePair<Player, Texture2D> entry in GameManager.Instance.Screenshots)
        {
            GameObject go = Instantiate(votingCard, CardGrid.transform);
            VotingCard NewCard = go.GetComponent<VotingCard>();
            NewCard.image.texture = entry.Value;
        }
    }

    public void PlayAgain()
    {
		PhotonView photonView = PhotonView.Get(this);
		photonView.RPC("PlayForAll", RpcTarget.All);
    }

	[PunRPC]
	void PlayForAll() {
        SceneManager.LoadScene("MultiPlayerVersion");
	}
}
