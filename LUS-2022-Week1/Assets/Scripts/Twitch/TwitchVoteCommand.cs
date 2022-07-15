using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchVoteCommand : MonoBehaviour, IGameCommand
{
    public string CommandString => "!vote";
    public string ShortString => "!v";

    public bool Execute(string username, List<string> arguments, TwitchManager gm = null)
{
    print(username + " sent the !vote command");
    return true;
}

}
