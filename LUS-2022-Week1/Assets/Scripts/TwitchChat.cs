using UnityEngine;
using System.IO;
using System.Net.Sockets;

public class TwitchChat : MonoBehaviour
{
    public string username;
    public string password;
    public string channelName;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;
    private float reconnectTimer;
    private float reconnectAfter;

    void Start() {
        reconnectAfter = 60.0f;
        Connect();
    }

    void Update() {
        if (twitchClient.Available == 0)
        {
            reconnectTimer += Time.deltaTime; 
        }

        if (twitchClient.Available == 0 && reconnectTimer >= reconnectAfter)
        {
            Connect(); 
            reconnectTimer = 0.0f; 
        }

        ReadChat();
    }

    void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream()); 
        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 *:" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }

    public void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            string message = reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                print(message); 
            }
        }
    }
}
