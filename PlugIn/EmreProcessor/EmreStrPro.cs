using DenyoSDK;

namespace EmreProcessor
{
    public class EmreStrPro : IStringProcess
    {
        public string GetName()
        {
            return "Büyük Harfe Çevir";
        }

        public string Process(string text)
        {
            return text.ToUpper();
        }
    }

    public class EmreStrPro2 : IStringProcess
    {
        public string GetName()
        {
            return "Küçük Harfe Çevir";
        }

        public string Process(string text)
        {
            return text.ToLower();
        }
    }
}
