using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.Testing
{
    class TestMind : AIComponent, IKeyboardWanted
    {
        private string Direction;

        public TestMind()
        {
            Direction = "right";
        }

        public void OnKeyDown(object sender, NanoKeyboardEventArgs args)
        {

            if (args.MyKeys.Contains(Keys.Left) & Direction == "left")
                controledEntity.AssetAnimation.ChangeAnimationState("runLeft");
            else if (args.MyKeys.Contains(Keys.Right) & Direction == "right")
                controledEntity.AssetAnimation.ChangeAnimationState("runRight");

            if (args.MyKeys.Contains(Keys.S))
                controledEntity.AssetAnimation.ChangeAnimationState("idleRight");
        }

        public void OnKeyPressed(object sender, NanoKeyboardEventArgs args)
        {
            if (args.MyKeys.Contains(Keys.Left))
            {
                Direction = "left";
                Console.WriteLine("PRESSED LEFT");
            }
            if (args.MyKeys.Contains(Keys.Right))
                Direction = "right";
        }

        public void OnKeyReleased(object sender, NanoKeyboardEventArgs args)
        {
            for (int i = 0; i < args.MyKeys.Count; i++)
                Console.WriteLine(args.MyKeys[i]);
            if (args.MyKeys.Contains(Keys.Left) && Direction == "left")
                controledEntity.AssetAnimation.ChangeAnimationState("idleLeft");
            if (args.MyKeys.Contains(Keys.Right) && Direction == "right")
                controledEntity.AssetAnimation.ChangeAnimationState("idleRight");
        }

        public override void Update()
        {

        }
    }
}