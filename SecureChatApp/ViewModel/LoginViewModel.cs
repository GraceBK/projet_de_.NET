using SecureChatApp.View;
using System;
using System.Collections.Generic;
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
        private Frame frame;

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
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            this.frame = frame;
        }


        #region commande
        public ICommand LoginCommand { get; set; }

        public void Login()
        {
            if ((usernameTXT != null && passwordTXT != null) || (usernameTXT != "" && passwordTXT != ""))
            {
                Console.WriteLine("[LOGIN]: {0} {1}", usernameTXT, passwordTXT);

                PageChat viewPageChat = new PageChat(frame);
                frame.Navigate(viewPageChat);

                this.NotifyPropertyChanged("Login");
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
            if ((usernameTXT != null && passwordTXT != null) || (usernameTXT != "" && passwordTXT != ""))
            {
                Console.WriteLine("[REGISTER]: {0} {1}", usernameTXT, passwordTXT);

                PageChat viewPageChat = new PageChat(frame);
                frame.Navigate(viewPageChat);

                this.NotifyPropertyChanged("Register");
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
