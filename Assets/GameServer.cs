#define USE_GS_AUTH_API
#define DISABLED
using UnityEngine;
using Steamworks;
using System;

public class GameServer : MonoBehaviour {
    const string SNAKES_SERVER_VERSION = "1.0.0.0";
    const ushort AUTHENTICATION_PORT = 8766;
    const ushort SERVER_PORT = 27015;
    const ushort MASTER_SERVER_UPDATER_PORT = 27016;
    protected Callback<SteamServersConnected_t> SteamServersConnected;
#if DISABLED
    protected Callback<SteamServerConnectFailure_t> SteamServersConnectedFailure;
    protected Callback<SteamServersDisconnected_t> SteamServerDisconnected;
    protected Callback<GSPolicyResponse_t> CallbackPolicyResponse;
    protected Callback<ValidateAuthTicketResponse_t> CallbackGSAuthTicketResponse;
    protected Callback<P2PSessionRequest_t> CallbackP2PSessionRequest;
    protected Callback<P2PSessionConnectFail_t> CallbackP2PSessionConnectFail;
#endif
    bool Initialized = false;
    bool _connectedToSteam = false;

    // Use this for initialization
    void Start () {
        SteamServersConnected = Callback<SteamServersConnected_t>.CreateGameServer(OnSteamServersConnected);
#if DISABLED
        SteamServersConnectedFailure = Callback<SteamServerConnectFailure_t>.CreateGameServer(OnSteamServersConnectFailure);
        SteamServerDisconnected = Callback<SteamServersDisconnected_t>.CreateGameServer(OnSteamServersDisconnected);
        CallbackPolicyResponse = Callback<GSPolicyResponse_t>.CreateGameServer(OnPolicyResponse);
        CallbackGSAuthTicketResponse = Callback<ValidateAuthTicketResponse_t>.CreateGameServer(OnValidateAuthTicketResponse);
        CallbackP2PSessionRequest = Callback<P2PSessionRequest_t>.CreateGameServer(OnP2PSessionRequest);
        CallbackP2PSessionConnectFail = Callback<P2PSessionConnectFail_t>.CreateGameServer(OnP2PSessionConnectFail);
#endif
        Initialized = false;
        _connectedToSteam = false;
#if USE_GS_AUTH_API
        EServerMode eMode = EServerMode.eServerModeAuthenticationAndSecure;
#endif
        uint unFlags = 27016;
        AppId_t nGameAppId = new AppId_t();
        Initialized = SteamGameServer.InitGameServer(0, AUTHENTICATION_PORT, SERVER_PORT, unFlags,nGameAppId, SNAKES_SERVER_VERSION);
        if(Initialized==false)
        {
            return;
        }
        SteamGameServer.SetModDir("snakes");
        SteamGameServer.SetProduct("Steamworks Example");
        SteamGameServer.SetGameDescription("Steamworks Example");
        SteamGameServer.LogOnAnonymous();
        SteamGameServer.EnableHeartbeats(true);
        print("Started");
    }

    // Update is called once per frame
    void Update () {
        if(Initialized==false)
        {
            return;
        }
        if (_connectedToSteam)
        {
            SendUpdatedServerSteam();
        }
	}
    private void OnSteamServersConnected(SteamServersConnected_t LogonSuccess)
    {
        _connectedToSteam = true;
        SteamGameServer.SetMaxPlayerCount(4);
        SteamGameServer.SetPasswordProtected(false);
        SteamGameServer.SetServerName("Snakes");
        SteamGameServer.SetBotPlayerCount(0);
        SteamGameServer.SetMapName("inGame");
    }
    void OnSteamServersConnectFailure(SteamServerConnectFailure_t connectedFailured)
    {
        _connectedToSteam = false;
    }
    void OnSteamServersDisconnected(SteamServersDisconnected_t serverDisconnected)
    {
        _connectedToSteam = false;
    }
    void OnPolicyResponse(GSPolicyResponse_t PolicyResponses)
    {
        if (SteamGameServer.BSecure())
        {
            print("VAC is Secure");
        }
        else
        {
            print("not VAC is Secure");
        }
        print("SteamID:" + SteamGameServer.GetSteamID().ToString());
    }
    void OnValidateAuthTicketResponse(ValidateAuthTicketResponse_t pResponse)
    {
        print(pResponse.m_SteamID);
        if(pResponse.m_eAuthSessionResponse==EAuthSessionResponse.k_EAuthSessionResponseOK)
        {

        }
        else
        {

        }
    }
    void OnP2PSessionRequest(P2PSessionRequest_t pCallback)
    {
        SteamGameServerNetworking.AcceptP2PSessionWithUser(pCallback.m_steamIDRemote);
    }
    void OnP2PSessionConnectFail(P2PSessionConnectFail_t Callbackfailed)
    {
        print("OnP2PSessionConnectFail:" + Callbackfailed.m_steamIDRemote);
    }
    void SendUpdatedServerSteam()
    {
        SteamGameServer.SetMaxPlayerCount(4);
        SteamGameServer.SetPasswordProtected(false);
        SteamGameServer.SetServerName("Snakes");
        SteamGameServer.SetBotPlayerCount(0);
        SteamGameServer.SetMapName("inGame");
#if USE_GS_AUTH_API
        for (uint i = 0; i < 4; ++i)
        {

        }
#endif
    }
}
