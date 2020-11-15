using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace XMLParser
{
    class LinqXmlSearcher : IXmlSearcher
    {
        public string Name => "LInQ";

        public IEnumerable<Filter> GetFilters()
        {
            var data = XDocument.Load(@"C:\Users\Surface\source\repos\XMLParser\ExampleData\1.xml");
            var filters = new List<Filter>();
            foreach (var scientist in data.Element("Scientists").Elements("Scientist"))
            {
                foreach (var prop in scientist.Elements())
                {
                    var xmlProp = prop.Name.ToString();

                    if (filters.All(filter => filter.XmlProperty != xmlProp))
                    {
                        var viewProp = prop.Attribute("ukr").Value;
                        var values = new List<string>();
                        var filter = new Filter() { ViewProperty = viewProp, Values = values, SelectedValue = null, XmlProperty = xmlProp };
                        filters.Add(filter);
                    }
                    var index = filters.FindIndex(filter => filter.XmlProperty == xmlProp);
                    var check = filters[index].Values.Contains(prop.Value);
                    if (!check)
                        filters[index].Values.Add(prop.Value);
                }
            }
            return filters;
        }

        public IEnumerable<Scientist> GetScientists(List<Filter> filters)
        {
            var data = XDocument.Load(@"C:\Users\Surface\source\repos\XMLParser\ExampleData\1.xml");
            return data.Element("Scientists")
                        .Elements("Scientist")
                        .Where(item =>
                       {
                           foreach (var filter in filters)
                           {
                               var actualValue = item.Element(filter.XmlProperty)?.Value;
                               var expectedValue = filter.SelectedValue;
                               if (actualValue != expectedValue && expectedValue is { })
                                   return false;
                           }
                           return true;
                       }
                        )
                        .Select(item =>
                        {
                            return new Scientist()
                            {
                                LastName = item.Element("LastName").Value,
                                FirstName = item.Element("FirstName").Value,
                                MiddleName = item.Element("MiddleName").Value,
                                Faculty = item.Element("Faculty").Value,
                                Cathedra = item.Element("Cathedra").Value,
                                Position = item.Element("LastName").Value,
                                PositionSalary = Convert.ToInt32(item.Element("PositionSalary").Value),
                                TimeOnPosition = Convert.ToInt32(item.Element("TimeOnPosition").Value)
                            };
                        }
                        );
        }
    }
}
