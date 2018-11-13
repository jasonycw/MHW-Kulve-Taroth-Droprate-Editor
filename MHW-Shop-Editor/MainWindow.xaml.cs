using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MHW_Shop_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static readonly string[] DEFAULT_ITEMS = { "0001", "0005", "000D", "0011", "0046", "0047", "0048", "004A", "004B", "004F", "0058", "0059", "005A", "005C", "0055", "0056", "0057", "0066", "0067", "008A", "008C", "008D", "008F", "0090", "0092", "0093", "0095", "0096", "0098", "0099", "009A", "009B", "009D", "009E", "009F", "00A0", "00A1", "00A2", "00A3", "00A4", "00A5", "00A7", "00A8", "00A9", "00AE", "00AF", "00B0", "00B1", "00B2", "00B4" };
        public static readonly string[] GEMS = { "02D7", "02D8", "02D9", "02DA", "02DB", "02DC", "02DD", "02DE", "02DF", "02E0", "02E1", "02E2", "02E3", "02E4", "02E5", "02E6", "02E7", "02E8", "02E9", "02EA", "02EB", "02EC", "02ED", "02EE", "02EF", "02F0", "02F1", "02F2", "02F3", "02F4", "02F5", "02F6", "02F7", "02F8", "02F9", "02FA", "02FB", "02FC", "02FD", "02FE", "02FF", "0300", "0301", "0302", "0303", "0304", "0305", "0306", "0307", "0308", "0309", "030A", "030B", "030C", "030D", "030E", "030F", "0310", "0311", "0312", "0313", "0314", "0315", "0316", "0317", "0318", "0319", "031A", "031B", "031C", "031D", "031E", "031F", "0320", "0321", "0322", "0323", "0324", "0325", "0326", "0327", "0328", "0329", "032A", "032B", "032C", "032D", "032E", "032F", "0330", "0331", "0332", "0333", "0334", "0335", "0336", "0337", "0338", "0339", "033A", "033B", "033C", "033D", "033E", "033F", "0340", "0341", "0342", "0343", "0344", "0345", "0346", "0347", "0348", "0349", "034A", "036A", "036B", "036C", "036D", "036E" };
        public static readonly string[] CONSUMABLES = { "0001", "0002", "0003", "0004", "0005", "0006", "0007", "0008", "0009", "000A", "000B", "000C", "000D", "000E", "000F", "0010", "0011", "0012", "0013", "0014", "0015", "0016", "0017", "0018", "0019", "001A", "001B", "001C", "001D", "001E", "001F", "0020", "0021", "0022", "0023", "0024", "0025", "0026", "0027", "0028", "0029", "002A", "002B", "002C", "002D", "002E", "002F", "0030", "0031", "0032", "0033", "0034", "0035", "0036", "0037", "0038", "0039", "003A", "003B", "003C", "003D", "003E", "003F", "0040", "0041", "0042", "0043", "0044", "0045", "0046", "0047", "0048", "0049", "004A", "004B", "004C", "004D", "004E", "004F", "0050", "0051", "0052", "0053", "0054", "005E", "005F", "0060", "0061", "0062", "0063", "0064", "0065", "0066", "0067", "0068", "0069", "006A", "006B", "006C", "006D", "006E", "006F", "0070", "0071", "008A", "008B", "008C", "008D", "008E", "008F", "0090", "0091", "0092", "0093", "0094", "0095", "0096", "0097", "0098", "0099", "009A", "009B", "009C", "009D", "009E", "009F", "00A0", "00A1", "00A2", "00A3", "00A4", "00A5", "00A6", "00A7", "00A8", "00A9", "00AA", "00AB", "00AE", "00AF", "00B0", "00B1", "00B2", "00B3", "00B4", "00C3", "00C4", "00C5", "00C6", "00C7" };
        public static readonly string[] LR_MATERIALS = { "00CD", "00CE", "00CF", "00D2", "00D3", "00D5", "00D8", "00E1", "00E2", "00E3", "00E4", "00E5", "00E6", "00EA", "00EB", "00EC", "00ED", "00EE", "00F3", "00F5", "00F7", "00F9", "00FB", "00FD", "00FF", "0101", "0103", "0108", "0109", "010B", "010D", "010F", "0110", "0111", "0114", "0115", "0118", "011C", "011D", "0121", "0122", "0125", "0126", "0129", "012A", "012B", "012E", "012F", "0130", "0131", "0135", "0136", "0137", "0138", "013D", "013E", "013F", "0140", "0141", "0146", "0147", "0148", "0149", "014A", "014B", "014F", "0150", "0151", "0152", "0157", "0158", "0159", "015A", "015B", "0160", "0161", "0162", "0163", "0164", "0165", "016B", "016C", "016D", "016E", "016F", "0176", "0177", "0178", "0179", "017E", "017F", "0180", "0181", "0186", "0187", "0188", "0189", "018A", "018F", "0190", "0191", "0192", "0194", "0198", "0199", "019A", "019B", "019C", "019D", "01A3", "01A4", "01A5", "01A6", "01A7", "01A8", "01AE", "01AF", "01B0", "01B1", "01B2", "01B3", "01B4", "01BE", "01BF", "01C0", "01C1", "01C2", "01C3", "01CB", "01CC", "01CD", "01CE", "01D3", "01D4", "01D5", "01D6", "01D8" };
        public static readonly string[] HR_MATERIALS = { "00D0", "00D1", "00D4", "00D6", "00D7", "00D9", "00DA", "00DB", "00DC", "00DD", "00DE", "00DF", "00E0", "00E7", "00E8", "00E9", "00EF", "00F0", "00F1", "00F2", "00F4", "00F6", "00F8", "00FA", "00FC", "00FE", "00100", "00102", "00104", "00105", "00106", "00107", "0010A", "0010C", "0010E", "00112", "00113", "00116", "00117", "00119", "0011A", "0011B", "0011E", "0011F", "00120", "00123", "00124", "00127", "00128", "0012C", "0012D", "00132", "00133", "00134", "00139", "0013A", "0013B", "0013C", "00142", "00143", "00144", "00145", "0014C", "0014D", "0014E", "00153", "00154", "00155", "00156", "0015C", "0015D", "0015E", "0015F", "00166", "00167", "00168", "00169", "0016A", "00170", "00171", "00172", "00173", "00174", "00175", "0017A", "0017B", "0017C", "0017D", "00182", "00183", "00184", "00185", "0018B", "0018C", "0018D", "0018E", "00193", "00195", "00196", "00197", "0019E", "0019F", "001A0", "001A1", "001A2", "001A9", "001AA", "001AB", "001AC", "001AD", "001B5", "001B6", "001B7", "001B8", "001B9", "001BA", "001BB", "001BC", "001BD", "001C4", "001C5", "001C6", "001C7", "001C8", "001C9", "001CA", "001CF", "001D0", "001D1", "001D2", "001D7", "001D9", "001DA", "001DB", "001DC", "001DD", "001DE", "001DF", "001E0", "001E1", "001E2", "001E3", "001E4", "001E5", "001E6", "001E7", "001E8", "001E9", "001EA", "001EB", "001EC", "001ED", "001EE", "001EF", "001F0", "001F1", "001F2", "001F3", "001F4", "001F5", "001F6", "001F7", "001F8", "001F9", "001FA", "001FB", "001FC", "001FD", "001FE", "001FF", "00200", "00201", "00202", "00203", "00204", "00205", "00206", "00207", "00208", "00209", "0020A", "0020B", "0020C", "0020D", "0020E", "0020F", "00210", "00211", "00212", "00213", "00214", "00215", "00216", "00217", "00218", "00219", "036F", "0370", "0371", "0372", "0373", "0374", "0375", "0376", "0377", "0378", "0379", "037A", "037B", "037C", "037F", "0380", "0381", "0382", "0383", "0384", "0385" };

        public MainWindow()
        {
            InitializeComponent();
            Clear();
        }

        private void Populate_Boxes(List<string> items)
        {
            foreach (ComboBox cb in listview.Items)
            {
                if (items.Count == 0)
                {
                    break;
                }
                cb.SelectedIndex = int.Parse(items[0], System.Globalization.NumberStyles.HexNumber);
                items.RemoveAt(0);
            }
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "shopList",
                DefaultExt = ".slt",
                Filter = "Shop List file | *.slt",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory
            };

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                byte[] input = System.IO.File.ReadAllBytes(filename);
                byte[] buffer = new byte[2];
                List<string> items = new List<string>();
                for (int i = 10; i < input.Length-1; i+=12)
                {
                    buffer[0] = input[i+1];
                    buffer[1] = input[i];
                    items.Add(BitConverter.ToString(buffer).Replace("-", ""));
                }
                Populate_Boxes(items);
            }
        }

        private async void saveFile(object sender, RoutedEventArgs e)
        {
            System.IO.Stream fs;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = "shopList",
                DefaultExt = ".slt",
                Filter = "Shop List file | *.slt",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory
            };


            if (dlg.ShowDialog() == true)
            {
                if ((fs = dlg.OpenFile()) != null)
                {
                    byte[] header = new byte[] { 0x18, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                    byte[] buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                    List<byte> items = header.ToList();
                    foreach (ComboBox cb in listview.Items)
                    {
                        Item item = (Item) cb.SelectedItem;
                        string hex = item.Key.Substring(4);
                        if (hex.Equals("0000"))
                        {
                            continue;
                        } else
                        {
                            items.Add(Convert.ToByte(int.Parse(hex.Substring(2), System.Globalization.NumberStyles.HexNumber)));
                            items.Add(Convert.ToByte(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)));
                            items.AddRange(buffer);
                        }                        
                    }
                    byte[] output = items.ToArray();
                    await fs.WriteAsync(output, 0, output.Length);
                    fs.Close();
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();

            switch (((sender as ComboBox).SelectedItem as ComboBoxItem).Tag.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("Lang.en-US.xaml", UriKind.Relative);
                    break;
                case "es-ES":
                    dict.Source = new Uri("Lang.es-ES.xaml", UriKind.Relative);
                    break;
                default:
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
            Clear();
        }

        private void Default_Items(object sender, RoutedEventArgs e)
        {
            Clear();
            Populate_Boxes(DEFAULT_ITEMS.ToList());
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            foreach (ComboBox cb in listview.Items)
            {
                cb.SelectedIndex = 0;
            }
        }

        private void All_Gems(object sender, RoutedEventArgs e)
        {
            Clear();
            Populate_Boxes(GEMS.ToList());
        }

        private void All_Consumables(object sender, RoutedEventArgs e)
        {
            Clear();
            Populate_Boxes(CONSUMABLES.ToList());
        }

        private void LR_Materials(object sender, RoutedEventArgs e)
        {
            Clear();
            Populate_Boxes(LR_MATERIALS.ToList());
        }

        private void HR_Materials(object sender, RoutedEventArgs e)
        {
            Clear();
            Populate_Boxes(HR_MATERIALS.ToList());
        }
    }

    public class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }

}
