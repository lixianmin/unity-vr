
// Warning: all code of this file are generated automatically, so do not modify it manually ~
// Any questions are welcome, mailto:lixianmin@gmail.com

using System;
using System.Collections;

namespace Metadata
{
    public class OuterMetaFactory
    {
        private static Hashtable _GetLookupTableByName ()
        {
            var table = new Hashtable(2);
            table.Add ("Metadata.PetEatFishTemplate", new MetaCreator(()=> new Metadata.PetEatFishTemplate()));
            table.Add ("Metadata.PetEatFishTemplate+StatUp", new MetaCreator(()=> new Metadata.PetEatFishTemplate.StatUp()));
            return table;
        }
    }
}
