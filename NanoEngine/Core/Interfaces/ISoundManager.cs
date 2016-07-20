using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace NanoEngine.Core.Interfaces
{
    public interface ISoundManager
    {
        /// <summary>
        /// Plays a sound once that is passed in
        /// </summary>
        /// <param name="sound">The sound effect to be looped</param>
        void PlaySound(SoundEffect sound);

        /// <summary>
        /// Loads a sound into a dictonary so its ready to be looped
        /// </summary>
        /// <param name="sound">The sound effect to be added to the dictonary</param>
        /// <param name="name">The name you want to give to the sound effect</param>
        void LoadLoopedSound(SoundEffect sound, string name);

        /// <summary>
        /// Method to start one of the looped sounds
        /// </summary>
        /// <param name="name">Name of the looped sound</param>
        void StartLoopedSound(string name);

        /// <summary>
        /// Stops a sound from looping
        /// </summary>
        /// <param name="soundName">The name of the sound to stop looping</param>
        void StopLoopedSound(string soundName);
    }
}
