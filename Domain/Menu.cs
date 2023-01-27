using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTools;
using System.Diagnostics;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI.Domain
{
    internal static class Menu
    {
        public static void Show()
        {
            var subConsultClientData = new ConsoleMenu(Array.Empty<string>(), level: 2)
                .Add("Consultar por nome", () => ConsultClientData.Execute(1))
                .Add("Consultar por CPF", () => ConsultClientData.Execute(2))
                .Add("Consultar por número da conta", () => ConsultClientData.Execute(3))
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Consultar dados do cliente";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subChangeClientData = new ConsoleMenu(Array.Empty<string>(), level: 2)
                .Add("Alterar nome", () => ChangeClientData.Execute(1))
                .Add("Alterar CPF", () =>ChangeClientData.Execute(2))
                .Add("Alterar telefone", () => ChangeClientData.Execute(3))
                .Add("Alterar endereço", () => ChangeClientData.Execute(4))
                //.Add("Alterar número de conta", () => ChangeClientData.Execute(5))
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Alterar cadastro do cliente";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar novo cliente", AddNewClient.Execute)
                .Add("Consultar dados do cliente", subConsultClientData.Show)
                .Add("Alterar cadastro do cliente", subChangeClientData.Show)
                .Add("Desativar/Ativar cadastro do cliente", DeactivateClientRegister.Execute)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Clientes";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => 
                        System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subConsultEmployeeData = new ConsoleMenu(Array.Empty<string>(), level: 2)
                .Add("Consultar por nome", () => ConsultEmployeeData.Execute(1))
                .Add("Consultar por CPF", () => ConsultEmployeeData.Execute(2))
                .Add("Consultar por usuário", () => ConsultEmployeeData.Execute(3))
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Consultar dados do funcionário";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subChangeEmployeeData = new ConsoleMenu(Array.Empty<string>(), level: 2)
                .Add("Alterar nome", () => ChangeEmployeeData.Execute(1))
                .Add("Alterar CPF", () => ChangeEmployeeData.Execute(2))
                .Add("Alterar usuário", () => ChangeEmployeeData.Execute(3))
                .Add("Alterar senha", () => ChangeEmployeeData.Execute(4))
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Alterar cadastro do Funcionário";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subEmployee = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Cadastrar novo funcionário", AddNewEmployee.Execute)
                .Add("Consultar dados do funcionário",() => subConsultEmployeeData.Show())
                .Add("Alterar cadastro do funcionário", () => subChangeEmployeeData.Show())
                .Add("Desativar/Ativar cadastro do funcionário", DeactivateEmployeeRegister.Execute)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Funcionários";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subTransactions = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Processar transações", ProcessTransactions.Execute)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Transações";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var subReports = new ConsoleMenu(Array.Empty<string>(), level: 1)
                .Add("Exibir clientes ativos e saldos", CheckActiveClients.Execute)
                .Add("Exibir clientes inativos", CheckInactiveClients.Execute)
                .Add("Exibir funcionários ativos e último login", CheckActiveEmployees.Execute)
                .Add("Exibir transações com erro", CheckFailedTransactions.Execute)
                .Add("Voltar", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Relatórios";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" / ", titles));
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            var menu = new ConsoleMenu(Array.Empty<string>(), level: 0)
                .Add("Clientes", subClient.Show)
                .Add("Funcionários", subEmployee.Show)
                .Add("Transações", subTransactions.Show)
                .Add("Relatórios", subReports.Show)
                .Add("Sair", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Menu principal";
                    config.EnableWriteTitle = false;
                    config.EnableBreadcrumb = true;
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkBlue;
                    config.SelectedItemForegroundColor = ConsoleColor.White;
                    config. WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                });

            menu.Show();
        }

         
    }
}
