using System.Collections.Generic;

namespace MhwKtDroprateEditor.Models
{
    public class Droprate
    {
        public WeaponType Type { get; set; }
        public decimal R6GoldPrefix { get; set; }
        public decimal R6GoldPostfix { get; set; }
        public decimal R7 { get; set; }
        public decimal R8 { get; set; }
        public decimal Kjarr { get; set; }

        public int TotalPercentage => (int)((R6GoldPrefix + R6GoldPostfix + R7 + R8 + Kjarr) * 100);
        public bool Valid => TotalPercentage == 100;

        public byte[] ToByte
            => new List<byte>
            {
                (byte) (R6GoldPrefix * 100), 0x00, 0x00, 0x00,
                (byte) (R6GoldPostfix * 100), 0x00, 0x00, 0x00,
                (byte) (R7 * 100), 0x00, 0x00, 0x00,
                (byte) (R8 * 100), 0x00, 0x00, 0x00,
                (byte) (Kjarr * 100), 0x00, 0x00, 0x00,
            }.ToArray();

        // JsonConvert need this
        public Droprate() { }

        public Droprate(WeaponType type) => Type = type;

        public Droprate(WeaponType type, decimal r6Pre, decimal r6Post, decimal r7, decimal r8, decimal kjarr)
        {
            Type = type;
            R6GoldPrefix = r6Pre;
            R6GoldPostfix = r6Post;
            R7 = r7;
            R8 = r8;
            Kjarr = kjarr;
        }
    }
}
