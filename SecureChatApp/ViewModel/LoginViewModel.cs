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
    public class LoginViewModel: ViewModelBase
    {
        private string dbPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "database\\grace.db3");// "db/grace.db3";
        SQLiteConnection db;

        private Frame frame;

        private static PersonneClass ROOT;

        private string usernameTXT;
        private string passwordTXT;

        #region proprietes
        public string InputLoginUsername
        {
            get => usernameTXT;
            set
            {
                if (usernameTXT != value)
                {
                    usernameTXT = value;
                    this.NotifyPropertyChanged("InputUsername");
                }
            }
        }
        public string InputLoginPassword
        {
            get => passwordTXT;
            set
            {
                if (passwordTXT != value)
                {
                    passwordTXT = value;
                    this.NotifyPropertyChanged("InputPassword");
                }
            }
        }
        #endregion

        public LoginViewModel(Frame frame)
        {

            #region Instanciation_de_la_connexion
            Console.WriteLine(dbPath);
            db = new SQLiteConnection(dbPath);
            #endregion
            db.CreateTable<PersonneClass>();
            db.CreateTable<MessageClass>();

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            this.frame = frame;
        }


        #region commande
        public ICommand LoginCommand { get; set; }

        public void Login()
        {
            if ((usernameTXT != null && passwordTXT != null)/* || (usernameTXT != "" && passwordTXT != "")*/)
            {
                Console.WriteLine("[LOGIN]: {0} {1}", usernameTXT, passwordTXT);

                var table = (from i in db.Table<PersonneClass>() where i.Username == usernameTXT select i);

                foreach (var pers in table)
                {
                    if (pers.Username == usernameTXT)
                    {
                        PageChat viewPageChat = new PageChat(frame);
                        frame.Navigate(viewPageChat);

                        this.NotifyPropertyChanged("Login");
                    }
                    else
                    {
                        MessageBox.Show("User no found ...");
                        Console.WriteLine("[ERROR LOGIN]");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez Entrer le Username ...");
                Console.WriteLine("[ERROR LOGIN]");
            }
        }

        public ICommand RegisterCommand { get; set; }

        public void Register()
        {
            if ((usernameTXT != null && passwordTXT != null)/* || (usernameTXT != "" && passwordTXT != "")*/)
            {
                ROOT = new PersonneClass(usernameTXT, passwordTXT);

                try
                {
                    db.Insert(ROOT);
                    Console.WriteLine("[REGISTER]: {0} {1}", usernameTXT, passwordTXT);

                    PageChat viewPageChat = new PageChat(frame);
                    frame.Navigate(viewPageChat);

                    this.NotifyPropertyChanged("Register");
                }
                catch (SQLite.SQLiteException e)
                {
                    Console.WriteLine("[EXIT] {0}", e);
                }
                
            }
            else
            {
                MessageBox.Show("Veuillez Entrer le Username ...");
                Console.WriteLine("[ERROR REGISTER]");
            }
        }
        #endregion
    }
}
