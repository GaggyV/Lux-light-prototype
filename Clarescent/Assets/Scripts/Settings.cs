using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Settings : MonoBehaviour
{
    public Controller controller;
    public int musicVolume;
    public int soundEffectsVolume;
    [SerializeField] TextAsset saveFile;

    private Controller controllerLagger;
    private int musicVolumeLagger;
    private int soundEffectsVolumeLagger;
    StreamWriter writer;
    StreamReader reader;
    void Start()
    {
        reader = new StreamReader("Assets/saveFile.txt");
        string controllerString = reader.ReadLine();
        if (controllerString == "Keyboard")
            controller = Controller.Keyboard;
        else if (controllerString == "Controller")
            controller = Controller.Controller;
        else 
            controller = Controller.Unique;
        musicVolume = musicVolumeLagger = int.Parse(reader.ReadLine());
        soundEffectsVolume = soundEffectsVolumeLagger = int.Parse(reader.ReadLine());
        reader.Close();
    }

    
    void Update()
    {
        if (controller != controllerLagger)
        {
            controllerLagger = controller;
            SaveSettings();
        }
        if (musicVolume != musicVolumeLagger)
        {
            musicVolumeLagger = musicVolume;
            SaveSettings();
        }
        if (soundEffectsVolume != soundEffectsVolumeLagger)
        {
            soundEffectsVolumeLagger = soundEffectsVolume;
            SaveSettings();
        }
    }

    void SaveSettings()
    {
        writer = new StreamWriter("Assets/saveFile.txt");
        switch (controller)
        {
            case Controller.Keyboard:
                writer.WriteLine("Keyboard");
                break;
            case Controller.Controller:
                writer.WriteLine("Controller");
                break;
            case Controller.Unique:
                writer.WriteLine("Unique");
                break;
        }
        writer.WriteLine(musicVolume);
        writer.WriteLine(soundEffectsVolume);
        writer.Close();
    }
}
