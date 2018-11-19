namespace MhwKtDroprateEditor.Models
{
    public class Droprate
    {
        public WeaponType Type { get; set; }
        public decimal R6GoldPrefix { get; set; }
        public decimal R6GoldPostfix { get; set; }
        public decimal R7 { get; set; }
        public decimal R8 { get; set; }

        public Droprate(WeaponType type)
            => Type = type;

        public Droprate(WeaponType type, decimal r6Pre, decimal r6Post, decimal r7, decimal r8)
        {
            Type = type;
            R6GoldPrefix = r6Pre;
            R6GoldPostfix = r6Post;
            R7 = r7;
            R8 = r8;
        }
    }
}
