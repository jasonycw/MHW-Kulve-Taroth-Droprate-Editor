using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MHWShopEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public static readonly string[] DEFAULT_ITEMS = { "0001", "0005", "000D", "0011", "0046", "0047", "0048", "004A", "004B", "004F", "0058", "0059", "005A", "005C", "0055", "0056", "0057", "0066", "0067", "008A", "008C", "008D", "008F", "0090", "0092", "0093", "0095", "0096", "0098", "0099", "009A", "009B", "009D", "009E", "009F", "00A0", "00A1", "00A2", "00A3", "00A4", "00A5", "00A7", "00A8", "00A9", "00AE", "00AF", "00B0", "00B1", "00B2", "00B4" };
        public static readonly string[] GEMS = { "02D7", "02D8", "02D9", "02DA", "02DB", "02DC", "02DD", "02DE", "02DF", "02E0", "02E1", "02E2", "02E3", "02E4", "02E5", "02E6", "02E7", "02E8", "02E9", "02EA", "02EB", "02EC", "02ED", "02EE", "02EF", "02F0", "02F1", "02F2", "02F3", "02F4", "02F5", "02F6", "02F7", "02F8", "02F9", "02FA", "02FB", "02FC", "02FD", "02FE", "02FF", "0300", "0301", "0302", "0303", "0304", "0305", "0306", "0307", "0308", "0309", "030A", "030B", "030C", "030D", "030E", "030F", "0310", "0311", "0312", "0313", "0314", "0315", "0316", "0317", "0318", "0319", "031A", "031B", "031C", "031D", "031E", "031F", "0320", "0321", "0322", "0323", "0324", "0325", "0326", "0327", "0328", "0329", "032A", "032B", "032C", "032D", "032E", "032F", "0330", "0331", "0332", "0333", "0334", "0335", "0336", "0337", "0338", "0339", "033A", "033B", "033C", "033D", "033E", "033F", "0340", "0341", "0342", "0343", "0344", "0345", "0346", "0347", "0348", "0349", "034A", "036A", "036B", "036C", "036D", "036E" };
        public static readonly string[] CONSUMABLES = { "0001", "0002", "0003", "0004", "0005", "0006", "0007", "0008", "0009", "000A", "000B", "000C", "000D", "000E", "000F", "0010", "0011", "0012", "0013", "0014", "0015", "0016", "0017", "0018", "0019", "001A", "001B", "001C", "001D", "001E", "001F", "0020", "0021", "0022", "0023", "0024", "0025", "0026", "0027", "0028", "0029", "002A", "002B", "002C", "002D", "002E", "002F", "0030", "0031", "0032", "0033", "0034", "0035", "0036", "0037", "0038", "0039", "003A", "003B", "003C", "003D", "003E", "003F", "0040", "0041", "0042", "0043", "0044", "0045", "0046", "0047", "0048", "0049", "004A", "004B", "004C", "004D", "004E", "004F", "0050", "0051", "0052", "0053", "0054", "005E", "005F", "0060", "0061", "0062", "0063", "0064", "0065", "0066", "0067", "0068", "0069", "006A", "006B", "006C", "006D", "006E", "006F", "0070", "0071", "008A", "008B", "008C", "008D", "008E", "008F", "0090", "0091", "0092", "0093", "0094", "0095", "0096", "0097", "0098", "0099", "009A", "009B", "009C", "009D", "009E", "009F", "00A0", "00A1", "00A2", "00A3", "00A4", "00A5", "00A6", "00A7", "00A8", "00A9", "00AA", "00AB", "00AE", "00AF", "00B0", "00B1", "00B2", "00B3", "00B4", "00C3", "00C4", "00C5", "00C6", "00C7" };
        public static readonly string[] LR_MATERIALS = { "00CD", "00CE", "00CF", "00D2", "00D3", "00D5", "00D8", "00E1", "00E2", "00E3", "00E4", "00E5", "00E6", "00EA", "00EB", "00EC", "00ED", "00EE", "00F3", "00F5", "00F7", "00F9", "00FB", "00FD", "00FF", "0101", "0103", "0108", "0109", "010B", "010D", "010F", "0110", "0111", "0114", "0115", "0118", "011C", "011D", "0121", "0122", "0125", "0126", "0129", "012A", "012B", "012E", "012F", "0130", "0131", "0135", "0136", "0137", "0138", "013D", "013E", "013F", "0140", "0141", "0146", "0147", "0148", "0149", "014A", "014B", "014F", "0150", "0151", "0152", "0157", "0158", "0159", "015A", "015B", "0160", "0161", "0162", "0163", "0164", "0165", "016B", "016C", "016D", "016E", "016F", "0176", "0177", "0178", "0179", "017E", "017F", "0180", "0181", "0186", "0187", "0188", "0189", "018A", "018F", "0190", "0191", "0192", "0194", "0198", "0199", "019A", "019B", "019C", "019D", "01A3", "01A4", "01A5", "01A6", "01A7", "01A8", "01AE", "01AF", "01B0", "01B1", "01B2", "01B3", "01B4", "01BE", "01BF", "01C0", "01C1", "01C2", "01C3", "01CB", "01CC", "01CD", "01CE", "01D3", "01D4", "01D5", "01D6", "01D8" };
        public static readonly string[] HR_MATERIALS = { "00D0", "00D1", "00D4", "00D6", "00D7", "00D9", "00DA", "00DB", "00E7", "00E8", "00E9", "00EF", "00F0", "00F1", "00F2", "00F4", "00F6", "00F8", "00FA", "00FC", "00FE", "0100", "0102", "0104", "0105", "0106", "0107", "010A", "010C", "010E", "0112", "0113", "0116", "0117", "0119", "011A", "011B", "011E", "011F", "0120", "0123", "0124", "0127", "0128", "012C", "012D", "0132", "0133", "0134", "0139", "013A", "013B", "013C", "0142", "0143", "0144", "0145", "014C", "014D", "014E", "0153", "0154", "0155", "0156", "015C", "015D", "015E", "015F", "0166", "0167", "0168", "0169", "016A", "0170", "0171", "0172", "0173", "0174", "0175", "017A", "017B", "017C", "017D", "0182", "0183", "0184", "0185", "018B", "018C", "018D", "018E", "0195", "0196", "0197", "019E", "019F", "01A0", "01A1", "01A2", "01A9", "01AA", "01AB", "01AC", "01AD", "01B5", "01B6", "01B7", "01B8", "01B9", "01BA", "01BB", "01BC", "01BD", "01C4", "01C5", "01C6", "01C7", "01C8", "01C9", "01CA", "01CF", "01D0", "01D1", "01D9", "01DA", "01DB", "01DC", "01DD", "01DE", "01DF", "01E0", "01E1", "01E2", "01E3", "01E4", "01E5", "01E6", "01E7", "01E8", "01E9", "01EA", "01EB", "01EC", "01ED", "01EE", "01EF", "01F0", "01F1", "01F2", "01F4", "01F5", "01F6", "01F7", "01F8", "01F9", "01FA", "01FB", "01FC", "01FD", "01FE", "01FF", "0201", "0202", "0203", "0204", "0205", "0206", "0207", "0208", "0209", "020A", "020B", "020C", "020D", "020E", "020F", "0210", "0211", "0212", "0213", "0214", "0215", "0216", "0217", "0218", "0219", "036F", "0370", "0371", "0372", "0373", "0374", "0375", "0376", "0377", "0378", "0379", "037A", "037B", "037C", "037F", "0380", "0381", "0382", "0383", "0384", "0385" };
        public static int insert = -1;
        private static Item[] hiddenlist = (Item[])Application.Current.FindResource("itemsList");
        private static List<Item> listboxin = new List<Item>();
        private static List<Item> listboxout = new List<Item>();
        private static string inputFilterText = "";
        private static string outputFilterText = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Init_Boxes();
        }

        private void Populate_Boxes(List<string> items)
        {
            Item[] itemlist = new Item[listboxin.Count];
            listboxin.CopyTo(itemlist, 0);                     
            if (listboxout.Count + items.Count > 254)
            {
                Error_Message();
            }
            else
            {
                if (insert == 0)
                {
                    items.Reverse();
                    foreach (string item in items)
                    {
                        var result = itemlist.SingleOrDefault(x => x.Key.Substring(4) == item);
                        if (result != null)
                        {
                            listboxin.Remove(result);
                            listboxout.Insert(0, result);
                        }
                    }
                }
                else
                {
                    foreach (string item in items)
                    {
                        var result = itemlist.SingleOrDefault(x => x.Key.Substring(4) == item);
                        if (result != null)
                        {
                            listboxin.Remove(result);
                            listboxout.Add(result);
                        }
                    }
                }
                Refresh();
            }
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "shopList",
                DefaultExt = ".slt",
                Filter = "Shop List file | *.slt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
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
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };


            if (dlg.ShowDialog() == true)
            {
                if ((fs = dlg.OpenFile()) != null)
                {
                    byte[] header = new byte[] { 0x18, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                    byte[] buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                    List<byte> items = header.ToList();
                    foreach (Item item in listboxout)
                    {
                        string hex = item.Key.Substring(4);
                        items.Add(Convert.ToByte(int.Parse(hex.Substring(2), System.Globalization.NumberStyles.HexNumber)));
                        items.Add(Convert.ToByte(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)));
                        items.AddRange(buffer);
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
            Init_Boxes();
        }

        private void Default_Items(object sender, RoutedEventArgs e)
        {
            Populate_Boxes(DEFAULT_ITEMS.ToList());
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            List<Item> itemlist = new List<Item>();
            foreach (Item item in listboxout) {
                listboxin.Add(item);
                itemlist.Add(item);
            }
            foreach (Item item in itemlist)
            {
                listboxout.Remove(item);
            }
            Sort();
            Refresh();
        }

        private void All_Gems(object sender, RoutedEventArgs e)
        {
            Populate_Boxes(GEMS.ToList());
        }

        private void All_Consumables(object sender, RoutedEventArgs e)
        {
            Populate_Boxes(CONSUMABLES.ToList());
        }

        private void LR_Materials(object sender, RoutedEventArgs e)
        {
            Populate_Boxes(LR_MATERIALS.ToList());
        }

        private void HR_Materials(object sender, RoutedEventArgs e)
        {
            Populate_Boxes(HR_MATERIALS.ToList());
        }

        private void Init_Boxes()
        {
            listboxin.Clear();
            Item[] itemlist = new Item[hiddenlist.Count()];
            hiddenlist.CopyTo(itemlist, 0);
            foreach (Item item in itemlist)
            {
                listboxin.Add(item);
            }
            Refresh();
        }

        private void Sort()
        {
            listboxin.Sort((x, y) => x.Hex.CompareTo(y.Hex));
            Refresh();
        }

        private void Send_Out(object sender, RoutedEventArgs e)
        {            
            if (input.SelectedItems != null && input.SelectedItems.Count + listboxout.Count < 256)
            {
                Item[] selecteditems = new Item[input.SelectedItems.Count];
                input.SelectedItems.CopyTo(selecteditems, 0);
                List<Item> itemlist = new List<Item>();
                if (insert == 0)
                {
                    Array.Reverse(selecteditems);
                    foreach (Item item in selecteditems)
                    {
                        listboxout.Insert(0, item);
                        itemlist.Add(item);
                    }                      
                }
                else
                {
                    foreach (Item item in selecteditems)
                    {
                        listboxout.Add(item);
                        itemlist.Add(item);
                    }
                        
                }
                foreach (Item item in itemlist)
                {
                    listboxin.Remove(item);
                }
                Refresh();
            }
            else
            {
                Error_Message();
            }
        }

        private void Send_In(object sender, RoutedEventArgs e)
        {
            if (output.SelectedItems != null)
            {
                List<Item> itemlist = new List<Item>();
                foreach (Item item in output.SelectedItems)
                {
                    itemlist.Add(item);
                    listboxin.Add(item);
                }
                foreach (Item item in itemlist)
                {
                    listboxout.Remove(item);
                }
                Sort();
                Refresh();
            }
        }

        private void Insertion_Method(object sender, SelectionChangedEventArgs e)
        {
            switch(insert)
            {
                case 0:
                    insert = -1;
                    break;
                case -1:
                    insert = 0;
                    break;
                default:
                    break;
            }
        }

        private void Error_Message()
        {
            MessageBox.Show("Too many items in the output box! Can't load this preset.", "Error");
        }

        public ObservableCollection<Item> filteredInput
        {
            get
            {
                if (String.IsNullOrEmpty(inputFilterText)) 
                {
                    return new ObservableCollection<Item>(listboxin);
                }
                var filtered = listboxin.Where(x => x.Value.ToUpper().Contains(inputFilterText.ToUpper()));
                return new ObservableCollection<Item>(filtered);
            }
        }

        public string filterInputText
        {
            get { return inputFilterText;  }
            set
            {
                inputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredInput"));
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredOutput"));
                }
            }
        }

        public ObservableCollection<Item> filteredOutput
        {
            get
            {
                if (String.IsNullOrEmpty(outputFilterText))
                {
                    return new ObservableCollection<Item>(listboxout);
                }
                var filtered = listboxout.Where(x => x.Value.ToUpper().Contains(outputFilterText.ToUpper()));
                Console.WriteLine(filtered.ElementAtOrDefault(0));
                return new ObservableCollection<Item>(filtered);
            }
        }

        public string filterOutputText
        {
            get { return outputFilterText; }
            set
            {
                outputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredInput"));
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredOutput"));
                }
            }
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredInput"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
        }
    }

    public class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Hex { get { return Convert.ToInt32(this.Key.Substring(4), 16); } }
        public override string ToString()
        {
            return Value;
        }
    }
}
