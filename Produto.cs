using System.IO;
namespace Aula27_28_29_30
{
    public class Produto
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

        public void Cadastrar(Produto prod){
            var linha = new string[] {PrepararLinha(prod)};
            File.AppendAllLines(PATH, linha);
        }

        private string PrepararLinha(Produto p){
            return $"Codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }
    }
}