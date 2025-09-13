using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        //verificar se foi digitado algo
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    //mensagem de erro
                    throw new Exception("Por favor, preencha a descrição");
                }
                //insere na descição
                _descricao = value;
            }
        }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total { get => Quantidade * Preco; }
        public string Categoria { get; set; }
    }
}