using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BaiTap3
{
    public class DoThi
    {
        public int soDinh { get; set; }
        public int[,] data { get; set; }
        public List<Canh> listCanh { get; set; }
        public int[] label { get; set; }
        public async Task DocFileAsync(string duongDan)
        {
            if (File.Exists(duongDan))
            {
                using (var stream = new StreamReader(duongDan))
                {
                    var content = await stream.ReadToEndAsync();
                    var lines = content.Split("\n");
                    this.soDinh = int.Parse(lines[0]);
                    this.label = new int[this.soDinh];
                    this.data = new int[this.soDinh, this.soDinh];
                    for (var i = 0; i < this.soDinh; ++i)
                    {
                        var values = lines[i + 1].Split(" ");
                        for (var j = 0; j < values.Length; ++j)
                        {
                            if (values != null)
                                this.data[i, j] = int.Parse(values[j]);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found!!!");
            }
        }
        public void KhoiTaoCacCanh()
        {
            for (var i = 0; i < this.label.Count(); i++)
                label[i] = i;
            listCanh = new List<Canh>();
            for (var i = 0; i < this.soDinh-1; i++)
            {
                for (var j = i + 1; j < this.soDinh; j++)
                {
                    if (this.data[i, j] > 0)
                    {
                        var canh = new Canh();
                        canh.dinhDau = i;
                        canh.dinhCuoi = j;
                        canh.trongSo = this.data[i, j];
                        listCanh.Add(canh);
                    }
                }
            }
        }
        public void Kruskal()
        {
            Console.WriteLine("Giai thuat Kruskal");

            var tree = new List<Canh>();
            KhoiTaoCacCanh();
            listCanh.Sort((x, y) => x.trongSo.CompareTo(y.trongSo));
            for(var i = 0; i<listCanh.Count(); i++)
            {
                if (!IsCircle(i))
                    tree.Add(listCanh[i]);
                if (tree.Count == soDinh - 1)
                    break;
            }
            Console.WriteLine("Tap canh cua cay khung nho nhat:");
            var trongSoCay = 0;
            foreach(var item in tree)
            {
                trongSoCay += item.trongSo;
                Console.WriteLine("{0} - {1}: {2}", item.dinhDau, item.dinhCuoi, item.trongSo);
            }
            Console.WriteLine("Trong so cua cay khung nho nhat: {0}", trongSoCay);
        }

        public bool IsCircle(int idx)
        {
            var canh = listCanh[idx];
            if (label[canh.dinhDau] == label[canh.dinhCuoi])
                return true;
            var lab1 = Math.Min(label[canh.dinhDau], label[canh.dinhCuoi]);
            var lab2 = Math.Max(label[canh.dinhDau], label[canh.dinhCuoi]);
            for(var i = 0; i < label.Count(); i++)
            {
                if (label[i] == lab2)
                    label[i] = lab1;
            }
            return false;
        }
    }
}
