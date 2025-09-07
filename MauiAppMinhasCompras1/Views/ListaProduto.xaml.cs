using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras1.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
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

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    //mostrar valor total dos produtos
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);

            string msg = $"O total � {soma:C}";
            DisplayAlert("Total dos podutos", msg, "OK");
        }
        catch (Exception ex)
        {

            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    //clicked para remover item (li��o)
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Remover", $"Deseja remover o item {p.Descricao}?", "Sim", "N�o");
            //confirma��o da deleta��o e remo��o da observablecollection//
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
}