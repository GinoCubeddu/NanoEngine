using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.Events;
using NanoEngine.Menus;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NanoEngine.ObjectManagement.Managers;

namespace NanoEngine
{
    class TestScreen : GameScreen
    {
        // Menu menu;

        public TestScreen()
        {
            // SoundManager.Manager.LoadLoopedSound(ContentManagerLoad.Manager.LoadResource<SoundEffect>("Sounds/600422_Hope-For-The-Lost"), "Level1");
            // SoundManager.Manager.StartLoopedSound("Level1");
            TileManager.Manager.LoadTileMap("level1");
        }

        public override void LoadContent()
        {
            //List<IMenuItem> list = new List<IMenuItem>();

            //list.Add(new MenuItem(ContentManagerLoad.Manager.LoadResource<Texture2D>("StartGameButton1"), ContentManagerLoad.Manager.LoadResource<Texture2D>("StartGameButton2"), new Vector2(100, 100)));
            //list.Add(new MenuItem(ContentManagerLoad.Manager.LoadResource<Texture2D>("resumebutton1"), ContentManagerLoad.Manager.LoadResource<Texture2D>("resumebutton2"), new Vector2(100, 200)));
            //list.Add(new MenuItem(ContentManagerLoad.Manager.LoadResource<Texture2D>("RestartLevel1"), ContentManagerLoad.Manager.LoadResource<Texture2D>("RestartLevel2"), new Vector2(100, 300)));
            //list[0].Initialise<MenuControler>();
            //list[1].Initialise<MenuControler2>();
            //list[2].Initialise<MenuControler3>();

            //menu = new Menu(list, true);

            //EventManager.Manager.AddDelegates(menu);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw()
        {
            // menu.Draw();
            RenderManager.Manager.StartDraw();
            TileManager.Manager.DrawTileMap();
            RenderManager.Manager.EndDraw();
        }

        public override void Update()
        {
        }
    }
}
