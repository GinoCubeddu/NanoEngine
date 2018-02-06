﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IAIComponent
    {
        //getter for the unique ID
        int UID { get; }

        //getter for the ynique name
        string UName { get; }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        void Update();

        /// <summary>
        /// Method to initalise the AI
        /// </summary>
        /// <param name="ent">The entity that the AI will control</param>
        void Initialise(IAsset ent);
    }
}
