using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

using TMPro;

public class VotingCard : MonoBehaviour
{
    int votes = 0;

    [SerializeField] TextMeshProUGUI VoteLabel;
    PhotonView PV;

    public RawImage image;

    private void Awake() {
        PV = GetComponent<PhotonView>();

        image = GetComponent<RawImage>();
    }

    public void Vote()
    {
        if (PV.IsMine)
        {
            if (VotingMenu.MyVotes == 1)
            {
                AddVote();
            }
            else
            {
                if (VotingMenu.VotedCard.gameObject == gameObject)
                {
                    RemoveVote();
                }
                else
                {
                    VotingMenu.VotedCard.RemoveVote();
                    AddVote();
                }
            }
        }
    }

    public void AddVote() {
		PV.RPC("VoteIncrement", RpcTarget.All);
    }

    public void RemoveVote() {
		PV.RPC("VoteDecrement", RpcTarget.All);
    }

	[PunRPC]
    void VoteIncrement()
    {
        VoteLabel.text = "" + ++votes;
    }

	[PunRPC]
    void VoteDecrement()
    {
        VoteLabel.text = "" + --votes;
    }
}