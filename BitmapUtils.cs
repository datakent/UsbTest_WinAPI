using System.Drawing;
using System.Linq;

namespace UsbTest_WinAPI
{
    public static class BitmapUtils
    {
        public static (int w, int h, byte[] byteArr, string hexStr) bitmap2Byte(string fileName)
        {
            //Dikkat: Transparan PNG ler komple siytah çıkabilir.
            //Zeminlerine Background Layer eklenmesi gerekir.
            //BMP dosyasından gitmek daha iyi diyebilirim.
            //
            Bitmap original = new Bitmap(fileName);

            int cx = 0;

            int width = (original.Width + 7) / 8;
            int height = original.Height;
            sbyte[] sbArr = new sbyte[width * height];
            for (int i = 0; i < sbArr.Length; i++)
                sbArr[i] = -1;

            for (int y = 0; y < original.Height; y++)
            {
                int bitIndex = 0; // Bir bayttaki bitlerin sırası
                byte currentByte = 0; // Geçici bayt

                for (int x = 0; x < original.Width; x++)
                {
                    Color originalColor = original.GetPixel(x, y);

                    // Gri ton değeri hesapla
                    int grayValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);

                    // Monokrom için gri değere göre siyah veya beyaz atama yap
                    Color monoColor = grayValue > 128 ? Color.White : Color.Black;

                    // Siyah-beyaz eşik kontrolü
                    int bitValue = grayValue > 128 ? 1 : 0;

                    // Bit'i mevcut bayta kaydırarak ekle
                    // currentByte << 1: Mevcut baytı sola kaydırarak yeni bit için yer açar.
                    // | bitValue: Yeni bit değerini bayta ekler.
                    currentByte = (byte)((currentByte << 1) | bitValue);
                    bitIndex++;

                    //#FFFFFF=white   -1=11111111
                    //#000000=black    0=00000000
                    //monochromeBitmap.SetPixel(x, y, bcx);

                    // Her 8 bit dolduğunda byte'ı kaydet
                    if (bitIndex == 8)
                    {
                        //0xFF -> 11111111 binary'nin byte(işaretsiz) = 255 sbyte(işaretli) = -1
                        sbArr[cx++] = unchecked((sbyte)currentByte);
                        bitIndex = 0;
                        currentByte = 0;
                    }
                }

                //Eğer 8 bite ulaşılmadıysa kalanları tamamla
                //0xFF = 11111111
                //Örneğin, bitIndex = 3 ise, 0xFF >> 3 = 00011111 olur. Bu, boş kalan 5 biti 1 ile doldurur.
                if (bitIndex > 0)
                {
                    currentByte <<= (8 - bitIndex); // mevcut bitleri sola kaydır. -- Boş bitleri sola kaydır
                    currentByte |= (byte)(0xFF >> bitIndex); // kalan boş bitleri 1 ile doldur.
                    sbArr[cx++] = unchecked((sbyte)currentByte);
                }
            }
            original.Dispose();

            byte[] byteArray = sbArr.Select(s => (byte)s).ToArray();

            // 0x formatında string oluştur
            string byteString = string.Join(",", byteArray.Select(b => $"0x{b:X2}"));

            return (width, height, byteArray, byteString);
        }
    }
}
