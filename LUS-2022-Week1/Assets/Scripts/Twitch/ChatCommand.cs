using System.Collections.Generic;
using System;

[System.Serializable]
public class ChatCommand
{
    public IGameCommand command; // 1
    public string username; // 2 
    public DateTime timestamp; // 3 
    public List<string> arguments; // 4 

    public ChatCommand(ChatMessage message, IGameCommand submittedCommand) // 1
    {
        arguments = new List<string>(); // 2
        command = submittedCommand; // 3
        username = message.user; 
        timestamp = DateTime.Now; 
        ParseCommandArguments(message); // 4
    }

    public void ParseCommandArguments(ChatMessage message)
    {
        string[] splitMessage = message.message.Split();
    
        if (splitMessage.Length > 1)
        {
            for (int i = 1; i < splitMessage.Length; i++)
            {
                arguments.Add(splitMessage[i]);
            }
        }
    }

    public bool Execute(TwitchManager gm)
{
    return command.Execute(username, arguments, gm);
}


}
