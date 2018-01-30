using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface IAssetFactory
    {
        /// <summary>
        /// Using generics to create a new IEntity
        /// </summary>
        /// <typeparam name="T">Type of IEntity passed in</typeparam>
        /// <param name="pos1">Position for x axis</param>
        /// <param name="pos2">Position for y axis</param>
        void AddEntity<T>(float pos1, float pos2, string uName) where T : IEntity, new();

        /// <summary>
        /// Passes back a list of the current Entitys
        /// </summary>
        /// <returns>List of type IEntity</returns>
        List<IEntity> GetList();

        /// <summary>
        /// Emptys the current entity list
        /// </summary>
        void emptyList();

        /// <summary>
        /// Removes an entity from the list based on its ID
        /// </summary>
        /// <param name="id">Unique ID of an entity</param>
        void remove(int id);

        /// <summary>
        /// Returns the entity by the ID passed in
        /// </summary>
        /// <param name="id">Id for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        IEntity getEntity(int id);

        /// <summary>
        /// Add a list of entitys to the game
        /// </summary>
        /// <param name="list"></param>
        void addEntitys(List<IEntity> list);

        /// <summary>
        /// Returns the entity by the name passed in
        /// </summary>
        /// <param name="name">name for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        IEntity getEntityByName(string name);

        /// <summary>
        /// Returns a new entity that can be used outside of the entity list
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="pos1">position on x axis</param>
        /// <param name="pos2">position on y axis</param>
        /// <param name="uName">The name to give the entity</param>
        /// <returns>a newly created entity</returns>
        IEntity RetriveNewEntity<T>(float pos1, float pos2, string uName) where T : IEntity, new();
    }
}
