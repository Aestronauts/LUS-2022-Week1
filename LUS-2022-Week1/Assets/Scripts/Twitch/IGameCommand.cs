using System.Collections.Generic;
public interface IGameCommand
{
    string CommandString { get; } 
    string ShortString { get; }  

    bool Execute(string username, List<string> arguments, TwitchManager gm = null);

}
