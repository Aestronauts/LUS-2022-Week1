using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;

public class VotingMenu : MonoBehaviour
{
    public static int MyVotes = 1;
    public static VotingCard VotedCard;

    [SerializeField] GameObject CardGrid;
    [SerializeField] GameObject votingCard;

    List<VotingCard> VotingCards = new List<VotingCard>();

    public void ShowVotingCards()
    {
        foreach(KeyValuePair<Player, Texture2D> entry in GameManager.Instance.Screenshots)
        {
            GameObject go = Instantiate(votingCard, CardGrid.transform);
            VotingCard NewCard = go.GetComponent<VotingCard>();
            NewCard.image.texture = entry.Value;
        }
    }
}
