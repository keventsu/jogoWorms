using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class Net : MonoBehaviourPunCallbacks
{
    public GameObject loginGO;
    public GameObject partidaGO;
    public GameObject playGO;
    public InputField playerName;
    string playerNameTemp;
    public InputField nomeSala;
    public GameObject jogador;
    public Transform content;
    public GameObject roomlistitem;
    Dictionary<string, RoomInfo> roominfolist;
    public Transform[] spaws;
    public int characterID;
    

    // Start is called before the first frame update
    void Start()
    {
        
        roominfolist = new Dictionary<string, RoomInfo>();
        playerNameTemp = "Player" + Random.Range(1000,10000);
        playerName.text = playerNameTemp;

        loginGO.gameObject.SetActive(true);
        partidaGO.gameObject.SetActive(false);
        playGO.gameObject.SetActive(false);

        nomeSala.text = "Sala" + Random.Range(1000,10000);

    }
    public void Login()
    {
        if (playerName.text != "")
        {
            PhotonNetwork.NickName = playerName.text;

        }
        else
        {
            playerName.text = playerNameTemp;
        }
        PhotonNetwork.ConnectUsingSettings();
        
        loginGO.gameObject.SetActive(false);
        

    }

    



    public void BotaoCriarSala(){
        
        


        string nomeSalaTemp = nomeSala.text;
        RoomOptions roomOptions = new RoomOptions() {MaxPlayers = 8};
        
        PhotonNetwork.JoinOrCreateRoom(nomeSalaTemp, roomOptions, TypedLobby.Default);
    }
    public void BotaoSairSala(){
        if(!PhotonNetwork.InRoom){
            return;
        }
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomlist){
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
        foreach (RoomInfo info in roomlist)
        {
            if(!info.IsOpen || !info.IsVisible ||info.RemovedFromList){
            continue;
            }
            GameObject objRoomListItem = Instantiate(roomlistitem);
            objRoomListItem.transform.SetParent(content);
            objRoomListItem.transform.position = Vector3.one;
            objRoomListItem.GetComponent<Roomlistitem>().Iniciar(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
    public void BotaoPlay(){
        loginGO.gameObject.SetActive(false);
        partidaGO.gameObject.SetActive(false);
        playGO.gameObject.SetActive(false);

        int position = PhotonNetwork.LocalPlayer.ActorNumber;
        //PhotonNetwork.Instantiate(jogador.name, jogador.transform.position, jogador.transform.rotation, 0);
        PhotonNetwork.NickName = PhotonNetwork.NickName + PhotonNetwork.LocalPlayer.ActorNumber;
        PhotonNetwork.Instantiate(jogador.name, spaws[position -1].position, Quaternion.identity);
        
    }











// pun call back
    // Update is called once per frame
    void Update()
    {
        
    }
        public override void OnConnected()
    {
        Debug.Log("OnConnected");

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + " Ping: " + PhotonNetwork.GetPing());
        partidaGO.gameObject.SetActive(true);
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
       Debug.Log("OnConnectedToLobby");
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        loginGO.gameObject.SetActive(false);
        partidaGO.gameObject.SetActive(false);
        playGO.gameObject.SetActive(true);
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
        //PhotonNetwork.Instantiate(jogador.name, jogador.transform.position, jogador.transform.rotation, 0);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer){
        if(PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount){
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }


    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        if(!PhotonNetwork.CurrentRoom.IsOpen && !PhotonNetwork.CurrentRoom.IsVisible && PhotonNetwork.CurrentRoom.MaxPlayers > PhotonNetwork.CurrentRoom.PlayerCount){
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
        

    }
    public override void OnLeftRoom(){
        partidaGO.gameObject.SetActive(true);
        playGO.gameObject.SetActive(false);

    }



}
