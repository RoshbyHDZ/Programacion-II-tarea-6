using System;
using System.Collections.Generic;

namespace Programacion_II_patrones_de_diseño
{
    class Program
    {
        static void Main(string[] args)
        {

            Book book = new Book("Tite kubo", "Bleach", 10);
            book.Display();

            Video video = new Video("Akira Toriyama", "Dragon Ball Z", 23, 92);
            video.Display();

            Console.WriteLine("\nvideo prestado:");

            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Cliente 1");
            borrowvideo.BorrowItem("Cliente 2");

            borrowvideo.Display();

            Console.ReadKey();
        }
    }

    abstract class LibraryItem

    {
        private int _numCopies;

        public int NumCopies
        {
            get { return _numCopies; }
            set { _numCopies = value; }
        }

        public abstract void Display();
    }


    class Book : LibraryItem

    {
        private string _author;
        private string _title;

        public Book(string author, string title, int numCopies)
        {
            this._author = author;
            this._title = title;
            this.NumCopies = numCopies;
        }

        public override void Display()
        {
            Console.WriteLine("\nLibro ------ ");
            Console.WriteLine(" Autor: {0}", _author);
            Console.WriteLine(" Titulo: {0}", _title);
            Console.WriteLine(" # Copias: {0}", NumCopies);
        }
    }

    class Video : LibraryItem

    {
        private string _director;
        private string _title;
        private int _playTime;

        public Video(string director, string title,
          int numCopies, int playTime)
        {
            this._director = director;
            this._title = title;
            this.NumCopies = numCopies;
            this._playTime = playTime;
        }

        public override void Display()
        {
            Console.WriteLine("\nVideo ----- ");
            Console.WriteLine(" Director: {0}", _director);
            Console.WriteLine(" Titulo: {0}", _title);
            Console.WriteLine(" # Copias: {0}", NumCopies);
            Console.WriteLine(" Tiempo de Duracion: {0}\n", _playTime);
        }
    }


    abstract class Decorator : LibraryItem

    {
        protected LibraryItem libraryItem;

        public Decorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            libraryItem.Display();
        }
    }

    class Borrowable : Decorator

    {
        protected List<string> borrowers = new List<string>();

        public Borrowable(LibraryItem libraryItem)
          : base(libraryItem)
        {
        }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();

            foreach (string borrower in borrowers)
            {
                Console.WriteLine(" Nombre del prestatario: " + borrower);
            }
        }
    }
}