using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestAsset : Entity
    {
        public override void Initilise()
        {
            SetTexture(ContentManagerLoad.Manager.LoadResource<Texture2D>("player"));
            IDictionary<string, IDictionary<string, int>> AnimationDict =
                new Dictionary<string, IDictionary<string, int>>();

            AnimationDict.Add("idleRight", new Dictionary<string, int>());
            AnimationDict["idleRight"].Add("TextureWidth", 27);
            AnimationDict["idleRight"].Add("TextureHeight", 64);
            AnimationDict["idleRight"].Add("Row", 0);
            AnimationDict["idleRight"].Add("ColumnCount", 8);

            AnimationDict.Add("idleLeft", new Dictionary<string, int>());
            AnimationDict["idleLeft"].Add("TextureWidth", 27);
            AnimationDict["idleLeft"].Add("TextureHeight", 64);
            AnimationDict["idleLeft"].Add("Row", 2);
            AnimationDict["idleLeft"].Add("ColumnCount", 7);

            AnimationDict.Add("runRight", new Dictionary<string, int>());
            AnimationDict["runRight"].Add("TextureWidth", 35);
            AnimationDict["runRight"].Add("TextureHeight", 64);
            AnimationDict["runRight"].Add("Row", 1);
            AnimationDict["runRight"].Add("ColumnCount", 6);

            AnimationDict.Add("runLeft", new Dictionary<string, int>());
            AnimationDict["runLeft"].Add("TextureWidth", 35);
            AnimationDict["runLeft"].Add("TextureHeight", 64);
            AnimationDict["runLeft"].Add("Row", 3);
            AnimationDict["runLeft"].Add("ColumnCount", 6);

            AssetAnimation = new Animation(
                this, AnimationDict
            );
        }
    }
}
