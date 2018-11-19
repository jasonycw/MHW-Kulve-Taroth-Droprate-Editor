using MhwKtDroprateEditor.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace MhwKtDroprateEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool rendered = false;

        public Droprate BrownDroprate;
        public Droprate SilverDroprate;
        public Droprate GoldDroprate;

        private void InitializeProperties()
        {
            BrownDroprate = new Droprate(WeaponType.Dissolved);
            SilverDroprate = new Droprate(WeaponType.Melded);
            GoldDroprate = new Droprate(WeaponType.Sublimated);
        }

        public MainWindow()
        {
            InitializeProperties();
            InitializeComponent();
            rendered = true;
            PresetDroprate("Default.json");
        }

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
            Render();
        }

        private void Render()
        {
            BrownR6Pre.Value = BrownDroprate.R6GoldPrefix;
            BrownR6Post.Value = BrownDroprate.R6GoldPostfix;
            BrownR7.Value = BrownDroprate.R7;
            BrownR8.Value = BrownDroprate.R8;
            BrownTotal.Text = $"{(BrownDroprate.R6GoldPrefix + BrownDroprate.R6GoldPostfix + BrownDroprate.R7 + BrownDroprate.R8) * 100}%";

            SilverR6Pre.Value = SilverDroprate.R6GoldPrefix;
            SilverR6Post.Value = SilverDroprate.R6GoldPostfix;
            SilverR7.Value = SilverDroprate.R7;
            SilverR8.Value = SilverDroprate.R8;
            SilverTotal.Text = $"{(SilverDroprate.R6GoldPrefix + SilverDroprate.R6GoldPostfix + SilverDroprate.R7 + SilverDroprate.R8) * 100}%";

            GoldR6Pre.Value = GoldDroprate.R6GoldPrefix;
            GoldR6Post.Value = GoldDroprate.R6GoldPostfix;
            GoldR7.Value = GoldDroprate.R7;
            GoldR8.Value = GoldDroprate.R8;
            GoldTotal.Text = $"{(GoldDroprate.R6GoldPrefix + GoldDroprate.R6GoldPostfix + GoldDroprate.R7 + GoldDroprate.R8) * 100}%";

            //TODO: Add over 100% error
        }

        private void LargerThan100(string item) => MessageBox.Show($"{item} is larger than 100%", "Error");

        private void LoadDefault(object sender, RoutedEventArgs e) => PresetDroprate("Default.json");

        private void LoadHex(object sender, RoutedEventArgs e) => PresetDroprate("Hex.json");

        private void BrownChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!rendered) return;
            if (((FrameworkElement)e.Source).Name == "BrownR6Pre") BrownDroprate.R6GoldPrefix = BrownR6Pre?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR6Post") BrownDroprate.R6GoldPostfix = BrownR6Post?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR7") BrownDroprate.R7 = BrownR7?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR8") BrownDroprate.R8 = BrownR8?.Value ?? 0;
            Render();
        }

        private void SilverChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!rendered) return;
            if (((FrameworkElement)e.Source).Name == "SilverR6Pre") SilverDroprate.R6GoldPrefix = SilverR6Pre?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR6Post") SilverDroprate.R6GoldPostfix = SilverR6Post?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR7") SilverDroprate.R7 = SilverR7?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR8") SilverDroprate.R8 = SilverR8?.Value ?? 0;
            Render();
        }

        private void GoldChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!rendered) return;
            if (((FrameworkElement)e.Source).Name == "GoldR6Pre") GoldDroprate.R6GoldPrefix = GoldR6Pre?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "GoldR6Post") GoldDroprate.R6GoldPostfix = GoldR6Post?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "GoldR7") GoldDroprate.R7 = GoldR7?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "GoldR8") GoldDroprate.R8 = GoldR8?.Value ?? 0;
            Render();
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

                Render();
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
    }
}
