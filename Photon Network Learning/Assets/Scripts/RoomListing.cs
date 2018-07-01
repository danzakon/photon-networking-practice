using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {

	[SerializeField]
	private Text _roomNameText;
	private Text RoomNameText
	{
		get { return _roomNameText; }
	}

	public bool Updated { get; set; }

	void Start () {
		GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;

		if (lobbyCanvasObj == null) {
			return;
		}

		LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas> ();

		Button button = GetComponent<Button> ();

		button.onClick.AddListener (() => lobbyCanvas.OnClickJoinRoom (RoomNameText.text));
	}

	private void OnDestroy()
	{
		Button button = GetComponent<Button> ();
		button.onClick.RemoveAllListeners ();
	}

	public void SetRoomNameText(string text)
	{
		RoomNameText.text = text;
	}

}
