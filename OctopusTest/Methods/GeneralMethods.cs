using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest.Methods
{
    class GeneralMethods
    {
        public static string GetRandomString(int length)
{
    var r = new Random();
    return new String(Enumerable.Range(0, length).Select(n => (Char)(r.Next(32, 127))).ToArray());
}
    }
}
