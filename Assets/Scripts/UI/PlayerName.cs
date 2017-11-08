using UnityEngine;
using UnityEngine.UI;


using System.Collections;


    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerName : MonoBehaviour
    {

        // Store the PlayerPref Key to avoid typos
        static string playerNamePrefKey = "PlayerName";

    //buttons
    public Button join;
    public Button create;

    //player name
    public string playername;
        void Start()
        {

            //set default
            string defaultName = "";
            InputField _inputField = this.GetComponent<InputField>();

            //get preferred name (remember from previous enter)
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            PhotonNetwork.playerName = defaultName;
        }

        //setter for player name
        public void SetPlayerName()
        {
            playername = GetComponent<InputField>().text;
        //PhotonNetwork.playerName = x + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated.
        PlayerNetwork.instance.pname = playername;
            PlayerPrefs.SetString(playerNamePrefKey, playername);
            //Debug.Log("pname set");
        PhotonNetwork.ConnectUsingSettings("1");
    }

        //enable buttons once name has been entered
        public void enableButtons()
    {
        join.interactable = true;
        create.interactable = true;
    }
        
    }

