using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class GooglePlayConnect : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {       
        PlayGamesPlatform.Activate();
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {           
            Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("Authenticate success! Token is " + ((PlayGamesLocalUser)Social.localUser).GetIdToken());
                    }
                    else
                    {
                        Debug.Log("Failed!");   
                    }
                });   
        }
        else
        {
            Debug.Log("Logged!");  
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
