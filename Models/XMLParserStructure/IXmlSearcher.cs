using System;
using System.Collections.Generic;
using System.Text;

namespace XMLParser
{
    interface IXmlSearcher
    {
        string Name { get; }
        IEnumerable<Scientist> GetScientists(List<Filter> filters);
        IEnumerable<Filter> GetFilters();
    }
}
