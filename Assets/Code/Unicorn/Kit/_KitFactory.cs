
// Warning: all code of this file are generated automatically, so do not modify it manually ~
// Any questions are welcome, mailto:lixianmin@gmail.com

using System;
using System.Collections;

namespace Unicorn.Kit
{
    public class _KitFactory
    {
        [UnityEngine.Scripting.Preserve]
        private static Hashtable _GetLookupTableByName ()
        {
            return new Hashtable(2)
            {
                { "Client.BowlKit", (Func<KitBase>)(() => new Client.BowlKit()) },
                { "Client.PlayerMoveKit", (Func<KitBase>)(() => new Client.PlayerMoveKit()) },
            };
        }
    }
}
