using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectManagement.Interfaces;

namespace NanoEngine
{
    interface IAssetmanagerNeeded
    {
        IAssetManager AssetManager { set; }
    }
}
