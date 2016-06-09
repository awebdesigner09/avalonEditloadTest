using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ICSharpCode.AvalonEdit.Rendering;

namespace TestAEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<string> allLines = File.ReadAllLines(@"C:\Users\Ratnasai\Downloads\Output_huge.txt").ToList();
            List<DiffLineViewModel> dLines = new List<DiffLineViewModel>();
            for (int i = 0; i < allLines.Count; i++)
            {
                DiffLineViewModel dLine = new DiffLineViewModel();
                dLine.LineNumber = Convert.ToString(i + 1);
                dLine.PrefixForStyle = "+";
                if (i % 2 == 0)
                    dLine.Style = DiffContext.Added;
                else
                    dLine.Style = DiffContext.Deleted;
                dLine.Text = allLines[i];
                dLines.Add(dLine);
            }
            left.TextArea.TextView.BackgroundRenderers.Add(new DiffLineBackgroundRenderer { Lines = dLines });
            left.Text = string.Join("\r\n", allLines);
            left.WordWrap = true;
            left.TextArea.TextView.ScrollOffsetChanged += TextView_ScrollOffsetChanged;
            right.TextArea.TextView.BackgroundRenderers.Add(new DiffLineBackgroundRenderer { Lines = dLines });
            right.Text = string.Join("\r\n", allLines);
            right.WordWrap = true;
            right.TextArea.TextView.ScrollOffsetChanged+=TextView_ScrollOffsetChanged;

        }

        Vector oldOffset;
        void TextView_ScrollOffsetChanged(object sender, EventArgs e)
        {
            TextView tv = sender as TextView;
            if (oldOffset != tv.ScrollOffset)
            {
                right.ScrollToVerticalOffset(tv.ScrollOffset.Y);
                left.ScrollToVerticalOffset(tv.ScrollOffset.Y);
                oldOffset = tv.ScrollOffset;
            }
        }
    }
}
