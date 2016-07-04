using Microsoft.Xna.Framework;
using NanoEngine.Core.Managers;
using NanoEngine.Menus;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    public class MenuControler3 : IMenuControler
    {
        private IMenuItem _menu;

        /// <summary>
        /// Constructor for the menu controler
        /// </summary>
        public MenuControler3()
        {

        }

        /// <summary>
        /// Method that will preform some code when a button is clicked
        /// </summary>
        public void Clicked()
        {
            UpdateManager.Manager.ExitGame();
        }

        /// <summary>
        /// Method that Initalises the menu controler by giving refrence to the menu it controls
        /// </summary>
        /// <param name="menu"></param>
        public void Initalise(IMenuItem menu)
        {
            _menu = menu;
        }
    }
}
