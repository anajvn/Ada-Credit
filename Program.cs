using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.UI.Data;
using AdaCredit.UI.Domain;
using AdaCredit.UI.Testing;

namespace AdaCredit.UI
{
    internal class Program
    {
        static void Main(string[] args)
        
        {
            Visual.Apply();
            Visual.Intro();

            // Logar usuario

            Login.Show();

            CreateClients.Execute();
            CreateTransactions.Execute();

            // Mostrar menu

            Menu.Show();
        }
    }
}