using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParser
{
    class DomXmlSearcher : IXmlSearcher
    {
        public string Name => "DOM";

        public IEnumerable<Filter> GetFilters()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Scientist> GetScientists(List<Filter> filters)
        {
            List<Scientist> scientists = new List<Scientist>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\Surface\source\repos\XMLParser\ExampleData\1.xml");
            var scientistsNode = xmlDoc.DocumentElement.ChildNodes[1].SelectNodes("//Scientists/Scientist");
            foreach (XmlNode node in scientistsNode)
            {
                bool check = true;
                foreach (var filter in filters)
                {
                    var prop = node.SelectSingleNode($"./{filter.XmlProperty}");
                    var expectedValue = filter.SelectedValue;
                    var actualValue = prop.InnerText;
                    if (actualValue != expectedValue && expectedValue is { })
                        check = false;
                }
                if (check)
                {
                    var scientist = new Scientist()
                    {
                        LastName = node.SelectSingleNode("./LastName").InnerText,
                        FirstName = node.SelectSingleNode("./FirstName").InnerText,
                        MiddleName = node.SelectSingleNode("./MiddleName").InnerText,
                        Faculty = node.SelectSingleNode("./Faculty").InnerText,
                        Cathedra = node.SelectSingleNode("./Cathedra").InnerText,
                        Position = node.SelectSingleNode("./LastName").InnerText,
                        PositionSalary = Convert.ToInt32(node.SelectSingleNode("./PositionSalary").InnerText),
                        TimeOnPosition = Convert.ToInt32(node.SelectSingleNode("./TimeOnPosition").InnerText)
                    };
                    scientists.Add(scientist);
                }
            }
            return scientists;

        }


        private static void parsingXmlDocument(List<Filter> filters)
        {
            
        }
        private static void RecurseNodes(XmlNode node)
        {
            var sb = new StringBuilder();
            //починаємо рекурсивний перегляд з рівня 0
            RecurseNodes(node, 0, sb);
            //друкуємо сформований рядок
            Console.WriteLine(sb.ToString());
        }

        private static void RecurseNodes(XmlNode node, int level, StringBuilder sb)
        {
            sb.AppendFormat("{0,-2} Type:{1,-9} Name:{2,-13} Attr:",
            level, node.NodeType, node.Name);
            foreach (XmlAttribute attr in node.Attributes)
            {
                sb.AppendFormat("{0}={1} ", attr.Name, attr.Value);
            }
            sb.AppendLine();
            foreach (XmlNode n in node.ChildNodes)
            {
                RecurseNodes(n, level + 1, sb);
            }
        }


    }
}
