using Microsoft.MixedReality.OpenXR.Remoting;
using Microsoft.MixedReality.OpenXR.Sample;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CustomAppRemoting : MonoBehaviour{

    [SerializeField] private Button connectButton = null;
    [SerializeField] private UnityEngine.UI.InputField textInput = null;

    [SerializeField, Tooltip("The configuration information for the remote connection.")]
    private RemotingConnectConfiguration remotingConnectConfiguration = new RemotingConnectConfiguration { RemotePort = 8265, MaxBitrateKbps = 20000 };
    private bool m_connected = false;
    private bool m_remotingInProgress = false;
    public void OnConnectButtonPressed() {
        if (m_remotingInProgress)
        {
            Debug.LogWarning("Current session is still in progress, try to connect again after completion");
            return;
        }

        if (textInput != null)
        {
            remotingConnectConfiguration.RemoteHostName = textInput.text;
        }

        if (string.IsNullOrWhiteSpace(remotingConnectConfiguration.RemoteHostName))
        {
            Debug.LogWarning($"No IP address was provided to {nameof(AppRemoting)}. Returning without connecting.");
            return;
        }

        m_connected = false;
        m_remotingInProgress = true;
        //m_appRemotingMode = AppRemotingMode.connect;

        //DisableButtons();
        Microsoft.MixedReality.OpenXR.Remoting.AppRemoting.StartConnectingToPlayer(remotingConnectConfiguration);
    }
}
