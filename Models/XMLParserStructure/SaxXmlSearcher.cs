using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;

namespace XMLParser
{
    class SaxXmlSearcher : IXmlSearcher
    {
        public string Name => "SAX";

        public IEnumerable<Filter> GetFilters()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Scientist> GetScientists(List<Filter> filters)
        {
            var scientists = new List<Scientist>();
            var xmlReader = new XmlTextReader(@"C:\Users\Surface\source\repos\XMLParser\ExampleData\1.xml");
            Scientist current = new Scientist();
            var prop = "";
            var check = true;
            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "Scientists": break;
                    case "Scientist":
                        if (xmlReader.NodeType == XmlNodeType.EndElement)
                        {
                            if(check)
                                scientists.Add(current);
                            current = new Scientist();
                            check = true;
                        }
                        break;
                    case "":
                        if (xmlReader.NodeType == XmlNodeType.Text)
                        {
                            switch (prop)
                            {
                                case "LastName":
                                    current.LastName = xmlReader.Value;
                                    break;
                                case "FirstName":
                                    current.FirstName = xmlReader.Value;
                                    break;
                                case "MiddleName":
                                    current.MiddleName = xmlReader.Value;
                                    break;
                                case "Faculty":
                                    current.Faculty = xmlReader.Value;
                                    break;
                                case "Cathedra":
                                    current.Cathedra = xmlReader.Value;
                                    break;
                                case "Position":
                                    current.Position = xmlReader.Value;
                                    break;
                                case "PositionSalary":
                                    current.PositionSalary = Convert.ToInt32(xmlReader.Value);
                                    break;
                                case "TimeOnPosition":
                                    current.TimeOnPosition = Convert.ToInt32(xmlReader.Value);
                                    break;
                            }
                            var filter = filters.Find(filter => filter.XmlProperty == prop);
                            var actualValue = xmlReader.Value;
                            var expectedValue = filter.SelectedValue;
                            if (actualValue != expectedValue && expectedValue is { })
                                check = false;
                        }
                        break;
                    default:
                        if(xmlReader.NodeType == XmlNodeType.Element)
                            prop = xmlReader.Name;
                        break;
                }
            }
            return scientists;
        }
        private static void parsingWithXmlTextReader()
        {

        }


    }
}
