using System;
using System.IO;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Splatoon.Cheats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CommonOpenFileDialog dialog;

        public MainWindow()
        {
            InitializeComponent();

            dialog = new CommonOpenFileDialog();
            dialog.Title = "Select Your Cemu Directory";
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
        }

        #region Constants

        private static readonly string INT32_STRING = "int32";

        private readonly Entry NoHashEntry = new Entry("No Hash", "0x101e568c", INT32_STRING, 2058308820);
        private readonly Entry HighJumpEntry = new Entry("High Jump", "0x100e92e4", INT32_STRING, 1090519040);
        private readonly Entry BigInklingEntry = new Entry("Big Inkling", "0x100ec2b0", INT32_STRING, 1069547520);
        private readonly Entry SmallInklingEntry = new Entry("Small Inkling", "0x100ec2b0", INT32_STRING, 1056964608);
        private readonly Entry InvisibleInklingEntry = new Entry("Invisible Inkling", "0x100ec2b0", INT32_STRING, 805306368);
        private readonly Entry SpeedHackEntry = new Entry("Speed Hack", "0x100e8e40", INT32_STRING, 1058013184);
        private readonly Entry NoHUDEntry1 = new Entry("No HUD", "0x1014d778", INT32_STRING, 1207959552);
        private readonly Entry NoHUDEntry2 = new Entry("No HUD", "0x10152644", INT32_STRING, 1207959552);
        private readonly Entry NoHUDEntry3 = new Entry("No HUD", "0x101526e0", INT32_STRING, 1207959552);
        private readonly Entry DisableSpawnBarrierEntry = new Entry("Disable Spawn Barrier", "0x100d4f10", INT32_STRING, 0);
        private readonly Entry InstantSpecialEntry = new Entry("Instant Special", "0x100e8fa8", INT32_STRING, 3208642560);
        private readonly Entry GhostInklingEntry = new Entry("Ghost Inkling", "0x100068dc", INT32_STRING, 4026531840);

        private readonly Entry[] RevertAllCheatsEntry =
        {
            new Entry("Normal Jump", "0x100e92e4", INT32_STRING, 1066611507),
            new Entry("Normal Inkling", "0x100ec2b0", INT32_STRING, 1065353216),
            new Entry("Normal Inkling", "0x100068dc", INT32_STRING, 1058013184),
            new Entry("Normal Speed", "0x100e8e40", INT32_STRING, 1058013184),
            new Entry("Enable HUD", "0x1014d778", INT32_STRING, 1065353216),
            new Entry("Enable HUD", "0x10152644", INT32_STRING, 1065353216),
            new Entry("Enable HUD", "0x101526e0", INT32_STRING, 1065353216),
            new Entry("Enable Spawn Barrier", "0x100d4f10", INT32_STRING, 1112014848),
            new Entry("Normal Special", "0x100e8fa8", INT32_STRING, 1127219200),
        };

        private static readonly string EUR_TITLE_ID = "0005000010176a00";
        private static readonly string USA_TITLE_ID = "0005000010176900";
        private static readonly string JPN_TITLE_ID = "0005000010162b00";

        private static string CEMU_DIRECTORY;

        #endregion

        #region Main Functions

        private void GenerateFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullEntry = NoHashEntry.GetFullEntry();
                string titleId = string.Empty;

                if ((bool)USARadioButton.IsChecked)
                {
                    titleId = USA_TITLE_ID;
                }
                else if ((bool)EuropeRadioButton.IsChecked)
                {
                    titleId = EUR_TITLE_ID;
                }
                else if ((bool)JapanRadioButton.IsChecked)
                {
                    titleId = JPN_TITLE_ID;
                }

                if ((bool)HighJumpCheckBox.IsChecked)
                {
                    fullEntry += HighJumpEntry.GetFullEntry();
                }

                if ((bool)BigInklingCheckBox.IsChecked)
                {
                    fullEntry += BigInklingEntry.GetFullEntry();
                }

                if ((bool)SmallInklingCheckBox.IsChecked)
                {
                    fullEntry += SmallInklingEntry.GetFullEntry();
                }

                if ((bool)InvisibleInklingCheckBox.IsChecked)
                {
                    fullEntry += InvisibleInklingEntry.GetFullEntry();
                }

                if ((bool)SpeedHackCheckBox.IsChecked)
                {
                    fullEntry += SpeedHackEntry.GetFullEntry();
                }

                if ((bool)NoHUDCheckBox.IsChecked)
                {
                    fullEntry += NoHUDEntry1.GetFullEntry();
                    fullEntry += NoHUDEntry2.GetFullEntry();
                    fullEntry += NoHUDEntry3.GetFullEntry();
                }

                if ((bool)DisableSpawnBarrierCheckBox.IsChecked)
                {
                    fullEntry += DisableSpawnBarrierEntry.GetFullEntry();
                }

                if ((bool)InstantSpecialCheckBox.IsChecked)
                {
                    fullEntry += InstantSpecialEntry.GetFullEntry();
                }

                if ((bool)GhostInklingCheckBox.IsChecked)
                {
                    fullEntry += GhostInklingEntry.GetFullEntry();
                }

                CEMU_DIRECTORY = dialog.FileName;

                if (File.Exists($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini"))
                {
                    File.Delete($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini");
                }

                File.AppendAllText($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini", fullEntry);
                MessageBox.Show("Successfully created your cheat file, now just run Splatoon in Cemu and open the Memory Searcher to activate your cheats!", "Operation Successfull", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("The Cemu directory selected is incorrect, please select the directory where Cemu.exe is located!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dialog.ShowDialog();
            }
        }

        private void RevertAllCheatsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullEntry = NoHashEntry.GetFullEntry();
                string titleId = string.Empty;

                if ((bool)USARadioButton.IsChecked)
                {
                    titleId = USA_TITLE_ID;
                }
                else if ((bool)EuropeRadioButton.IsChecked)
                {
                    titleId = EUR_TITLE_ID;
                }
                else if ((bool)JapanRadioButton.IsChecked)
                {
                    titleId = JPN_TITLE_ID;
                }

                foreach (var entry in RevertAllCheatsEntry)
                {
                    fullEntry += entry.GetFullEntry();
                }

                CEMU_DIRECTORY = dialog.FileName;

                if (File.Exists($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini"))
                {
                    File.Delete($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini");
                }

                File.AppendAllText($@"{CEMU_DIRECTORY}\memorySearcher\{titleId}.ini", fullEntry);
                MessageBox.Show("Successfully created your cheat file, now just run Splatoon in Cemu and open the Memory Searcher to undo your cheats!", "Operation Successfull", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("The Cemu directory selected is incorrect, please select the directory where Cemu.exe is located!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dialog.ShowDialog();
            }
        }

        #endregion
    }
}
