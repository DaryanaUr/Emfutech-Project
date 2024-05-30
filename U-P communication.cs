using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketServer : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;

    void Start()
    {
        serverThread = new Thread(new ThreadStart(StartServer));
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    void StartServer()
    {
        server = new TcpListener(IPAddress.Any, 12345);
        server.Start();
        Debug.Log("Server started on port 12345");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Debug.Log("Received: " + data);

            if (data == "scene1")
            {
                LoadScene("Scene1");
            }
            else if (data == "scene2")
            {
                LoadScene("Scene2");
            }

            client.Close();
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void OnApplicationQuit()
    {
        server.Stop();
    }
}