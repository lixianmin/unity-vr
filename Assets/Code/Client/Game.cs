/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
#pragma warning disable 0436
using System;

namespace Client
{
    public class Game
    {
        private Game()
        {

        }

        public void Tick(float deltaTime)
        {

        }

        public static readonly Game Instance = new();
        private readonly GameMetadataManager _metadataManager = new();
    }
}
