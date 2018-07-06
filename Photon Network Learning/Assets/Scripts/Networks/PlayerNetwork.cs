using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;

	void Awake () 
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
		PlayerName = "Dan#" + Random.Range (1000, 9999);

        // increase photon sendRate, smoother online functionality at the cost
        // of increased bandwidth use
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading; 
	}


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            if(PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    // you are the master client and you just loaded the game
    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    // you are not the master client and you just loaded the game
    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayerManagement.Instance.AddPlayerStats(photonPlayer);
    }

    //public void NewHealth(PhotonPlayer photonPlayer, int health)
    //{
    //    PhotonView.RPC("RPC_NewHealth", photonPlayer, health);
    //}

    //[PunRPC]
    //private void RPC_NewHealth(int health)
    //{
    //    if (CurrentPlayer == null)
    //        return;

    //    if (health <= 0)
    //        PhotonNetwork.Destroy(CurrentPlayer.gameObject);
    //    else
    //        CurrentPlayer.Health = health;
    //}

    //[PunRPC]
    //private void RPC_CreatePlayer()
    //{
    //    float randomValue = Random.Range(0f, 5f);
    //    GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NewPlayer"), Vector3.up * randomValue, Quaternion.identity, 0);
    //    CurrentPlayer = obj.GetComponent<PlayerMovement>();
    //}
}
