using System;

namespace Client
{
    class GameMetadataManager: Metadata.MetadataManager
    {
        internal GameMetadataManager()
        {
            Instance = this;
        }
    }
}