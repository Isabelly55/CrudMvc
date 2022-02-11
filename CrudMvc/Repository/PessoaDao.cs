using CrudMvc.Utils;
using CrudMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrudMvc.Repository
{
    public class PessoaDao
    {
        // private string caminho = "C:\\Users\\isabe\\OneDrive\\Documentos\\projetos\\CrudMvc\\CrudMvc\\Models\\Banco.xml";
        private readonly string caminho = Path.GetTempPath() + "Banco.xml";
        private static PessoaDao instance;
        List<Pessoa> lista;

        public static PessoaDao getInstance()
        {
            if (instance == null)
            {
                instance = new PessoaDao();
            }

            return instance;
        }

        private static int GetIndex(List<Pessoa> lista, Pessoa pessoa)
        {
            return lista.FindIndex(c => c.Id == pessoa.Id);
        }

        #region CRUD

        public void Cadastrar(Pessoa pessoa)
        {
            lista = GetAll();
            pessoa.Id = AutoIncrement(lista);
            lista.Add(pessoa);
            Persistir(lista);
        }
        public void Alterar(Pessoa pessoa)
        {
            lista = GetAll();
            lista[GetIndex(lista, pessoa)] = pessoa;
            Persistir(lista);
        }
        public void Excluir(Pessoa pessoa)
        {
            lista = GetAll();
            lista.RemoveAt(GetIndex(lista, pessoa));
            Persistir(lista);
        }
        public List<Pessoa> GetAll()
        {
            lista = new List<Pessoa>();
            if (File.Exists(caminho))
            {
                String recuperado = ArquivoUtil.RecuperarArquivo(caminho);
                lista = (List<Pessoa>)XmlUtil.Deserialize(typeof(List<Pessoa>), caminho);
            }
            return lista;
        }
        public Pessoa BuscarPessoa(int id)
        {
            Pessoa pessoa = new Pessoa();
            var filtrado = GetAll().Where(x => x.Id == id).ToList();

            foreach (var pes in filtrado)
            {
                pessoa.Id = pes.Id;
                pessoa.Nome = pes.Nome;
                pessoa.Telefone = pes.Telefone;
                pessoa.Email = pes.Email;
            }
            return pessoa;
        }
        #endregion

        public int AutoIncrement(List<Pessoa> lista)
        {
            int numero = lista.Count + 1;
            var filtrado = lista.Where(x => x.Id == numero).ToList();

            foreach (var p in filtrado)
            {
                if (numero == p.Id)
                    numero++;
            }
            return numero;
        }
        public void Persistir(List<Pessoa> lista)
        {
            String xml = XmlUtil.Serializer(lista);
            ArquivoUtil.GravarArquivo(caminho, xml);
        }


    }
}
