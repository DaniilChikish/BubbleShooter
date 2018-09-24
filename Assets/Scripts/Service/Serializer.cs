using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class Serializer {
    public static bool Save(GameSpecs specs)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    return SaveSpecsAndroid(specs);
                }
            default:
                {
                    return SaveSpecsDefault(specs);
                }
        }
    }
    private static bool SaveSpecsAndroid(GameSpecs specs)
    {
        string path = Application.persistentDataPath + "/specs.dat";
        try
        {
            XmlSerializer formatter = new XmlSerializer(typeof(GameSpecs));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, specs);
            }
            Debug.Log(path + " - saved(for Andriod)");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
    private static bool SaveSpecsDefault(GameSpecs specs)
    {
        string path = Application.streamingAssetsPath + "/specs.dat";
        try
        {
            XmlSerializer formatter = new XmlSerializer(typeof(GameSpecs));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, specs);
            }
            Debug.Log(path + " - saved(default)");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
    public static bool Save(UserProfile userData)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    return SaveUserAndroid(userData);
                }
            default:
                {
                    return SaveUserDefault(userData);
                }
        }
    }
    private static bool SaveUserAndroid(UserProfile user)
    {
        string path = Application.persistentDataPath + "/user.dat";
        try
        {
            XmlSerializer formatter = new XmlSerializer(typeof(UserProfile));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, user);
            }
            Debug.Log(path + " - saved(for Android)");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
    private static bool SaveUserDefault(UserProfile specs)
    {
        string path = Application.streamingAssetsPath + "/user.dat";
        try
        {
            XmlSerializer formatter = new XmlSerializer(typeof(UserProfile));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, specs);
            }
            Debug.Log(path + " - saved(default)");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
    public static bool Load(out GameSpecs specs)
    {
        Debug.Log("Try to load settings.");
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    return LoadSpecsAndroid(out specs);
                }
            default:
                {
                    return LoadSettingsDefault(out specs);
                }
        }
    }
    private static bool LoadSpecsAndroid(out GameSpecs specs)
    {
        string path = Application.persistentDataPath + "/specs.dat";
        // передаем в конструктор тип класса
        XmlSerializer formatter = new XmlSerializer(typeof(GameSpecs));
        specs = new GameSpecs();
        // десериализация
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            specs = (GameSpecs)formatter.Deserialize(fs);
            Debug.Log(path + " - ok");
            fs.Close();
            Debug.Log("Settings loaded.");
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Set default");
            return false;
        }
    }
    private static bool LoadSettingsDefault(out GameSpecs specs)
    {
        string path = Application.streamingAssetsPath + "/specs.dat";
        // передаем в конструктор тип класса
        XmlSerializer formatter = new XmlSerializer(typeof(GameSpecs));
        specs = new GameSpecs();
        // десериализация
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            specs = (GameSpecs)formatter.Deserialize(fs);
            Debug.Log(path + " - ok");
            fs.Close();
            Debug.Log("Settings loaded.");
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Set default");
            return false;
        }
    }
    public static bool Load(out UserProfile userData)
    {
        Debug.Log("Try to load settings.");
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    return LoadUserAndroid(out userData);
                }
            default:
                {
                    return LoadUserDefault(out userData);
                }
        }
    }
    private static bool LoadUserAndroid(out UserProfile specs)
    {
        string path = Application.persistentDataPath + "/user.dat";
        // передаем в конструктор тип класса
        XmlSerializer formatter = new XmlSerializer(typeof(UserProfile));
        specs = new UserProfile();
        // десериализация
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            specs = (UserProfile)formatter.Deserialize(fs);
            Debug.Log(path + " - ok");
            fs.Close();
            Debug.Log("Settings loaded.");
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Set default");
            return false;
        }
    }
    private static bool LoadUserDefault(out UserProfile specs)
    {
        string path = Application.streamingAssetsPath + "/user.dat";
        // передаем в конструктор тип класса
        XmlSerializer formatter = new XmlSerializer(typeof(UserProfile));
        specs = new UserProfile();
        // десериализация
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            specs = (UserProfile)formatter.Deserialize(fs);
            Debug.Log(path + " - ok");
            fs.Close();
            Debug.Log("Settings loaded.");
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Set default");
            return false;
        }
    }
}
[Serializable]
public class GameSpecs
{
    public readonly float easyFactor = 1f;
    public readonly float mediunFactor = 1.5f;
    public readonly float hardFactor = 2f;
    public readonly float scoreFactor = 2f;
    public readonly float timeFactor = 0.005f;
    public GameSpecs()
    {
    }
    public void SetDafault()
    {
    }
}

[Serializable]
public class UserProfile
{
    public float prevSessionScore = 0f;
    public float recordScore = 0;
    public UserProfile()
    {
    }
    public void SetDafault()
    {
    }
}