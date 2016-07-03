using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IAsset
    {
        //Getter for the texture of the entity
        Texture2D Texture { get; }

        //getter for the position
        Vector2 Position { get; set; }

        //getter for the entitys bounding box
        Rectangle Bounds { get; set; }
    }
}
