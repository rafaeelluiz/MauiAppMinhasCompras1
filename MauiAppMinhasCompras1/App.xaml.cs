// importa helpers
using MauiAppMinhasCompras1.Helpers;

namespace MauiAppMinhasCompras1;

    // define app principal
    public partial class App : Application
    {
        
        static SQLiteDataBaseHelper _db;
        // cria db estático
        public static SQLiteDataBaseHelper Db
        {    // acessa db
            get
            {
                if (_db == null)
                // verifica db
                {   
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");
                    // define caminho
                    _db = new SQLiteDataBaseHelper(path);
                }
                return _db;

            }

        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
            // define página
        }


    }
