using MhwKtDroprateEditor.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MhwKtDroprateEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Droprate BrownDroprate;
        public Droprate SilverDroprate;
        public Droprate GoldDroprate;

        public MainWindow()
        {
            InitializeComponent();
            PresetDroprate("Default.json");
        }

        private void LoadDefault(object sender, RoutedEventArgs e) 
            => PresetDroprate("Default.json");

        private void LoadHex(object sender, RoutedEventArgs e) 
            => PresetDroprate("Hex.json");

        private void PresetDroprate(string file)
        {
            var asm = Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream($"MhwKtDroprateEditor.Presets.{file}"))
            using (var reader = new StreamReader(stream))
            {
                var jsonFile = reader.ReadToEnd();
                var droprates = JsonConvert.DeserializeObject<Droprate[]>(jsonFile);

                BrownDroprate = droprates.FirstOrDefault(d => d.Type == WeaponType.Dissolved);
                SilverDroprate = droprates.FirstOrDefault(d => d.Type == WeaponType.Melded);
                GoldDroprate = droprates.FirstOrDefault(d => d.Type == WeaponType.Sublimated);
            }
            Populate_Boxes(BrownDroprate, SilverDroprate, GoldDroprate);
        }

        private void Populate_Boxes(Droprate brownDroprate, Droprate silverDroprate, Droprate goldDroprate)
        {
            BrownR6Pre.Value = brownDroprate.R6GoldPrefix / 100;
            BrownR6Post.Value = brownDroprate.R6GoldPostfix / 100;
            BrownR7.Value = brownDroprate.R7 / 100;
            BrownR8.Value = brownDroprate.R8 / 100;
            BrownTotal.Text = $"{brownDroprate.R6GoldPrefix + brownDroprate.R6GoldPostfix + brownDroprate.R7 + brownDroprate.R8}%";

            SilverR6Pre.Value = silverDroprate.R6GoldPrefix / 100;
            SilverR6Post.Value = silverDroprate.R6GoldPostfix / 100;
            SilverR7.Value = silverDroprate.R7 / 100;
            SilverR8.Value = silverDroprate.R8 / 100;
            SilverTotal.Text = $"{silverDroprate.R6GoldPrefix + silverDroprate.R6GoldPostfix + silverDroprate.R7 + silverDroprate.R8}%";

            GoldR6Pre.Value = goldDroprate.R6GoldPrefix / 100;
            GoldR6Post.Value = goldDroprate.R6GoldPostfix / 100;
            GoldR7.Value = goldDroprate.R7 / 100;
            GoldR8.Value = goldDroprate.R8 / 100;
            GoldTotal.Text = $"{goldDroprate.R6GoldPrefix + goldDroprate.R6GoldPostfix + goldDroprate.R7 + goldDroprate.R8}%";

            //TODO: Add over 100% error
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "em117_grade_lot",
                DefaultExt = ".em117glt",
                Filter = "KL Droprate table | *.em117glt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            if (dlg.ShowDialog() == true)
            {
                var filename = dlg.FileName;
                var input = System.IO.File.ReadAllBytes(filename);
                var buffer = new byte[1];
                for (var i = 6; i < input.Length - 1; i += 24)
                {
                    buffer[0] = input[i + 8];
                    var r6Pre = BitConverter.ToInt32(input, i + 8);
                    var r6Post = BitConverter.ToInt32(input, i + 12);
                    var r7 = BitConverter.ToInt32(input, i + 16);
                    var r8 = BitConverter.ToInt32(input, i + 20);
                    if (i == 6)
                        BrownDroprate = new Droprate(WeaponType.Dissolved, r6Pre, r6Post, r7, r8);
                    else if (i == 30)
                        SilverDroprate = new Droprate(WeaponType.Melded, r6Pre, r6Post, r7, r8);
                    else if (i == 54)
                        GoldDroprate = new Droprate(WeaponType.Sublimated, r6Pre, r6Post, r7, r8);
                }

                Populate_Boxes(BrownDroprate, SilverDroprate, GoldDroprate);
            }
        }

        private async void SaveFile(object sender, RoutedEventArgs e)
        {
            System.IO.Stream fs;
            var dlg = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = "em117_grade_lot",
                DefaultExt = ".em117glt",
                Filter = "KL Droprate table | *.em117glt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };


            if (dlg.ShowDialog() == true)
            {
                if ((fs = dlg.OpenFile()) != null)
                {
                    //var header = new byte[] { 0x18, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    //var buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    //var items = header.ToList();
                    //foreach (var item in listboxout)
                    //{
                    //    var hex = item.Key.Substring(4);
                    //    items.Add(Convert.ToByte(int.Parse(hex.Substring(2), System.Globalization.NumberStyles.HexNumber)));
                    //    items.Add(Convert.ToByte(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)));
                    //    items.AddRange(buffer);
                    //}
                    //var output = items.ToArray();
                    //await fs.WriteAsync(output, 0, output.Length);
                    //fs.Close();
                }
            }
        }

        private void Default_Button(object sender, RoutedEventArgs e)
        {
            var input = System.IO.File.ReadAllText("./Preset/Default.json");
            Set(JsonConvert.DeserializeObject<Droprate>(input));
        }

        private void Hex_Button(object sender, RoutedEventArgs e)
        {
            var input = System.IO.File.ReadAllText("./Preset/Hex.json");
            Set(JsonConvert.DeserializeObject<Droprate>(input));
        }

        private void Set(Droprate droprate) =>
            //var itemlist = new List<Item>();
            //foreach (var item in listboxout)
            //{
            //    listboxin.Add(item);
            //    itemlist.Add(item);
            //}
            //foreach (var item in itemlist)
            //{
            //    listboxout.Remove(item);
            //}
            //Sort();
            Refresh();

        private void LargerThan100(string item) => MessageBox.Show($"{item} is larger than 100%", "Error");

        public void Refresh()
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredInput"));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
        }

        private void Input_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


    }

    public class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Hex => Convert.ToInt32(Key.Substring(4), 16);
        public override string ToString() => Value;
    }
}
