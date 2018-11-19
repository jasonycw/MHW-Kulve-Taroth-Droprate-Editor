using MhwKtDroprateEditor.Annotations;
using MhwKtDroprateEditor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace MhwKtDroprateEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Rendered = false;

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
            Rendered = true;
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
            Render(BrownDroprate, BrownR6Pre, BrownR6Post, BrownR7, BrownR8, BrownTotal);
            Render(SilverDroprate, SilverR6Pre, SilverR6Post, SilverR7, SilverR8, SilverTotal);
            Render(GoldDroprate, GoldR6Pre, GoldR6Post, GoldR7, GoldR8, GoldTotal);
        }

        private void Render(Droprate droprate, DecimalUpDown r6Pre, DecimalUpDown r6Post, DecimalUpDown r7, DecimalUpDown r8, TextBlock total)
        {
            r6Pre.Value = droprate.R6GoldPrefix;
            r6Post.Value = droprate.R6GoldPostfix;
            r7.Value = droprate.R7;
            r8.Value = droprate.R8;

            RenderTotal(droprate, total);
        }

        private void RenderTotal(Droprate droprate, TextBlock total)
        {
            total.Text = $"{droprate.TotalPercentage}%";
            total.Foreground = !droprate.Valid ? Brushes.Red : Brushes.Black;
        }

        private void LargerThan100(string item) => MessageBox.Show($"{item} probability is larger than 100%", "Error");

        private void LoadDefault(object sender, RoutedEventArgs e) => PresetDroprate("Default.json");

        private void LoadHex(object sender, RoutedEventArgs e) => PresetDroprate("Hex.json");

        private void TextBoxChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!Rendered) return;
            if (((FrameworkElement)e.Source).Name == "BrownR6Pre") BrownDroprate.R6GoldPrefix = BrownR6Pre?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR6Post") BrownDroprate.R6GoldPostfix = BrownR6Post?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR7") BrownDroprate.R7 = BrownR7?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "BrownR8") BrownDroprate.R8 = BrownR8?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR6Pre") SilverDroprate.R6GoldPrefix = SilverR6Pre?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR6Post") SilverDroprate.R6GoldPostfix = SilverR6Post?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR7") SilverDroprate.R7 = SilverR7?.Value ?? 0;
            if (((FrameworkElement)e.Source).Name == "SilverR8") SilverDroprate.R8 = SilverR8?.Value ?? 0;
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
                var input = File.ReadAllBytes(filename);
                var buffer = new byte[1];
                for (var i = 6; i < input.Length - 1; i += 24)
                {
                    buffer[0] = input[i + 8];
                    var r6Pre = (decimal) (BitConverter.ToInt32(input, i + 8) / 100.0);
                    var r6Post = (decimal)(BitConverter.ToInt32(input, i + 12) / 100.0);
                    var r7 = (decimal)(BitConverter.ToInt32(input, i + 16) / 100.0);
                    var r8 = (decimal)(BitConverter.ToInt32(input, i + 20) / 100.0);
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
            if (!BrownDroprate.Valid)
            {
                LargerThan100(BrownDroprate.Type.ToString());
                return;
            }
            if (!SilverDroprate.Valid)
            {
                LargerThan100(SilverDroprate.Type.ToString());
                return;
            }
            if (!GoldDroprate.Valid)
            {
                LargerThan100(GoldDroprate.Type.ToString());
                return;
            }

            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "em117_grade_lot",
                DefaultExt = ".em117glt",
                Filter = "KL Droprate table | *.em117glt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };


            if (dlg.ShowDialog() == true)
            {
                using (var fs = dlg.OpenFile())
                {
                    var buffer = new List<byte> { 0x1F, 0x00, 0x03, 0x00, 0x00, 0x00 };
                    var brownHeader = new List<byte> { 0x00, 0x00, 0x00, 0x00, 0xA8, 0x03, 0x00, 0x00 };
                    var silverHeader = new List<byte> { 0x01, 0x00, 0x00, 0x00, 0xA9, 0x03, 0x00, 0x00 };
                    var goldHeader = new List<byte> { 0x02, 0x00, 0x00, 0x00, 0xAA, 0x03, 0x00, 0x00 };

                    buffer.AddRange(brownHeader);
                    buffer.AddRange(BrownDroprate.ToByte);
                    buffer.AddRange(silverHeader);
                    buffer.AddRange(SilverDroprate.ToByte);
                    buffer.AddRange(goldHeader);
                    buffer.AddRange(GoldDroprate.ToByte);

                    var output = buffer.ToArray();
                    await fs.WriteAsync(output, 0, output.Length);
                    fs.Close();
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
