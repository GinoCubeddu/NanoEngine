using Microsoft.Xna.Framework;
using NanoEngine.Core.Managers;
using NanoEngine.Events;
using NanoEngine.Menus;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestScreen : GameScreen
    {
        Menu menu;

        public TestScreen()
        {
         
        }

        public override void LoadContent()
        {
            List<IMenuItem> list = new List<IMenuItem>();

            list.Add(new MenuItem(ContentManagerLoad.Manager.GetTexture("StartGameButton1"), ContentManagerLoad.Manager.GetTexture("StartGameButton2"), new Vector2(100, 100)));
            list.Add(new MenuItem(ContentManagerLoad.Manager.GetTexture("resumebutton1"), ContentManagerLoad.Manager.GetTexture("resumebutton2"), new Vector2(100, 200)));
            list.Add(new MenuItem(ContentManagerLoad.Manager.GetTexture("RestartLevel1"), ContentManagerLoad.Manager.GetTexture("RestartLevel2"), new Vector2(100, 300)));
            list[0].Initialise<MenuControler>();
            list[1].Initialise<MenuControler2>();
            list[2].Initialise<MenuControler3>();

            menu = new Menu(list, true);

            EventManager.Manager.AddDelegates(menu);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw()
        {
            menu.Draw();
        }

        public override void Update()
        {
        }
    }
}
