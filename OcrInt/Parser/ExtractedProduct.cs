using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class ExtractedProduct
    {
        public ProductType Type { get; set; }

        public double Probability { get; set; }

        public string Text { get; set; }
    }
}
