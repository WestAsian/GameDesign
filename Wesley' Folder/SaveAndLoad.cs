using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public Transform Player;

    public float Xp, Yp, Zp;

    public void SaveTransform()
    {
        Xp = Player.transform.position.x;
        Yp = Player.transform.position.y;
        Zp = Player.transform.position.z;

        PlayerPrefs.SetFloat("Xp", Xp);
        PlayerPrefs.SetFloat("Yp", Yp);
        PlayerPrefs.SetFloat("Zp", Zp);

    }

    public void LoadTransform()
    {
        Xp = PlayerPrefs.GetFloat("Xp");
        Yp = PlayerPrefs.GetFloat("Yp");
        Zp = PlayerPrefs.GetFloat("Zp");

        Vector3 LoadPosition = new Vector3(Xp, Yp, Zp);
        transform.position = LoadPosition;

    }
}
