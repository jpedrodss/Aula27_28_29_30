using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto : IProduto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        private const string PATH = "Database/produto.csv";
        
        public Produto(){
            // Cria o arquivo caso não exista
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }
        /// <summary>
        /// Reescreve uma linha dentro da lista 
        /// </summary>
        /// <param name="linhas">linha que contém informações do produto</param>
        public void ReescreverCSV(List<string> linhas){
            using (StreamWriter output = new StreamWriter(PATH))
            {
                foreach (string ln in linhas)
                {
                    output.Write(ln + "\n");
                }
            }
        }
        public string Separar(string dado){
            return dado.Split('=')[1];
        }
        /// <summary>
        /// Cadastra um produto
        /// </summary>
        /// <param name="prod">Produto</param>
        public void Cadastrar(Produto prod){
            var linha = new string[] {PrepararLinha(prod)};
            File.AppendAllLines(PATH, linha);
        }
        /// <summary>
        /// Lê o csv
        /// </summary>
        /// <returns>Lista do csv</returns>
        public List<Produto> Ler(){
            List<Produto> produtos = new List<Produto>();

            string[] linhas = File.ReadAllLines(PATH);

            foreach (var linha in linhas){
                string [] dados = linha.Split(';');

                Produto prod = new Produto();
                prod.Codigo  = Int32.Parse(Separar(dados[0]));
                prod.Nome    = Separar(dados[1]);
                prod.Preco   = float.Parse( Separar(dados[2]));

                produtos.Add(prod);
            }
            
            produtos = produtos.OrderBy(z => z.Nome).ToList();

            return produtos;
        }
        /// <summary>
        /// Procura um nome, codigo ou preço específico
        /// </summary>
        /// <param name="_nome">Nome digitado para pesquisa</param>
        /// <returns>Lista de resultados encontrados</returns>
        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll(n => n.Nome == _nome);
        }
        /// <summary>
        /// Remove um produto da lista
        /// </summary>
        /// <param name="_termo">Nome, codigo, ou preço para remover o produto</param>
        public void Remover(string _termo){

            List<string> linhas = new List<string>();
            
            using(StreamReader arquivo = new StreamReader(PATH)){

                string linha;

                while((linha = arquivo.ReadLine()) != null ){
                    linhas.Add(linha);
                }

                linhas.RemoveAll(z => z.Contains(_termo));
            }

            ReescreverCSV(linhas);

        }
        /// <summary>
        /// Altera um produto
        /// </summary>
        /// <param name="_produtoAlterado">Objeto de produto</param>
        public void Alterar(Produto _produtoAlterado){
            List<string> linhas = new List<string>();

            using (StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while ((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            linhas.RemoveAll(z => z.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));

            linhas.Add(PrepararLinha(_produtoAlterado));

            ReescreverCSV(linhas);

        }
        private string PrepararLinha(Produto p){
            return $"Codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

    }
}