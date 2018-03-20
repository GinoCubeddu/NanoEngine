using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoEngine.Core.Locator
{
    public abstract class DefaultNanoServices
    {
        public static string SceneManager => "SceneManager";
        public static string SoundManager => "SoundManager";
        public static string ContentManager => "ContentManager";
    }
}
