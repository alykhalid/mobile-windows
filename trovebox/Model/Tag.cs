using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trovebox.Model
{
    public class Tag
    {
        public Tag()
        {
            Id = string.Empty;
        }

        public string Id { get; set; }
        public int Count { get; set; }
    }
}
