using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    void Start()
    {
        // Check if saved position exists and set the player position
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);
        }
    }
}

