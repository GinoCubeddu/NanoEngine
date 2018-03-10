using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IAiComponent
    {
        //getter for the unique ID
        int UID { get; }

        // getter for the minds controled asset
        IAsset ControledAsset { get; }

        //getter for the ynique name
        string UName { get; }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        void Update(IUpdateManager updateManager);

        /// <summary>
        /// Method to initalise the AI
        /// </summary>
        /// <param name="ent">The entity that the AI will control</param>
        void InitialiseAiComponent(IAsset ent);

        void Initialise();
    }
}
