using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsbTest_WinAPI
{
    public class TSPLCmd
    {
        List<byte> cmdList = new List<byte>();

        public byte[] data => cmdList.ToArray();
        public uint dataSize => (uint)cmdList.Count;

        public void addCmd(string value)
            => cmdList.AddRange(Encoding.ASCII.GetBytes(value + "\r\n"));

        public void addCmdText(int x, int y, int fSize, string value)
            => cmdList.AddRange(Encoding.ASCII.GetBytes($"TEXT {x},{y},\"TST24.BF2\",0,{fSize},{fSize},\"{value}\"\r\n"));

        public void addCmdQR(int x, int y, string value)
            => cmdList.AddRange(Encoding.ASCII.GetBytes($"QRCODE {x},{y},H,4,A,0,\"{value}\"\r\n"));

        public void addCmdImg(int x, int y, int w, int h, byte[] value)
            => cmdList.AddRange(Encoding.ASCII.GetBytes($"BITMAP {x},{y},{w},{h},0,").Concat(value).Concat(new byte[] { 0x0d, 0x0a }));

        public void addCmdImgFromFile(int x, int y, string fileName)
        {
            var bx = BitmapUtils.bitmap2Byte(fileName);
            cmdList.AddRange(Encoding.ASCII.GetBytes($"BITMAP {x},{y},{bx.w},{bx.h},0,").Concat(bx.byteArr).Concat(new byte[] { 0x0d, 0x0a }));
        }

        public void save2Disk(string fullPath)
            => System.IO.File.WriteAllBytes(fullPath, data);

        public void clear()
            => cmdList.Clear();

        public TSPLCmd()
        {
        }
    }

}
