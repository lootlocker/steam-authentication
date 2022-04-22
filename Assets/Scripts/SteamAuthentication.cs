using UnityEngine;
using TMPro;
using Steamworks;
using LootLocker.Requests;

public class SteamAuthentication : MonoBehaviour
{
    // Text component to show user name
    public TextMeshProUGUI steamUserName;

    // Animator for buttons
    public Animator buttonAnimator;

    public void Login()
    {
        // Start to play loading animation
        buttonAnimator.SetTrigger("Login");

        // Check that Steam is initialized
        if (!SteamManager.Initialized)
        {
            // Steam not initialized, play error animation
            buttonAnimator.SetBool("Success", false);
            return;
        }

        // Create a steam authentication session ticket
        byte[] ticket = new byte[1024];
        var newTicket = SteamUser.GetAuthSessionTicket(ticket, 1024, out uint ticketSize);

        // Convert it so LootLocker can read it
        string steamSessionTicket = LootLockerSDKManager.SteamSessionTicket(ref ticket, ticketSize);

        // Verify the steam session ticket
        LootLockerSDKManager.VerifySteamID(steamSessionTicket, (response) =>
        {
            if (!response.success)
            {
                // Error
                Debug.Log("Error verifying steam ID:" + response.Error);
                buttonAnimator.SetBool("Success", false);
                return;
            }
            else if (response.success)
            {
                Debug.Log("Successfully verified steam user");

                // Get the steam ID
                CSteamID steamID = SteamUser.GetSteamID();

                LootLockerSDKManager.StartSteamSession(steamID.ToString(), (response) =>
                {
                    if (!response.success)
                    {
                        // Error
                        Debug.Log("Error starting session:" + response.Error);
                        buttonAnimator.SetBool("Success", false);
                        return;
                    }
                    else if (response.success)
                    {
                        Debug.Log("Session started!");
                        buttonAnimator.SetBool("Success", true);

                        // Use steam ID to get Steam account name
                        string steamName = SteamFriends.GetFriendPersonaName(steamID);

                        // Show steam name on screen
                        steamUserName.text = steamName;
                    }

                });
            }
        });
    }
}
