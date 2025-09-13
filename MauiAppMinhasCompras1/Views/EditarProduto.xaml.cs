using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras1.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();

		// Seleciona a categoria salva no produto, se houver
		this.Appearing += (s, e) =>
		{
			if (BindingContext is Produto produto && !string.IsNullOrEmpty(produto.Categoria))
			{
				pck_categoria.SelectedItem = produto.Categoria;
			}
		};
	}
    //adição da alteração do produto 
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            //puxa o produto do bindingcontext
            Produto produto_anexado = BindingContext as Produto;
            
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = pck_categoria.SelectedItem?.ToString() ?? produto_anexado.Categoria
            };
            //faz o update do produto e dispara mensagem de ok 
            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Registro alterado com sucesso!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}
