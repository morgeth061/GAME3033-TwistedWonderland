using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
class SaveData
{
    public int weaponType;
    public int currentAmmo;
    public float currentHealth;
    public int currentWave;
    public float playerXPos;
    public float playerYPos;
    public float playerZPos;

    public SaveData()
    {
    }
}

public class SaveGameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject waveManager;
    public GameObject AK47Prefab;
    public GameObject ShotgunPrefab;

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ZombieSurvivalSaveData.dat");
        SaveData data = new SaveData();

        if (player.GetComponent<WeaponHolder>().weaponToSpawn.name == "AK47Prefab")
        {
            data.weaponType = 1;
            data.currentAmmo = player.GetComponent<WeaponHolder>().equippedWeapon.weaponStats.bulletsInClip;
        }
        else if (player.GetComponent<WeaponHolder>().weaponToSpawn.name == "ShotgunPrefab")
        {
            data.weaponType = 2;
            data.currentAmmo = player.GetComponent<WeaponHolder>().equippedWeapon.weaponStats.bulletsInClip;
        }
        else //No Weapon
        {
            data.weaponType = 0;
            data.currentAmmo = 0;
        }

        data.currentHealth = player.GetComponent<PlayerHealthComponent>().CurrentHealth;

        data.currentWave = waveManager.GetComponent<WaveManager>().currentWave;

        data.playerXPos = player.transform.position.x;
        data.playerYPos = player.transform.position.y;
        data.playerZPos = player.transform.position.z;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved! - Binary File");
    }

    public void LoadGame()
    {
        //.GetComponent<WeaponHolder>().weaponToSpawn
        if (File.Exists(Application.persistentDataPath + "/ZombieSurvivalSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ZombieSurvivalSaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            if (data.weaponType == 1)
            {
                player.GetComponent<WeaponHolder>().weaponToSpawn = AK47Prefab;
                player.GetComponent<WeaponHolder>().InstantiateWeapon();
                player.GetComponent<WeaponHolder>().equippedWeapon.weaponStats.bulletsInClip = data.currentAmmo;
            }
            else if (data.weaponType == 2)
            {
                player.GetComponent<WeaponHolder>().weaponToSpawn = ShotgunPrefab;
                player.GetComponent<WeaponHolder>().InstantiateWeapon();
                player.GetComponent<WeaponHolder>().equippedWeapon.weaponStats.bulletsInClip = data.currentAmmo;
            }
            else
            {
                player.GetComponent<WeaponHolder>().weaponToSpawn = null;
                player.GetComponent<WeaponHolder>().ClearWeapon();
            }

            foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
            {
                Destroy(zombie);
            }

            waveManager.GetComponent<WaveManager>().currentWave = data.currentWave;
            waveManager.GetComponent<WaveManager>().NewWave();

            player.transform.position = new Vector3(data.playerXPos, data.playerYPos, data.playerZPos);


            Debug.Log("Game data loaded! - Binary File");
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void ResetData()
    {

        if (File.Exists(Application.persistentDataPath + "/ZombieSurvivalSaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/ZombieSurvivalSaveData.dat");
            Debug.Log("Data reset complete! - Binary File");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}
