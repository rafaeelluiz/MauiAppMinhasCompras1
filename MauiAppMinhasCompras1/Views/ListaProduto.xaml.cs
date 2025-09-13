using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras1.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    private object minhaCollectionView;

    public IEnumerable<object> Produtos { get; private set; }

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }




    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());


        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
    //limpar lista
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {


            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }finally
{           lst_produtos.IsRefreshing = false;
        }
    }

    //mostrar valor total dos produtos
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            var produtosExibidos = lst_produtos.ItemsSource as IEnumerable<Produto>;
            double soma = produtosExibidos?.Sum(i => i.Total) ?? 0;

            string msg = $"O total é {soma:C}";
            DisplayAlert("Total dos podutos", msg, "OK");
        }
        catch (Exception ex)
        {

            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    //clicked para remover item (lição)
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Remover", $"Deseja remover o item {p.Descricao}?", "Sim", "Não");
            //confirmação da deletação e remoção da observablecollection//
            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
    //editar produto 
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            //seleciona o item como produto 
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }

        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            pckFiltroCategoria.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void pckFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        var categoriaSelecionada = pckFiltroCategoria.SelectedItem?.ToString();
        if (categoriaSelecionada == "Todos")
        {
            lst_produtos.ItemsSource = lista; // Mostra todos os itens
        }
        else if (!string.IsNullOrEmpty(categoriaSelecionada))
        {
            var filtrado = lista.Where(p => p.Categoria == categoriaSelecionada).ToList();
            lst_produtos.ItemsSource = filtrado;
        }
        pckFiltroCategoria.IsVisible = false; // Esconde o Picker após selecionar
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}
