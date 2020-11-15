using System;
using System.Collections.Generic;
using System.Text;

namespace XMLParser
{
    interface IXmlTransformer
    {
        void Transform(string inputPath, string outputPath);
    }
}
