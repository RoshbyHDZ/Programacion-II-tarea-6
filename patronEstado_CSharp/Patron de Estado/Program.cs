using System;
using System.Collections.Generic;
namespace Programacion_II_patrones_de_diseño
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("Roshby Hernandez");

            account.Deposit(500.0);
            account.Deposit(300.0);
            account.Deposit(550.0);
            account.PayInterest();
            account.Withdraw(2000.00);
            account.Withdraw(1100.00);

            Console.ReadKey();
        }
    }

    abstract class State

    {
        protected Account account;
        protected double balance;

        protected double interest;
        protected double lowerLimit;
        protected double upperLimit;


        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void PayInterest();
    }


    class Rojo : State

    {
        private double _serviceFee;


        public Rojo(State state)
        {
            this.balance = state.Balance;
            this.account = state.Account;
            Initialize();
        }

        private void Initialize()
        {

            interest = 0.0;
            lowerLimit = -100.0;
            upperLimit = 0.0;
            _serviceFee = 15.00;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            amount = amount - _serviceFee;
            Console.WriteLine("No hay fondos disponibles para retirar");
        }

        public override void PayInterest()
        {

        }

        private void StateChangeCheck()
        {
            if (balance > upperLimit)
            {
                account.State = new Plata(this);
            }
        }
    }

    class Plata : State

    {
        public Plata(State state) :
          this(state.Balance, state.Account)
        {
        }

        public Plata(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            Initialize();
        }

        private void Initialize()
        {

            interest = 0.0;
            lowerLimit = 0.0;
            upperLimit = 1000.0;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            balance += interest * balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (balance < lowerLimit)
            {
                account.State = new Rojo(this);
            }
            else if (balance > upperLimit)
            {
                account.State = new Dorado(this);
            }
        }
    }


    class Dorado : State

    {

        public Dorado(State state)
          : this(state.Balance, state.Account)
        {
        }

        public Dorado(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            Initialize();
        }

        private void Initialize()
        {

            interest = 0.05;
            lowerLimit = 1000.0;
            upperLimit = 10000000.0;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            balance += interest * balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (balance < 0.0)
            {
                account.State = new Rojo(this);
            }
            else if (balance < lowerLimit)
            {
                account.State = new Plata(this);
            }
        }
    }

    class Account

    {
        private State _state;
        private string _owner;

        public Account(string owner)
        {

            this._owner = owner;
            this._state = new Plata(0.0, this);
        }

        public double Balance
        {
            get { return _state.Balance; }
        }

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Deposit(double amount)
        {
            _state.Deposit(amount);
            Console.WriteLine("Depositado {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Estado = {0}",
              this.State.GetType().Name);
            Console.WriteLine("");
        }

        public void Withdraw(double amount)
        {
            _state.Withdraw(amount);
            Console.WriteLine("Se retiro {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Estado = {0}\n",
              this.State.GetType().Name);
        }

        public void PayInterest()
        {
            _state.PayInterest();
            Console.WriteLine("Pago de interes --- ");
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Estado = {0}\n",
              this.State.GetType().Name);
        }
    }
}