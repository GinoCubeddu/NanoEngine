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
        private ISoundManager manager;

        private ISoundManager Manager
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
            }
        }
    }
}
