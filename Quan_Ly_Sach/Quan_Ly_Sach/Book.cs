using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sach
{
    internal class Book
    {
        private int id_book;
        private string name_book;
        private string author;
        private double price;

        public Book() { }
        public Book(int id_book, string name_book, string author, double price)
        {
            this.Id_book = id_book;
            this.Name_book = name_book;
            this.Author = author;
            this.Price = price;
        }

        public int Id_book { get => id_book; set => id_book = value; }
        public string Name_book { get => name_book; set => name_book = value; }
        public string Author { get => author; set => author = value; }
        public double Price { get => price; set => price = value; }

       
        public void Output()
        {
            Console.WriteLine("Ma sach: " +  this.id_book +" ,Ten sach: " + this.name_book + " ,Ten tac gia: " + this.author + " ,Gia tien: " + this.price);
        }
    }
}
