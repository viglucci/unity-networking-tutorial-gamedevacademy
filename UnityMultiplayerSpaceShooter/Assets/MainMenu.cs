using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button JoinServerButton;
    public Button HostServerButton;
    
    NetworkManager networkManager;

    void Awake()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    private void Start()
    {
        HostServerButton.onClick.AddListener(() =>
        {
            networkManager.StartHost();
        });
    }
}
