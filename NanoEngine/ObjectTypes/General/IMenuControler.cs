using NanoEngine.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.General
{
    public interface IMenuControler
    {
        /// <summary>
        /// Method that will preform some code when a button is clicked
        /// </summary>
        void Clicked();

        /// <summary>
        /// Method that Initalises the menu controler by giving refrence to the menu it controls
        /// </summary>
        /// <param name="menu"></param>
        void Initalise(IMenuItem menu);
    }
}
