using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaughnApp.Models
{
    public class Port
    {
        public string PortName { get; set; }
        public string FriendlyName { get; set; }

        public override string ToString()
        {
            return $"{PortName} - {FriendlyName}";
        }
    }
}
