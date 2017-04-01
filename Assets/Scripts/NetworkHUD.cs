using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkHUD : MonoBehaviour {

    private GameObject startPanel;
    private GameObject runningPanel;
    private bool isHost;
    public NetworkManager networkManager;

	void Start () {
        startPanel = this.gameObject.transform.GetChild(0).gameObject;
        runningPanel = this.gameObject.transform.GetChild(1).gameObject;

        runningPanel.SetActive(false);
    }

    public void StartHost()
    {
        networkManager.networkPort = 7777;
        networkManager.StartHost();
        isHost = true;

        runningPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void JoinGame()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        if (ipAddress == "*") ipAddress = "localhost";
        networkManager.networkAddress = ipAddress;
        networkManager.networkPort = 7777;
        networkManager.StartClient();
        isHost = false;

        runningPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void Disconnect()
    {
        if (isHost) networkManager.StopHost();
        else networkManager.StopClient();

        runningPanel.SetActive(false);
        startPanel.SetActive(true);
    }

}
