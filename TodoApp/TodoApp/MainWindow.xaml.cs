using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<TodoModel> _todoDataList;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _todoDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            
            dgTodoList.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;
        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }

                
            }
        }
    }
}

