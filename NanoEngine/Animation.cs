using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;

namespace NanoEngine
{
    public class Animation
    {
        private IDictionary<string, IDictionary<string, int>> States;

        private Texture2D SpriteSheet;

        private string CurrentAninmation;

        private int CurrentColumn;

        private int Timer;

        public Animation(
            IDictionary<string, IDictionary<string, int>> states,
            Texture2D spriteSheet
        )
        {
            SpriteSheet = spriteSheet;
            States = states;
            CurrentAninmation = states.Keys.First();
            CurrentColumn = 0;
            Timer = 0;
        }

        public void ChangeAnimationState(string animationState)
        {
            if (animationState == CurrentAninmation)
                return;

            if (!States.Keys.Contains(animationState))
                throw new Exception(
                    string.Format(
                        "The animation state with the id {0} does not " +
                        "exsist. The avaliable options are: {1}",
                        animationState, States.Keys.ToString()
                    )
                );
            CurrentAninmation = animationState;
            CurrentColumn = 0;
        }

        public void Animate(IRenderManager renderManager)
        {
            IDictionary<string, int> AnimationData = States[CurrentAninmation];
            renderManager.Draw(
                    SpriteSheet, new Vector2(100f, 100f),
                    new Rectangle(
                        CurrentColumn * AnimationData["TextureWidth"],
                        AnimationData["TextureHeight"] * AnimationData["Row"],
                        AnimationData["TextureWidth"],
                        AnimationData["TextureHeight"]
                    ),
                    Color.White
                );
            if (Timer > 3)
            {
                CurrentColumn++;

                if (CurrentColumn >= AnimationData["ColumnCount"])
                    CurrentColumn = 0;
                Timer = 0;
            }
            Timer++;

        }
    }
}
