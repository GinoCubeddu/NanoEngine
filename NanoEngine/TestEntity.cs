using NanoEngine.Core.Managers;
using NanoEngine.Events;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    public class TestEntity : Entity
    {
        IAIComponent mind;

        public TestEntity()
        {
            setTexture(ContentManagerLoad.Manager.GetTexture("Test"));
        }

        public override void Initilise()
        {
            mind = AIManager.Manager.CreateAI<TestAI>(this);
        }
    }
}
