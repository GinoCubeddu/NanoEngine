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
        private IDictionary<string, SoundEffectInstance> _availableSoundEffectInstances;

        private IDictionary<string, SoundEffect> _avaliableSoundEffects;

        private IDictionary<string, Song> _availableSongs;

        // Dict to hold sounds that are avaliable
        private float _currentVolume;

        /// <summary>
        /// Main constructor for the sound manager
        /// </summary>
        public SoundManager()
        {
            _availableSoundEffectInstances = new Dictionary<string, SoundEffectInstance>();
            _avaliableSoundEffects = new Dictionary<string, SoundEffect>();
            _availableSongs = new Dictionary<string, Song>();
            _currentVolume = 0.5f;
            SoundEffect.MasterVolume = _currentVolume;
        }

        /// <summary>
        /// Plays the requested Song
        /// </summary>
        /// <param name="songName">The name of the song</param>
        /// <param name="loop">Informs the sound manager if it should be looped</param>
        public void PlaySong(string songName, bool loop = false)
        {
            // If the sound does not exsist throw an error
            if (!_availableSongs.ContainsKey(songName))
                throw new KeyNotFoundException(
                    "ERROR: sound with the id of " + songName + " not found! Have you loaded it?"
                );
            MediaPlayer.Play(_availableSongs[songName]);
            MediaPlayer.IsRepeating = loop;
        }

        /// <summary>
        /// Informs the sound manager to stop playing the current song
        /// </summary>
        public void StopPlayingSong()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Informs the sound manager to pause the current song
        /// </summary>
        public void PausePlayingSong()
        {
            MediaPlayer.Pause();
        }

        /// <summary>
        /// Loads the sound so its ready to play from the specified path
        /// </summary>
        /// <param name="songName">The id you want to give the sound</param>
        /// <param name="path">The path to the sound file</param>
        public void LoadSong(string songName, string path)
        {
            // If the sound does exsist log a warning
            if (_availableSongs.ContainsKey(songName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + songName);

            // load sound from the content manager
            _availableSongs[songName] = ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Song>(path);
        }

        /// <summary>
        /// Loads the passed SoundEffect so its ready to play 
        /// </summary>
        /// <param name="songName">The id you want to give the sound</param>
        /// <param name="soundEffect">The SoundEffect to load</param>
        public void LoadSong(string songName, Song soundEffect)
        {
            // If the sound does exsist log a warning
            if (_availableSongs.ContainsKey(songName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + songName);

            // Store the base sound effect
            _availableSongs[songName] = soundEffect;
        }

        /// <summary>
        /// Plays the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        /// <param name="loop">Informs the sound manager if it should be looped</param>
        public void PlaySoundEffect(string soundName, bool loop = false)
        {
            // If the sound does not exsist throw an error
            if (!_availableSoundEffectInstances.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: sound with the id of " + soundName + " not found! Have you loaded it?"
                );
            _availableSoundEffectInstances[soundName].IsLooped = loop;
            _availableSoundEffectInstances[soundName].Play();
        }

        /// <summary>
        /// Plays the requested SoundEffect, however the sound can not be stopped or
        /// paused
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        public void PlayBaseSoundEffect(string soundName)
        {
            // If the sound does not exsist throw an error
            if (!_avaliableSoundEffects.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: sound with the id of " + soundName + " not found! Have you loaded it?"
                );
            _avaliableSoundEffects[soundName].Play();
        }

        /// <summary>
        /// Pauses the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName">The name of the sound</param>
        public void Pause(string soundName)
        {
            // If the sound does not exsist throw an error
            if (!_availableSoundEffectInstances.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: sound with the id of " + soundName + " not found! Have you loaded it?"
                );
            _availableSoundEffectInstances[soundName].Pause();
        }

        /// <summary>
        /// Stops the requested SoundEffectInstance
        /// </summary>
        /// <param name="soundName"></param>
        public void Stop(string soundName)
        {
            // If the sound does not exsist throw an error
            if (!_availableSoundEffectInstances.ContainsKey(soundName))
                throw new KeyNotFoundException(
                    "ERROR: sound with the id of " + soundName + " not found! Have you loaded it?"
                );
            _availableSoundEffectInstances[soundName].Stop();
        }

        /// <summary>
        /// Loads the sound so its ready to play from the specified path
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="path">The path to the sound file</param>
        public void LoadSound(string soundName, string path)
        {
            // If the sound does exsist log a warning
            if (_availableSoundEffectInstances.ContainsKey(soundName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + soundName);

            // load sound from the content manager
            _avaliableSoundEffects[soundName] = ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<SoundEffect>(path);
            
            // Create an insatnce of the sound effect
            _availableSoundEffectInstances[soundName] = _avaliableSoundEffects[soundName].CreateInstance();
        }

        /// <summary>
        /// Loads the passed SoundEffect so its ready to play 
        /// </summary>
        /// <param name="soundName">The id you want to give the sound</param>
        /// <param name="soundEffect">The SoundEffect to load</param>
        public void LoadSound(string soundName, SoundEffect soundEffect)
        {
            // If the sound does exsist log a warning
            if (_availableSoundEffectInstances.ContainsKey(soundName))
                Console.WriteLine("WARNING: you are overtiting a sound with the id of " + soundName);

            // Store the base sound effect
            _avaliableSoundEffects[soundName] = soundEffect;

            // Load the sound
            _availableSoundEffectInstances[soundName] = soundEffect.CreateInstance();
        }

        /// <summary>
        /// Change the volume of all sounds
        /// </summary>
        /// <param name="amount">The amount to increae the sound by</param>
        public void ChangeSoundEffectVolume(float amount)
        {
            // Increase the sound
            _currentVolume += amount;

            // Make sure the sound does not go above the max/below the min
            if (_currentVolume < 0)
                _currentVolume = 0;
            if (_currentVolume > 1)
                _currentVolume = 1;

            // loop through each sound an change the volume
            foreach (SoundEffectInstance sound in _availableSoundEffectInstances.Values)
                sound.Volume = _currentVolume;

            SoundEffect.MasterVolume = _currentVolume;
        }

        /// <summary>
        /// Changes the volume for songs
        /// </summary>
        /// <param name="amount"></param>
        public void ChangeSongVolume(float amount)
        {
            MediaPlayer.Volume += amount;
            // Make sure the sound does not go above the max/below the min
            if (MediaPlayer.Volume < 0)
                MediaPlayer.Volume = 0;
            if (MediaPlayer.Volume > 1)
                MediaPlayer.Volume = 1;
        }

    }
}
