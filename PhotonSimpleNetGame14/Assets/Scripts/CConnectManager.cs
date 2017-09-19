using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [포톤 네트워크 망 구조]
// 클라이언트 <-> 포톤 방 ..(여러개일수도).. <-> 포톤 로비(게임로비) <-> 포톤 클라우드

// [포톤 접속 순서]
// 1. 포톤 클라우드에 접속 (포톤 관리 서버)
// 2. 포톤 로비 생성 또는 접속 (포톤 앱(게임) 당 1개)
// 3. 포톤 방 생성 또는 접속 (포톤 앱(게임) 당 여러개)

public class CConnectManager : MonoBehaviour {

    private void Awake()
    { 
        // 1. 포톤 서버 접속

        // 포톤 서버에 접속한 상태가 아니라면
        if (!PhotonNetwork.connected)
        {
            // 포톤 서버 및 로비에 접속함
            // PhotonNetwork.ConnectUsingSettings("버전");// 버전은 자신의 서버 버전을 사용. 클라버전이 맞는 사람끼리 접속을 하기위함.
            PhotonNetwork.ConnectUsingSettings("v1.0");
        }
    }

    // 포톤 로비 접속 완료 이벤트 (주의 : 대소문자)
    public void OnJoinedLobby()
    {
        Debug.Log("Photon Cloud Lobby Connected...");
        Debug.Log("[알림] 포톤 클라우드 접속에 성공함 (로비 접속 완료)");

        // 방생성 API
        // PhotonNetwork.CreateRoom(...) : 포톤에 방을 생성함
        // * 생성한 사람은 자동 접속됨
        // PhotonNetwork.JoinRoom(...) : 포톤에 방을 접속함
        // PhotonNetwork.JoinRandomRoom(...) : 포톤에 랜덤하게 방에 접속함

        // 포톤에 방을 생성하되 이미 생성되어 있다면 방접속함
        // PhotonNetwork.JoinOrCreateRoom(방이름, 룸정보, 방타입)
        PhotonNetwork.JoinOrCreateRoom(
            "Room",
            new RoomOptions()
            {
                MaxPlayers = 10, // 최대 수용 인원
                IsOpen = true, // 공개 여부
                IsVisible = true // 활성 여부
            },
            TypedLobby.Default);
    }

    // 포톤 로비에 생성된 방에 접속을 완료함
    public void OnJoinedRoom()
    {
        Debug.Log("[알림] 게임방 접속을 완료함.");

        // 플레이어의 위치
        float posX = Random.Range(-3f, 3f);
        float posZ = Random.Range(0f, 3f);


        /*
        // 1. 일반적인 유니티 오브젝트 생성
        // 플레이어의 프리팹을 생성함
        Object playerPrefab = Resources.Load("MyPlayer"); // Resources 폴더 안에서 찾아 로드.

        // 플레이어 게임 오브젝트 생성
        Instantiate(playerPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);
        */

        // 포톤 네트워크 오브젝트를 생성함
        // * 모든 포톤 네트워크 오브젝트에는 PhotonView 컴포넌트가 추가 되어 있어야 함.
        // PhotonNetwork.Instantiate("Resources폴더의하위경로", 위치, 쿼터니언, 0);
        PhotonNetwork.Instantiate("Soldier", new Vector3(posX, 0, posZ), Quaternion.identity, 0);


    }


    // 포톤 클라우드 접속 오류
    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("[오류] 포톤 클라우드 접속을 실패함 : " + cause.ToString());
    }

    // 포톤 로비에 방 생성을 실패함
    public void OnPhotonCreateRoomFailed(object[] errorMsg)
    {
        Debug.Log("[오류] 방 생성을 실패함 : " + errorMsg[1].ToString());
    }

    // 포톤 방 접속에 실패함
    public void OnPhotonJoinRoomFailed(object[] errorMsg)
    {
        Debug.Log("[오류] 방 접속을 실패함 => " + errorMsg[1].ToString());
    }

    // 포톤 랜덤 방 접속에 실패함
    public void OnPhotonRandomJoinFailed(object[] errorMsg)
    {
        Debug.Log("[오류] 랜덤 방 접속을 실패함 => " + errorMsg[1].ToString());
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
