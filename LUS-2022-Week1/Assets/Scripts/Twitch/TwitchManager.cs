using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchManager : MonoBehaviour
{
    public enum GameState
{
    Playing,
    GameOver
}
public GameState gameState; // 1
public ChatCommand currentCommand; // 2 

private TwitchChat chat; // 3
private List<ChatCommand> newCommands; // 4
private IGameCommand[] gameCommands; // 5

void Start()
{
    gameCommands = GetComponents<IGameCommand>();
  
    newCommands = new List<ChatCommand>();
    chat = gameObject.GetComponent<TwitchChat>(); 
  
    SetGameState(GameState.Playing); 
}

private void SetGameState(GameState newGameState)
{
    gameState = newGameState;
}
public void ResetGame()
{
    if (gameState != GameState.Playing)
    {
        SetGameState(GameState.Playing);
    }
}
public void EndGame()
{
    if (gameState != GameState.GameOver)
    {
        SetGameState(GameState.GameOver);
    }
}

private IGameCommand CommandIsValid(ChatMessage chatMessage) 
{
    if(chatMessage != null) 
    {
        print("one");
        string commandString = chatMessage.message.Split()[0]; // 1

        foreach (IGameCommand command in gameCommands) // 2
        {
            if (commandString == command.CommandString || commandString == command.ShortString) // 3
            {
                return command;
            }
        }
    }       
    return null; // 4
}

void FixedUpdate()
{
    if (gameState == GameState.Playing) //1
    {
        ChatMessage recentMessage = chat.ReadChat(); //2

        IGameCommand command = CommandIsValid(recentMessage); //3
        if (command != null) //4
        {
            print("new command");
            ChatCommand newCommand = new ChatCommand(recentMessage, command); //5
            newCommands.Add(newCommand);
        }

        ProcessCommands(); //6
                  
    }
}

private void ProcessCommands()
{
    if (newCommands.Count > 0)
    {
        newCommands[0].Execute(this); //7
        newCommands.RemoveAt(0);
    }
}

}
