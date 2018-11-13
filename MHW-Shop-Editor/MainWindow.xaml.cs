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
