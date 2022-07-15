using UnityEngine;
using System.IO;
using System.Net.Sockets;

// https://www.raywenderlich.com/24373608-how-to-create-a-twitch-chat-game-with-unity

public class TwitchChat : MonoBehaviour
{
    public string username;
    public string password;
    public string channelName;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;
    private float reconnectTimer;
    private float reconnectAfter = 3.0f;

    [SerializeField] bool Connected = false;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Connect();
    }

    void Update() {
        if (twitchClient.Available == 0)
        {
            reconnectTimer += Time.deltaTime; 
            Connected = false;
        }
        else 
        {
            Connected = true;

        }

        if (twitchClient.Available == 0 && reconnectTimer >= reconnectAfter)
        {
            Connect(); 
            reconnectTimer = 0.0f; 
        }

        ReadChat();
    }

    public void Connect()
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

    public ChatMessage ReadChat() // 1
    {
        if (twitchClient.Available > 0)
        {
            string message = reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                // Get the username
                int splitPoint = message.IndexOf("!", 1); // 2
                string chatName = message.Substring(0, splitPoint); 
                chatName = chatName.Substring(1);

                //Get the message
                splitPoint = message.IndexOf(":", 1); 
                message = message.Substring(splitPoint + 1);
                ChatMessage chatMessage = new ChatMessage(); // 3
                chatMessage.user = chatName;
                chatMessage.message = message.ToLower();
                return chatMessage;
            }

        }
        return null; // 4
    }
}
