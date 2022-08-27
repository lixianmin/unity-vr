
// Warning: all code of this file are generated automatically, so do not modify it manually ~
// Any questions are welcome, mailto:lixianmin@gmail.com

using System;
using System.Collections;

namespace Unicorn
{
    public class _KitFactory
    {
        private static Hashtable _GetLookupTableByName ()
        {
            return new Hashtable(3)
            {
                { "Client.AnotherKit", (Func<KitBase>)(() => new Client.AnotherKit()) },
                { "Client.BowlKit", (Func<KitBase>)(() => new Client.BowlKit()) },
                { "Client.TestKit", (Func<KitBase>)(() => new Client.TestKit()) },
            };
        }
    }
}
