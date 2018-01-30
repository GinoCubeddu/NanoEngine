using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Managers
{
    public class EntityManager : IAssetFactory
    {
        //Private static field holding the refrence to the manager
        private static IAssetFactory manager;

        //Private list that holds the current entitys
        private List<IEntity> EntityList;

        //private static field that holds the current highest unique ID
        private static int uniqueID;

        //private boolean field to determin if the main player is set
        private bool mainPlayerSet;

        //A public static getter to return the instance of the manager sinice its static it will be the same instance every time
        public static IAssetFactory Manager
        {
            //uses the null coalescing operator to check if the manager has a null refrence if it does it creates a new instance
            get { return manager ?? (manager = new EntityManager()); }
        }

        /// <summary>
        /// Private constructor so it can only be called in the class by the getter making it a singleton
        /// </summary>
        private EntityManager()
        {
            //Instansiate instance vars
            uniqueID = 2;
            EntityList = new List<IEntity>();
            mainPlayerSet = false;
        }

        /// <summary>
        /// Method to add multiple entitys at once
        /// </summary>
        /// <param name="list">A list of entitys to be added to the manager</param>
        public void addEntitys(List<IEntity> list)
        {
            foreach (var item in list)
            {
                uniqueID++;
                item.setUniqueData((item.Texture.Name + uniqueID), uniqueID);
                item.Initilise();
                item.UpdateBounds();
                EntityList.Add(item);
            }
        }

        /// <summary>
        /// Using generics to create a new IEntity
        /// </summary>
        /// <typeparam name="T">Type of IEntity passed in</typeparam>
        /// <param name="pos1">Position for x axis</param>
        /// <param name="pos2">Position for y axis</param>
        public void AddEntity<T>(float pos1, float pos2, string uName) where T : IEntity, new()
        {
            //Create new entity
            IEntity ent = new T();
            //Add entity to list
            EntityList.Add(ent);
            //Increase and set unquie id and name         
            if (uName == null)
            {
                ent.setUniqueData((ent.Texture.Name + uniqueID), uniqueID);
            }
            else
            {
                ent.setUniqueData(uName, uniqueID);
            }
            //set the start position
            ent.setPosition(new Vector2(pos1, pos2));

            ent.Initilise();
            //Increase and set unquie id and name
            uniqueID++;
        }

        /// <summary>
        /// Returns a new entity that can be used outside of the entity list
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="pos1">position on x axis</param>
        /// <param name="pos2">position on y axis</param>
        /// <param name="uName">The name to give the entity</param>
        /// <returns>a newly created entity</returns>
        public IEntity RetriveNewEntity<T>(float pos1, float pos2, string uName) where T : IEntity, new()
        {
            //Create new entity
            IEntity ent = new T();
            //Increase and set unquie id and name         
            if (uName == null)
            {
                ent.setUniqueData((ent.Texture.Name + uniqueID), uniqueID);
            }
            else
            {
                ent.setUniqueData(uName, uniqueID);
            }
            //set the start position
            ent.setPosition(new Vector2(pos1, pos2));
            //Increase and set unquie id and name
            uniqueID++;
            return ent;
        }

        /// <summary>
        /// Passes back a list of the current Entitys
        /// </summary>
        /// <returns>List of type IEntity</returns>
        public List<IEntity> GetList()
        {
            return EntityList;
        }

        /// <summary>
        /// Removes an entity from the list based on its ID
        /// </summary>
        /// <param name="id">Unique ID of an entity</param>
        public void remove(int id)
        {
            //Checks if there is an entity in the list that has the passed in ID
            var ent1 = EntityList.Where(x => x.UniqueID == id).ToArray();
            //Remove entity
            EntityList.Remove(ent1[0]);
            //Set the entity to null
            ent1[0] = null;
        }

        /// <summary>
        /// Returns the entity by the ID passed in
        /// </summary>
        /// <param name="id">Id for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        public IEntity getEntity(int id)
        {
            IEntity ent = null;
            //Checks if there is an entity in the list that has the passed in ID
            var ent1 = EntityList.Where(x => x.UniqueID == id).ToArray();
            if (ent1.Count() != 0)
            {
                ent = ent1[0];
            }
            return ent;
        }

        /// <summary>
        /// Returns the entity by the name passed in
        /// </summary>
        /// <param name="name">name for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        public IEntity getEntityByName(string name)
        {
            IEntity ent = null;
            //Checks if there is an entity in the list that has the passed in ID
            var ent1 = EntityList.Where(x => x.UniqueName == name).ToArray();
            if (ent1.Count() != 0)
            {
                ent = ent1[0];
            }
            return ent;
        }

        /// <summary>
        /// Emptys the current entity list
        /// </summary>
        public void emptyList()
        {
            EntityList.Clear();
            EntityList.TrimExcess();
            mainPlayerSet = false;
        }
    }
}
