using SecureChatApp.Model;
using SecureChatApp.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecureChatApp.ViewModel
{
    class AddViewModel: ViewModelBase
    {
        //ObservableCollection<PersonneClass> myCollection;
        private string dbPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "database\\grace.db3");
        SQLiteConnection db;

        private Frame frame;

        private string usernameTXT;

        #region proprietes
        public string InputNameContact
        {
            get => usernameTXT;
            set
            {
                if (usernameTXT != value)
                {
                    usernameTXT = value;
                    this.NotifyPropertyChanged("InputaddContact");
                }
            }
        }
        #endregion

        public AddViewModel(Frame frame/*ObservableCollection<PersonneClass> personnes*/)
        {
            //myCollection = new ObservableCollection<PersonneClass>();
            //myCollection = personnes;
            //DataContext = myCollection;

            db = new SQLiteConnection(dbPath);

            AddCommand = new RelayCommand(Add);
            BackCommand = new RelayCommand(Back);
            this.frame = frame;
        }

        #region commande
        public ICommand BackCommand { get; set; }

        public void Back()
        {
            Console.WriteLine("[BACK TO CHAT]: {0}", usernameTXT);

            PageChat viewPageChat = new PageChat(frame);
            frame.Navigate(viewPageChat);

            this.NotifyPropertyChanged("Back");
        }


        public ICommand AddCommand { get; set; }

        public void Add()
        {
            if (usernameTXT != null || usernameTXT != "")
            {
                Console.WriteLine("[ADD CONTACT]: {0}", usernameTXT);
                
                try
                {
                    db.Insert(new PersonneClass(usernameTXT));
                }
                catch (SQLite.SQLiteException e)
                {
                    Console.WriteLine("[EXIT] {0}", e);
                }


                PageChat viewPageChat = new PageChat(frame);
                frame.Navigate(viewPageChat);
                //InputNameContact = "";
                //Console.WriteLine("[ADD CONTACT]{0}", usernameTXT);


                this.NotifyPropertyChanged("Add");
            }
            else
            {
                MessageBox.Show("Veuillez Entrer le Username ...");
                Console.WriteLine("[ERROR ADD CONTACT]");
            }
        }
        #endregion
    }
}
