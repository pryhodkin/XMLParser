using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;

namespace XMLParser
{
    [AddINotifyPropertyChangedInterface]
    class Filter : BaseViewModel
    {
        public string ViewProperty { get; set; }
        public string XmlProperty { get; set; }
        public List<string> Values { get; set; }
        public string SelectedValue { get; set; }
    }
}
