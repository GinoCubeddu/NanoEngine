using NanoEngine.ObjectManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.ObjectTypes.Assets.Control
{
    public abstract class AiComponent : IAiComponent
    {
        //protected field for the components Unique ID
        protected int uID;
        //Public getter for the Unique ID
        public int UID
        {
            get { return uID; }
        }

        //Protected Field to hold the components unique name
        protected string uName;
        //Public getter for the unique name
        public string UName
        {
            get { return uName; }
        }

        //Protected filed to hold what entity is being controled
        protected IAsset controledEntity;

        public IAsset ControledAsset
        {
            get { return controledEntity; }
        }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public abstract void Update(IUpdateManager updateManager);

        /// <summary>
        /// Method that sets the Unique Data of the AI
        /// </summary>
        private void SetUniqueData()
        {
            //SET the unqie data to match the entiys
            // uID = controledEntity.UniqueID;
            uName = "Behaviour" + controledEntity.UniqueName;
        }

        /// <summary>
        /// Method to initalise the mind
        /// </summary>
        /// <param name="ent"></param>
        public void InitialiseAiComponent(IAsset asset)
        {
            controledEntity = asset;
            SetUniqueData();
            this.Initialise();
        }

        public abstract void Initialise();
    }
}
