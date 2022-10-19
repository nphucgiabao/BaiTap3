using System;
using System.Threading.Tasks;

namespace BaiTap3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var doThi = new DoThi();
            await doThi.DocFileAsync("../../../input.txt");
            doThi.Kruskal();

            Console.ReadKey();
        }
    }
}
