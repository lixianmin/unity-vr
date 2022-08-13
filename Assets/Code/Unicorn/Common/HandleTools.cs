/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;

namespace Unicorn
{
    public static class HandleTools
    {
        public static void SafeHandle<T>(Action<T> handler, T argument )
        {
            if (handler == null) return;
            
            try
            {
                handler(argument);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}