using MauiAppMinhasCompras1.Models;
using SQLite;

namespace MauiAppMinhasCompras1.Helpers
{
    public class SQLiteDataBaseHelper
    {   
        // conexão banco
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDataBaseHelper(string path)
        {   
            // abre conexão
            _conn = new SQLiteAsyncConnection(path);
            // cria tabela
            _conn.CreateTableAsync<Produto>().Wait();
        }
        // inserir produto
        public Task<int> Insert(Produto p) 
        {
            // insere registro
            return _conn.InsertAsync(p);
        }

        // atualizar produto
        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            // executa update
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }
        // deletar produto
        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        // pesquisar produto
        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            // comando select
            string sql = "SELECT * FROM Produto WHERE descricao like '%" + q + "%'";
            // executa select
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
