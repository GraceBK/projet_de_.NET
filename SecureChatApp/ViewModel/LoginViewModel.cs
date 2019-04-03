using SecureChatApp.Model;
using SecureChatApp.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecureChatApp.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {

        HttpClient client = new HttpClient();

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

                var table = (from i in db.Table<PersonneClass>() where i.Username == usernameTXT select i);
                

                foreach (var pers in table)
                {
                    
                    if (pers.Username == usernameTXT)
                    {
                        Console.WriteLine("[LOGIN]: {0} {1}", usernameTXT, passwordTXT);

                        

                        string json = "{ \"username\" : " + usernameTXT + ", \"password\" : " + passwordTXT + " }";

                        PageChat viewPageChat = new PageChat(frame);
                        frame.Navigate(viewPageChat);

                        this.NotifyPropertyChanged("Login");


                        WebRequest httpWebRequest = WebRequest.Create(new Uri("http://baobab.tokidev.fr/api/login"));
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = WebRequestMethods.Http.Post;

                        

                        string result = string.Empty;

                        byte[] postBytes = Encoding.UTF8.GetBytes(json);
                        /*
                        try
                        {
                            using (Stream stream = httpWebRequest.GetRequestStream())
                            {
                                stream.Write(postBytes, 0, postBytes.Length);
                            }

                            using (WebResponse response = httpWebRequest.GetResponse())
                            {
                                var sr = new StreamReader(response.GetResponseStream());
                                result = sr.ReadToEnd();
                                sr.Close();
                            }

                            WebResponse aaaaaa = httpWebRequest.GetResponse();


                            Console.WriteLine("[LOGIN]: {0} {1}", usernameTXT, aaaaaa);
                            if (result != null)
                            {
                                Console.WriteLine("[==]: {0} {1}", usernameTXT, result);
                            }


                        }
                        catch (WebException ex)
                        {

                            Console.WriteLine(ex.Message);
                        }*/

                        /*httpWebRequest.ContentLength = postBytes.Length;

                        Stream s = httpWebRequest.GetRequestStream();
                        s.Write(postBytes, 0, postBytes.Length);
                        s.Close();


                            
                        WebResponse response = httpWebRequest.GetResponse();

                        string ResponsesStatus = (((HttpWebResponse)response).StatusDescription);

                        s = response.GetResponseStream();
                        StreamReader reader = new StreamReader(s);
                        string responsesFromServer = reader.ReadToEnd();

                        Console.WriteLine("$$$$$$$$$ {0}", responsesFromServer);

                        PageChat viewPageChat = new PageChat(frame);
                        frame.Navigate(viewPageChat);

                        this.NotifyPropertyChanged("Login");

                        reader.Close();
                        s.Close();
                        response.Close();*/
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
                    Console.WriteLine("[REGISTER]: {0} {1}", usernameTXT, passwordTXT);

                    try
                    {
                        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://baobab.tokidev.fr/api/createUser");
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.Method = "POST";

                        using (var streamWrite = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = "{ \"username\" : "+ usernameTXT + ", \"password\" : "+ passwordTXT  + " }";

                            streamWrite.Write(json);
                            streamWrite.Flush();
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var responseText = streamReader.ReadToEnd();
                            Console.WriteLine(responseText);

                            if (!string.IsNullOrEmpty(responseText))
                            {
                                db.Insert(ROOT);

                                PageChat viewPageChat = new PageChat(frame);
                                frame.Navigate(viewPageChat);

                                this.NotifyPropertyChanged("Register");
                            }
                        }
                    } catch(WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    
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
