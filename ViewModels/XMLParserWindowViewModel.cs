using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace XMLParser
{
    class XMLParserWindowViewModel : BaseViewModel
    {
        public ObservableCollection<Scientist> Scientists { get; set; }
        public IXmlSearcher SelectedXmlSearcher { get; set; }
        public ObservableCollection<IXmlSearcher> XmlSearchers { get; set; }
        public ObservableCollection<Filter> Filters { get; set; }

        public RelayCommand XmlToHtmlCommand { get; set; }
        public RelayCommand ChangeFilterCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ChooseMethodCommand { get; set; }

        public XMLParserWindowViewModel()
        {
            XmlSearchers = new ObservableCollection<IXmlSearcher>()
            {
                new LinqXmlSearcher(),
                new DomXmlSearcher(),
                new SaxXmlSearcher()
            };

            Scientists = new ObservableCollection<Scientist>()
            {
                new Scientist()
                {
                    FirstName = "Людмила",
                    MiddleName = "Леонідівна",
                    LastName = "Омельчук",
                    Faculty = "Комп'ютерних наук та кібернетики",
                    Cathedra = "Теорії та технології програмування",
                    Position = "доцент",
                    PositionSalary = 16890,
                    TimeOnPosition = 120
                }
            };

            Filters = new ObservableCollection<Filter>(XmlSearchers[0].GetFilters().ToList());

            SearchCommand = new RelayCommand(SearchScientists, o => true);
            ChooseMethodCommand = new RelayCommand(o =>
            {
                var method = (IXmlSearcher)o;
                SelectedXmlSearcher = method;
            },
            o => true
            );
        }

        private void SearchScientists(object obj)
        {
            Scientists = new ObservableCollection<Scientist>(SelectedXmlSearcher.GetScientists(Filters.ToList()).ToList());
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
        }
    }
}
