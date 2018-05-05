using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Animation
{
    public class AnimationState
    {
        // The current frame in the animation
        public int CurrentFrame { get; private set; }

        public float FrameRate { get; private set; }
        
        // A list of all the frames the animation has
        private IList<Rectangle> _frames;

        public AnimationState(int frameRate)
        {
            FrameRate = 1f / frameRate;
            CurrentFrame = 0;
            _frames = new List<Rectangle>();
        }

        /// <summary>
        /// Returns how many frames the animation has
        /// </summary>
        /// <returns>The amount of frames within the animation</returns>
        public int GetFrameCount()
        {
            return _frames.Count;
        }

        /// <summary>
        /// Add a frame to the animation state
        /// </summary>
        /// <param name="posX">The X position on the texture</param>
        /// <param name="posY">The Y postion on the texture</param>
        /// <param name="frameWidth">The frame width</param>
        /// <param name="frameHeight">The frame height</param>
        public void AddFrame(int posX, int posY, int frameWidth, int frameHeight)
        {
            // Add a new rectangle to the styate
            _frames.Add(new Rectangle(posX, posY, frameWidth, frameHeight));
        }

        /// <summary>
        /// Adds the passed in frame bounds to the list
        /// </summary>
        /// <param name="frame">The rectangle position of the frame on the texture</param>
        public void AddFrame(Rectangle frame)
        {
            // Add the frame to the frams and incrment the count
            _frames.Add(frame);
        }

        /// <summary>
        /// Returns the requested frame
        /// </summary>
        /// <param name="id">The id of the requested frame</param>
        /// <returns>The requested frame</returns>
        public Rectangle GetFrame(int id)
        {
            return _frames[id];
        }

        /// <summary>
        /// Returns the current frame within the animation
        /// </summary>
        /// <returns>The current animation bounds</returns>
        public Rectangle GetCurrentFrame()
        {
            return _frames[CurrentFrame];
        }

        /// <summary>
        /// Updates the position of the animation
        /// </summary>
        /// <param name="amount"></param>
        public void ChangePosition(int amount)
        {
            // use moduls to change the frame
            // Suggestion from Twitch user Tyyppi_77
            // Modulus works by using the remander of the division
            CurrentFrame = (CurrentFrame + amount) % _frames.Count;
        }
        

        /// <summary>
        /// Resets the animation counter to 0
        /// </summary>
        public void ResetAnimation()
        {
            CurrentFrame = 0;
        }
    }
}
