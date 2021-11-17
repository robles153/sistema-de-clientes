using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sistema_de_Gestão__de_Clientes
{
    class Program
    {
        [System.Serializable] //Salvar dados dentro de arquivos
        struct Cliente
        {
            public string nome;
            public string email;
            public string CPF;
        }
        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem = 1, Adicionar, Remover, SAIR }
        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistemas de Clientes - BEM VINDO!");
                Console.WriteLine(" 1-Listagem\n 2-Adicionar\n 3-Remover\n 4-Sair");
                int intOPC = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOPC;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;

                    case Menu.Listagem:
                        Listagem();
                        break;

                    case Menu.Remover:
                        Remover();
                        break;

                    case Menu.SAIR:
                        escolheuSair = true;
                        break;

                }
                Console.Clear();

            }
        }
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Clientes: ");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cLIENTE: ");
            cliente.CPF = Console.ReadLine();

            clientes.Add(cliente); //Adicionando cliente a lista de cliente
            salvar();


            Console.WriteLine("Cadastro Concluido, Aperte ENTER para SAIR ");
            Console.ReadLine();

        }
        static void Listagem()
        {
            if (clientes.Count > 0) //exibir se tiver pelomenos um cliente 
            {
                Console.WriteLine("Lista de Clientes");

                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome} ");
                    Console.WriteLine($"Email: {cliente.email} ");
                    Console.WriteLine($"CPF: {cliente.CPF} ");
                    Console.WriteLine("---------------------------");
                    i++;
                }

            }
            else
            {
                Console.WriteLine("Nenhum Cliente cadastrado");
            }



            Console.WriteLine("Aperte ENTER para SAIR");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do Cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                salvar();

            }
            else
            {
                Console.WriteLine("ID Digitado é INVALIDO, Tente Novamente");
                Console.ReadLine();
            }
        }

        static void salvar()
        {
            FileStream stream = new FileStream("Cliet.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);
            stream.Close();

        }
        static void Carregar()
        {
            FileStream stream = new FileStream("Cliet.dat", FileMode.OpenOrCreate); //Ler arquivo
            try
            {

                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream); // ler lista de cçientes do arquivo e salvar na variavel cliente

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }   

            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();

            }
            stream.Close();

        }

    }
}