
/********************************************************************
created:    2022-08-12
author:     lixianmin


Copyright (C) - All Rights Reserved
*********************************************************************/

namespace Unicorn.Web
{
    internal static class PathManager
    {
        public static string GetFullPath(string localPath)
        {
            return "Assets/" + localPath;
        }
    }
}