using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

using SecureChatApp.Model;

namespace SecureChatApp.View
{
    /// <summary>
    /// Logique d'interaction pour WindowAddPersonne.xaml
    /// </summary>
    public partial class WindowAddPersonne : Window
    {
        //ObservableCollection<PersonneClass> myCollection;

        public WindowAddPersonne(/*ObservableCollection<PersonneClass> collection*/)
        {
            InitializeComponent();
            //this.myCollection = collection;
            //this.DataContext = myCollection;
        }
        
        /*
        #region Methodes
        public void ButtonCreatePersonne_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.myCollection.Count);
            //this.myCollection.Add(new PersonneClass(MyUsername.Text));
            this.Close();
        }
        #endregion*/
    }
}
