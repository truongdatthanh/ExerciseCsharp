using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quan_Ly_Sach
{
    internal class Program
    {
        static void Main()
        {
            List<Book> books = new List<Book>();
            bool exit = false;
            Validated vl = new Validated();
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("----MENU----");
                Console.WriteLine("1. Them 1 cuon sach.");
                Console.WriteLine("2. Xoa 1 cuon sach.");
                Console.WriteLine("3. Thay doi thong tin 1 cuon sach.");
                Console.WriteLine("4. Xuat thong tin tat ca cuon sach.");
                Console.WriteLine("5. Tim cuon sach co chua tu 'Lap trinh'.");
                Console.WriteLine("6. Tim kiem sach theo gia tien.");
                Console.WriteLine("7. Tim sach theo tac gia.");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("----Them 1 cuon sach-----------------------------------------------");
                        AddBook(books);
                        //InPut(books);
                        break;
                    case "2":
                        Console.WriteLine("---------------Xoa 1 cuon sach.------------------------------------");
                        ShowList(books);
                        int id_remove = vl.GetInt("----Nhap ma sach can xoa: ", "====>Ma sach khong duoc chu ki tu 'Chu'", 0, int.MaxValue);
                        RemoveBook(books, id_remove);
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("---------Thay doi thong tin sach-----------------------------------");
                        ShowList(books);
                        int id_replace = vl.GetInt("----Nhap MA SO cua sach de thay doi thong tin: ", "====>Ma so khong hop le!", 0, int.MaxValue);
                        ReplaceInfoBook(books, id_replace);
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine("----Xuat thong tin tat ca cac cuon sach---------------------------");
                        if (books.Count() == 0)
                        {
                            Console.WriteLine("====>Danh sach rong");
                        }
                        else
                        {
                            ShowList(books);
                        }
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine("----Cuon sach co chua tu 'Lap trinh'------------------------------");
                        Console.Write("Tim kiem sach bang ten sach: ");
                        string find_book = Console.ReadLine();
                        FindBookByName(books, find_book);
                        break;
                    case "6":
                        Console.WriteLine("----Tim kiem sach theo gia tien-----------------------------------");
                        double find_book_price = vl.GetDouble("Ban can tim sach khoang (VND): ", "So tien khong duoc la so am", 0, double.MaxValue);
                        int k = vl.GetInt("Moi ban nhap so luong sach can tim: ", "====>So luong sach ban nhap khong hop le!", 1, books.Count());
                        FindBookByPriceAndQuantity(books, k, find_book_price);
                        break;
                    case "7":
                        Console.WriteLine("-----Tim kiem sach theo tac gia-----------------------------------");
                        Console.Write("Nhap danh sach ten tac gia (cach nhau boi dau phay): ");
                        string inputAuthors = Console.ReadLine();
                        string[] authors = inputAuthors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();//Cat mang tu dau ',', trim: xoa khoang trang
                        SearchBooksByAuthors(books, authors);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Ket thuc chuong trinh");
                        break;
                    default:
                        Console.WriteLine("Tuy chon sai. Vui long chon lai");
                        break;
                }
            }
        }
        static void AddBook(List<Book> books)
        {
            Console.WriteLine("----Nhap thong tin sach");
            //Book b = new Book();
            //b.Input();
            //books.Add(b);
            books.Add(new Book(12, "ngon ngu lAp TriNh", "dat", 12000f));
            books.Add(new Book(13, "lap trinh huong doi tuong", "dat", 10000f));
            books.Add(new Book(14, "triet hoc Mac-LeNin", "truong", 14000f));
            books.Add(new Book(15, "triet hoc Mac-LeNin", "truong", 16000f));
            books.Add(new Book(16, "Duong len dinh Olympia", "truong", 18000f));
            books.Add(new Book(22, "COng nghe thong tin", "phuoc", 120000f));
            books.Add(new Book(17, "Cong nghe O To", "nhan", 12000f));
            books.Add(new Book(19, "LEague Of Legend", "phuoc", 22000f));
            books.Add(new Book(20, "Valorant", "phuc", 26000f));
            books.Add(new Book(21, "Toan Ly hoa", "dat", 30000f));
            Console.WriteLine("====>Them thanh cong");
        }
        static void InPut(List<Book> books)
        {
            Validated vl = new Validated();
            int id_book = vl.CheckID("Nhap ma sach: ", books);
            Console.Write("----Nhap ten sach: ");
            string name_book = Console.ReadLine();
            Console.Write("----Nhap ten tac gia: ");
            string author = Console.ReadLine();
            double price = vl.GetDouble("----Nhap gia tien: ", " Gia tien khong duoc la so am!", 0, double.MaxValue);
            books.Add(new Book(id_book, name_book, author, price));
        }
        static void RemoveBook(List<Book> books, int id)
        {
            var result = books.Where(b => b.Id_book == id).ToList();
            ShowList(result);
            if (result.Count() == 0)
            {
                Console.WriteLine("====>Khong tim thay sach");
            }
            else
            {
                for (int i = 0; i < books.Count(); i++)
                {
                    if (result.Contains(books[i]))
                    {
                        books.RemoveAt(i);
                        Console.WriteLine("====>Xoa thanh cong");
                    }
                }
            }

        }
        static void ReplaceInfoBook(List<Book> books, int id_r)
        {
            Validated vl = new Validated();
            var result = books.Where(b => b.Id_book == id_r).ToList();
            if (result.Count() == 0)
            {
                Console.WriteLine("====>Khong tim thay sach");
            }
            else
            {
                foreach (var item in result)
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Ban muon thay doi thong tin gi cua sach?");
                        Console.WriteLine("1. Tac gia");
                        Console.WriteLine("2. Ten sach");
                        Console.WriteLine("3. Gia tien");
                        Console.WriteLine("4. Thoat");
                        Console.Write("----Lua chon: ");
                        int choice = int.Parse(Console.ReadLine());
                        if (choice < 0 || choice > 4)
                        {
                            Console.WriteLine("Tuy chon khong hop le. Vui long nhap lai.");
                        }
                        else if (choice == 1)
                        {
                            Console.Write("----Sua 'Ten Tac Gia' thanh: ");
                            string author_r = Console.ReadLine();
                            item.Author = author_r;
                            Console.WriteLine("====>Sua thanh cong");
                        }
                        else if (choice == 2)
                        {
                            Console.Write("----Sua 'Ten Sach' thanh: ");
                            string name_book_r = Console.ReadLine();
                            item.Name_book = name_book_r;
                            Console.WriteLine("====>Sua thanh cong");
                        }
                        else if (choice == 3)
                        {
                            double price_r = vl.GetDouble("----Sua 'Gia Tien' thanh:", "====>So tien khong duoc la so am", 0, double.MaxValue);
                            item.Price = price_r;
                            Console.WriteLine("====>Sua thanh cong");
                        }
                        else
                        {
                            Console.WriteLine("====>Hoan tat chinh sua!!");
                            break;
                        }
                    }
                }
            }
        }
        static void FindBookByName(List<Book> books, string find)
        {
            string f = find.ToUpperInvariant();

            Console.WriteLine(f);
            var result = books.Where(b => b.Name_book.ToUpperInvariant().Contains(f)).ToList();
            if (result.Count() == 0)
            {
                Console.WriteLine("====>Khong tim thay sach ma ban can tim!");
            }
            else
            {
                Console.WriteLine("====>Sach ban can tim");
                ShowList(result);
            }
        }
        static void FindBookByPriceAndQuantity(List<Book> books, int quantity, double price_f)
        {
            var result = books.Where(b => b.Price <= price_f).OrderByDescending(b => b.Price).ToArray();
            if (result.Count() == 0)
            {
                Console.WriteLine("====>Khong co loai sach ban can tim");
            }
            else if (result.Count() != quantity)
            {
                Console.WriteLine("====>So luong sach khong du");
            }
            else
            {
                for (int i = 0; i < quantity; i++)
                {
                    Book book = result[i];
                    book.Output();
                }
            }
        }
        static void FindBookByAuthor(List<Book> books)
        {
            var result = books.GroupBy(b => b.Author).ToArray();
            for (int i = 0; i < result.Length; i++)
            {

            }
        }
        static void SearchBooksByAuthors(List<Book> books, string[] authors)
        {
            Console.WriteLine("Cac cuon sach cua cac tac gia:");
            foreach (string author in authors)
            {
                Console.WriteLine($"Tac gia: {author}");
                var authorBooks = books.Where(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
                bool flag = false;
                foreach (var book in authorBooks)
                {
                    book.Output();
                    flag = true;
                }
                if (flag==false)
                {
                    Console.WriteLine($"Khong co ten tac gia: {author}");
                }
                Console.WriteLine();
            }
        }
        static void ShowList(List<Book> books)
        {
            foreach (var book in books)
            {
                book.Output();
            }
        }
    }
}
