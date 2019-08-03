using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Pandemonium.Classes;

namespace Pandemonium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<CheckBox, bool> _selectionLookup;

        public MainWindow()
        {
            InitializeComponent();

            _selectionLookup = new Dictionary<CheckBox, bool>();

            G_OptionSelect.Children.Cast<CheckBox>().ToList().ForEach(cb => cb.Click += CB_Click);

            CB_SwapMouse.Tag = new ActionSwapMouseButtons();
            CB_ToggleDesktopIcons.Tag = new ActionToggleDesktopIcons();
        }

        private void CB_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var lookupContains = _selectionLookup.ContainsKey(checkBox);

            if (!lookupContains)
            {
                _selectionLookup.Add(checkBox, (bool)checkBox.IsChecked);
            }
            else
            {
                _selectionLookup[checkBox] = (bool)checkBox.IsChecked;
            }
        }

        private void B_Chaos_Click(object sender, RoutedEventArgs e)
        {
            var selectedElements = _selectionLookup.Where(x => x.Value).ToList();
            var actualElements = new List<CheckBox>();
            var tasks = new List<Task>();

            selectedElements.ForEach(kvp => actualElements.Add(kvp.Key));

            foreach (var element in actualElements)
            {
                var tag = (ActionBase)element.Tag;
                var task = tag.DoChaos;

                task.Start();
                tasks.Add(task);
            }

            tasks.ForEach(t => t.Wait());
        }
    }
}
