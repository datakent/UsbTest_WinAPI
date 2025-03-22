Cihaz path bilgisi ile "Bulk Transfer" / raw data transferi

Denemelerde kullanılan Path ve açıklaması:
\\?\usb#vid_0fe6&pid_811e#6&4817b6d&0&2#{a5dcbf10-6530-11d2-901f-00c04fb951ed}

vid_0fe6
Vendor ID (Üretici Kimliği), cihazın üreticisini temsil eder.

pid_811e
Product ID (Ürün Kimliği), cihaz modelini tanımlar.

6
Bağlandığı kökün (hub/root) bir indeksini gösterebilir.

4817b6d
Cihazın belirli oturumdaki "instance" kimliğidir. USB port değiştiğinde değişebilir..

0&2: Fiziksel bağlantının yerini tanımlayan bir alt bilgi

{a5dcbf10-6530-11d2-901f-00c04fb951ed}
GUID ise USB cihazları için kullanılan bir arayüz sınıfını temsil ediyor. 
Aşağıki adreslerde detay var

*Bu bilgiler büyük/küçük harf duyarlı değil.

her iki GUI ile erişim oluyor, fakat teknik dokümanlar
"a5dcbf10..." olanı kullanmayı öneriyor! Zira ilki eski sürüm Windowslar zamanında. 2binli yıllar...

https://learn.microsoft.com/en-us/windows-hardware/drivers/install/guid-class-usb-device
string devicePath = @"\\.\\usb#vid_0fe6&pid_811e#6&4817b6d&0&2#{a6782bce-4376-4de2-8096-70aa9e8fed19}";

https://learn.microsoft.com/en-us/windows-hardware/drivers/install/guid-devinterface-usb-device

İlk kısım Nirsoft'un "USBDeview" yazılımı ile alınabilir.
USBDeview InstanceID Örneği: USB\VID_0FE6&PID_811E\6&197fd1e3&0&2

Bu veriyi devicePath'a aktarırken "\" kısmı "#" ile değiştirilmeli, diğer kısımlar aynen kalmalı!
