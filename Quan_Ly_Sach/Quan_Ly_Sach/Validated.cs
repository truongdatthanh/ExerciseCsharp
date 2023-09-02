using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sach
{
    internal class Validated
    {
        public int GetInt(string mgs, string error, int lowerBound, int upBound)
        {
            int Number = 0;
            while (true)
            {
                try
                {
                    Console.Write(mgs);
                    Number = int.Parse(Console.ReadLine());
                    if (Number < lowerBound || Number > upBound)
                    {
                        throw new Exception();
                    }
                    return Number;
                }
                catch
                {
                    Console.WriteLine(error);
                }
            }
        }
        public double GetDouble(string mgs, string error, double lowerBound, double upBound)
        {
            double Number = 0;
            while (true)
            {

                try
                {
                    Console.Write(mgs);
                    Number = double.Parse(Console.ReadLine());
                    if (Number < lowerBound || Number > upBound)
                    {
                        throw new Exception();
                    }
                    return Number;
                }
                catch
                {
                    Console.WriteLine(error);
                }
            }
        }
        public bool Duplicated(List<Book> books, int id)
        {
            foreach(Book book in books)
            {
                if (book.Id_book == id)
                {
                    return false;
                }
            }
            return true;
        }
        public int CheckID(string mgs, List<Book> books)
        {
           
            while (true)
            {
                int ms = GetInt(mgs, "====>Ma so ban nhap khong hop le", 0, int.MaxValue);
                if(Duplicated(books, ms)==false)
                {
                    Console.WriteLine("====>Ma so bi trung. Moi nhap lai!");
                    continue;
                }
                return ms;
            }
        }
    }
}
