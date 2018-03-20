using Microsoft.Xna.Framework.Audio;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Managers
{
    public class SoundManager : ISoundManager
    {
        // Dict to hold the currently playing sounds
        private IDictionary<string, SoundEffectInstance> _availableSounds;

        // Dict to hold sounds that are avaliable
        private float _currentVolume;

        /// <summary>
        /// Main constructor for the sound manager
        /// </summary>
        public SoundManager()
        {
            _availableSounds = new Dictionary<string, SoundEffectInstance>();
            _currentVolume = 0.5f;
        }

        /// <summary>
        /// Plays the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        /// <param name="loop">Informs the sound manager if it should be looped</param>
        public void Play(string soundName, bool loop = false)
        {
            // If the sound does not exsist throw an error
            if (!_availableSounds.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: song with the id of " + soundName +  " not found! Have you loaded it?"
                );
            _availableSounds[soundName].IsLooped = loop;
            _availableSounds[soundName].Play();
        }

        /// <summary>
        /// Pauses the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        public void Pause(string soundName)
        {
            // If the sound does not exsist throw an error
            if (!_availableSounds.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: song with the id of " + soundName + " not found! Have you loaded it?"
                );
            _availableSounds[soundName].Pause();
        }

        /// <summary>
        /// Stops the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName"></param>
        public void Stop(string soundName)
        {
            // If the sound does not exsist throw an error
            if (!_availableSounds.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: song with the id of " + soundName + " not found! Have you loaded it?"
                );
            _availableSounds[soundName].Stop();
        }

        /// <summary>
        /// Loads the sound so its ready to play from the specified path
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="path">The path to the sound file</param>
        public void LoadSound(string soundName, string path)
        {
            // If the sound does exsist log a warning
            if (_availableSounds.ContainsKey(soundName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + soundName);

            // load sound from the content manager
            _availableSounds[soundName] = ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<SoundEffect>(path).CreateInstance();
        }

        /// <summary>
        /// Loads the passed SoundEffect so its ready to play 
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="soundEffect">The SoundEffect to load</param>
        public void LoadSound(string soundName, SoundEffect soundEffect)
        {
            // If the sound does exsist log a warning
            if (_availableSounds.ContainsKey(soundName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + soundName);

            // Load the sound
            _availableSounds[soundName] = soundEffect.CreateInstance();
        }

        /// <summary>
        /// Change the volume of all sounds
        /// </summary>
        /// <param name="amount">The amount to increae the sound by</param>
        public void ChangeVolume(float amount)
        {
            // Increase the sound
            _currentVolume += amount;

            // Make sure the sound does not go above the max/below the min
            if (_currentVolume < 0)
                _currentVolume = 0;
            if (_currentVolume > 1)
                _currentVolume = 1;

            // loop through each sound an change the volume
            foreach (SoundEffectInstance sound in _availableSounds.Values)
                sound.Volume = _currentVolume;
        }
    }
}
