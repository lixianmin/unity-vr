
/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
using System.IO;

namespace Unicorn
{
	public static class ClientFileTools
	{
		private const int _bombLength = 3;

		private static readonly byte[] _bombBuffer = new byte[3];

		public static Stream OpenFileByStream(string path)
		{
			if (path == null || !File.Exists(path))
			{
				return null;
			}

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
			return stream;
		}

		public static byte[] ReadAllBytesNoBomb(string path)
		{
            using FileStream fileStream = new (path, FileMode.Open, FileAccess.Read, FileShare.Read);
            long num = fileStream.Length;
            if (num > int.MaxValue)
            {
                throw new IOException("The file is too long for ReadAllBytes().");
            }

            bool flag = false;
            if (num >= 3)
            {
                _bombBuffer[0] = 0;
                int num2 = fileStream.Read(_bombBuffer, 0, 3);
                if (num2 == 3)
                {
                    flag = (_bombBuffer[0] == 239 && _bombBuffer[1] == 187 && _bombBuffer[2] == 191);
                }
                if (flag)
                {
                    num -= 3;
                }
                else
                {
                    fileStream.Position = 0L;
                }
            }

            int num3 = 0;
            int num4 = (int)num;
            byte[] array = new byte[num];

            while (num4 > 0)
            {
                int num5 = fileStream.Read(array, num3, num4);
                if (num5 == 0)
                {
                    throw new EndOfStreamException();
                }
                num3 += num5;
                num4 -= num5;
            }
            return array;
        }
	}
}
