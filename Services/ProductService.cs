using SmartInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Services
{
    public static class ProductService
    {
        public static readonly string[] Categories =
        {    "電子", "生活", "文具", "食品" };

        public static Dictionary<string, decimal[]> Statistics(List<Product> all)
        {
            //字典 <TKey,Tvalue>
            var stat = new Dictionary<string, decimal[]>();
            foreach (var p in all)
            {
                //檢查key是否存在 不存在 則需要初始化
                if (!stat.ContainsKey(p.Category))
                {
                    stat[p.Category] = new decimal[2];
                }


                    //數量
                    stat[p.Category][0] += p.Quantity;
                    //金額
                    stat[p.Category][1] += p.TotalValue;
                          
            }
            return stat;
        }
        public static (decimal,int) GetTotalValue(List<Product> all)
        {
            decimal total = 0;
            int qty =0;

            foreach (Product product in all)
            {
                total += product.TotalValue;
                qty += product.Quantity;
            }
            return (total ,qty);
        }
        public static  List<Product> GatLowStock(List<Product> all,int lowStock =10)
        {
            
            
            var result = new List<Product>();
            foreach (var p in all)
            {
                if (p.Quantity < lowStock)
                {
                    //Console.WriteLine($"{p.Name} {p.Quantity}");
                    result.Add(p);
                }
            }
            return result;
        }
        public static List<Product> Search(List<Product> all, string keyword, string category)
        {
            // 1. 判斷是否為空字串
            if (keyword == string.Empty && category == string.Empty) return all;


            // 2. 是否為搜尋關鍵字
            // 3.是否是搜尋分類
            var result = new List<Product>();
            foreach (var p in all)
            {
                if (!p.Name.Contains(keyword)) continue;
                
                if(category == "全部"|| p.Category.Contains(category))
                {
                    result.Add(p);
                }

               



            }
            // 4.回應
            return result;
        }
        // public static TotalValue
    }
}
