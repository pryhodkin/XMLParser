using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLParser
{
    class LinqToXmlTransformer : IXmlTransformer
    {
        public void Transform(string inputPath, string outputPath)
        {
            XmlDocument input = new XmlDocument();
            input.Load(inputPath);

            var xInput = input.ToXDocument();

            var xOutPut = new XDocument
            (
   new XElement("html",

    new XElement("head",
     new XElement("Title",
                  "Кадры науковців (зарплата)"
                 ),
     new XElement("style",
                   "td" +
                   "{"  +
                   "    border: 0;" +
                   "    background: #35f4a0;" +
                   "    font-family: sans-serif;" +
                   "    padding: 20px;" +
                   "    text-align: center;"   +
                   "}"
                 )
                ),
    
    new XElement("body",
     new XElement("table",
      new XElement("tr",
       new XElement("td", "Прізвище"),
       new XElement("td", "Ім'я"),
       new XElement("td", "По-батькові"),
       new XElement("td", "Факультет"),
       new XElement("td", "Кафедра"),
       new XElement("td", "Посада"),
       new XElement("td", "Посадовий оклад"),
       new XElement("td", "Час на посаді")
                  ),

      xInput.Element("Scientists").Elements("Scientist")
       .Select(item =>
      new XElement("tr",
       new XElement("td", item.Element("LastName").Value),
       new XElement("td", item.Element("FirstName").Value),
       new XElement("td", item.Element("MiddleName").Value),
       new XElement("td", item.Element("Faculty").Value),
       new XElement("td", item.Element("Cathedra").Value),
       new XElement("td", item.Element("Position").Value),
       new XElement("td", item.Element("PositionSalary").Value),
       new XElement("td", item.Element("TimeOnPosition").Value + " місяців")
                  )
                ))
                )
               )
            );

            var outPut = xOutPut.ToXmlDocument();
            xOutPut.Save(outputPath);
        }
    }
}
