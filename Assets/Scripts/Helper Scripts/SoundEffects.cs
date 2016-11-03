using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SoundEffects : MonoBehaviour {

    public Dictionary<string, AudioClip> sfx;
    public string test;
    public List<string> names,prevNames;
    public List<AudioClip> sounds,prevSounds;
    public bool Updated;
    
    public void PlaySound(string Sound, float volume = 1)
    {

        if (sfx == null) { sfx = new Dictionary<string, AudioClip>();
            sfx.FromLists(names, sounds);
        }
       // Debug.Log(sfx["Jump"]);
        try {
            AudioClip s = sfx[Sound];
            Music.PlaySound(s, volume);
        }
        catch (Exception e)
        {
            Debug.Log("Sound Effect "+Sound+" not defined.");
        }
        
    }

}
