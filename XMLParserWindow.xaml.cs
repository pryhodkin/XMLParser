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

namespace XMLParser
{
    /// <summary>
    /// Interaction logic for XMLParserWindow.xaml
    /// </summary>
    public partial class XMLParserWindow : Window
    {
        public XMLParserWindow()
        {
            DataContext = new XMLParserWindowViewModel();
            InitializeComponent();
            IXmlTransformer trans = new LinqToXmlTransformer();
            trans.Transform(@"C:\Users\Surface\source\repos\XMLParser\ExampleData\1.xml", @"C:\Users\Surface\source\repos\XMLParser\ExampleData\result.html");
        }
    }
}
