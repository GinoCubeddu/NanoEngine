using Microsoft.Xna.Framework.Audio;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Managers
{
    public class SoundManager : ISoundManager
    {
        //Private soundmanagaer instance
        public static ISoundManager manager;

        //Private float to hold the sound of the looped song
        private float loopedSoundVoloume = 0.2f;

        //private list to hold all currently playing songs
        private IList<SoundEffectInstance> currentlyPlaying;

        public static ISoundManager Manager
        {
            get { return manager ?? (manager = new SoundManager()); }
        }

        //Dictonary for holding looped sounds
        private Dictionary<string, SoundEffectInstance> loopedSounds;

        /// <summary>
        /// Main constructor for the sound manager
        /// </summary>
        private SoundManager()
        {
            loopedSounds = new Dictionary<string, SoundEffectInstance>();
            currentlyPlaying = new List<SoundEffectInstance>();
        }

        /// <summary>
        /// Plays a sound once that is passed in
        /// </summary>
        /// <param name="sound">The sound effect to be looped</param>
        public void PlaySound(SoundEffect sound)
        {
            sound.Play();
        }

        /// <summary>
        /// Loads a sound into a dictonary so its ready to be looped
        /// </summary>
        /// <param name="sound">The sound effect to be added to the dictonary</param>
        /// <param name="name">The name you want to give to the sound effect</param>
        public void LoadLoopedSound(SoundEffect sound, string name)
        {
            //If the dictonary does not allready have an object by that name
            if (!loopedSounds.ContainsKey(name))
            {
                //Convert sound effect into instance
                SoundEffectInstance sound1 = sound.CreateInstance();
                //Set IsLooped to true
                sound1.IsLooped = true;
                //Add the sound to the dictonary
                loopedSounds.Add(name, sound1);
            }
            else
            {
                Console.WriteLine("SOUND WITH THE NAME " + name + " ALLREADY EXSISTS");
            }
        }

        /// <summary>
        /// Method to start one of the looped sounds
        /// </summary>
        /// <param name="name">Name of the looped sound</param>
        public void StartLoopedSound(string name)
        {
            //if the looped sound name exsists
            if(loopedSounds.ContainsKey(name))
            {
                //Add to currently playing list
                currentlyPlaying.Add(loopedSounds[name]);
                //Set volume
                loopedSounds[name].Volume = loopedSoundVoloume;
                //Play the sound
                loopedSounds[name].Play();
            }
            else
            {
                Console.WriteLine("SOUND NOT PLAYING: NO LOOPED SOUND CALLED " + name);
            }
        }

        /// <summary>
        /// Stops a sound from looping
        /// </summary>
        /// <param name="soundName">The name of the sound to stop looping</param>
        public void StopLoopedSound(string soundName)
        {
            //If the name of the looped sound exsists
            if(loopedSounds.ContainsKey(soundName))
            {
                //Stop the sound
               loopedSounds[soundName].Stop(true);
               //Remove to currently playing list
               currentlyPlaying.Add(loopedSounds[soundName]);
            }
        }

        /// <summary>
        /// Method that allows the increase or decrease of the voloume
        /// </summary>
        /// <param name="amount">The amount to increase or decrease by</param>
        public void ChangeLoopedSoundVolume(float amount)
        {
            //If the amount does not exceed the bounds
            if(!(loopedSoundVoloume + amount < 0) && !(loopedSoundVoloume + amount > 1))
            {
                //Updaate the volume
                loopedSoundVoloume += amount;
                //set volume of all current playing songs
                foreach(SoundEffectInstance sound in currentlyPlaying)
                {
                    sound.Volume = loopedSoundVoloume;
                }
            }
        }
    }
}
