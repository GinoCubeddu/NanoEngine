using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Interfaces
{
    public interface ISoundManager : IService
    {
        /// <summary>
        /// Plays the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        /// <param name="loop">Informs the sound manager if it should be looped</param>
        void Play(string soundName, bool loop = false);

        /// <summary>
        /// Plays the requested SoundEffect
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        void PlayBaseSound(string soundName);

        /// <summary>
        /// Pauses the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        void Pause(string soundName);

        /// <summary>
        /// Stops the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName"></param>
        void Stop(string soundName);

        /// <summary>
        /// Loads the sound so its ready to play from the specified path
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="path">The path to the sound file</param>
        void LoadSound(string soundName, string path);

        /// <summary>
        /// Loads the passed SoundEffect so its ready to play 
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="soundEffect">The SoundEffect to load</param>
        void LoadSound(string soundName, SoundEffect soundEffect);

        /// <summary>
        /// Change the volume of all sounds
        /// </summary>
        /// <param name="amount">The amount to increae the sound by</param>
        void ChangeVolume(float amount);
    }
}
