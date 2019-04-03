using SecureChatApp.Model;
using SecureChatApp.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecureChatApp.ViewModel
{
    public class ChatViewModel: ViewModelBase
    {

        private string dbPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "database\\grace.db3");// "db/grace.db3";
        SQLiteConnection db;



        private Page page;

        private Frame frame;

        private static PersonneClass ROOT;

        private ObservableCollection<PersonneClass> collectionPersonnes;
        private PersonneClass selectedPersonne;
        private string edtMsgText;


        #region Proprietes
        public PersonneClass SelectPersonne
        {
            get => selectedPersonne;
            set
            {
                if (selectedPersonne != value)
                {
                    selectedPersonne = value;
                    Console.WriteLine("==========> {0}", selectedPersonne.Username);
                    this.NotifyPropertyChanged("SelectPersonne");
                    this.NotifyPropertyChanged("CollectionMsgByPersonne");
                }
            }
        }


        public ObservableCollection<PersonneClass> CollectionPers
        {
            get => collectionPersonnes;
            set
            {
                if (collectionPersonnes != value)
                {
                    collectionPersonnes = value;
                    this.NotifyPropertyChanged("CollectionPers");
                }
            }
        }

        public string InputMsg
        {
            get => edtMsgText;
            set
            {
                if (edtMsgText != value)
                {
                    edtMsgText = value;
                    this.NotifyPropertyChanged("InputMsg");
                }
            }
        }

        public ObservableCollection<MessageClass> CollectionMsg
        {
            get => MessageSingleton.NewInstance.GetSetMessages;
            set
            {
                if (MessageSingleton.NewInstance.GetSetMessages != value)
                {
                    MessageSingleton.NewInstance.GetSetMessages = value;
                    this.NotifyPropertyChanged("CollectionMsg");
                }
            }
        }

        public ObservableCollection<MessageClass> CollectionMsgByPersonne
        {
            get
            {
                if (selectedPersonne != null)
                {
                    
                    return ROOT.MessagesByUser(selectedPersonne.Username);
                }
                return null;
            }
        }

        public Page Page
        {
            get => page;
            set
            {
                if (Equals(page, value))
                {
                    return;
                }

                this.page = value;
                this.NotifyPropertyChanged("Page");
            }
        }
        #endregion


        public ChatViewModel(Frame frame)
        {
            #region Instanciation_de_la_connexion
            Console.WriteLine(dbPath);
            db = new SQLiteConnection(dbPath);
            #endregion
            db.CreateTable<PersonneClass>();
            db.CreateTable<MessageClass>();

            var rootQuery = (from i in db.Table<PersonneClass>() where i.Root != "nope" select i);

            foreach (var pers in rootQuery)
            {
                if (pers.Root != "nope")
                {
                    ROOT = new PersonneClass(pers.Username);
                }
            }
            
            var table = (from i in db.Table<PersonneClass>() where i.Root == "nope" select i);
            var tableMsg = (from i in db.Table<MessageClass>() select i);

            collectionPersonnes = new ObservableCollection<PersonneClass>();

            foreach (var pers in table) {
                collectionPersonnes.Add(new PersonneClass(pers.Username));

                foreach (var msg in tableMsg)
                {
                    CollectionMsg.Add(new MessageClass(msg.Msg, ROOT.ID, pers.ID));
                }
            }

            var innerJoinQuery =
                from sms in db.Table<MessageClass>()
                join per in db.Table<PersonneClass>() on sms.To equals per.ID
                select new { PersonneClass = per.ID, MessageClass = sms.To };

            foreach (var _it in innerJoinQuery)
            {
                Console.WriteLine("==========> {0}", _it.PersonneClass);
            }

            Console.WriteLine("====§§§§=> {0}", innerJoinQuery);




            /*collectionPersonnes = new ObservableCollection<PersonneClass>();

            PersonneClass pers1 = new PersonneClass("Grace 1");
            PersonneClass pers2 = new PersonneClass("Grace 2");

            collectionPersonnes.Add(pers1);
            collectionPersonnes.Add(pers2);

            //CollectionMsg.Add(new MessageClass("Coucou toi", p.ID, pers2.ID));
            //CollectionMsg.Add(new MessageClass("Coucou ca va?", pers2.ID, p.ID));


            try
            {
                db.InsertAll(collectionPersonnes);
            }
            catch (SQLite.SQLiteException e)
            {
                Console.WriteLine("[ERROR INSERT] {0}", e);
            }

            try
            {
                db.InsertAll(CollectionMsg);
            }
            catch (SQLite.SQLiteException e)
            {
                Console.WriteLine("[ERROR INSERT] {0}", e);
            }*/

            /*foreach(var per in collectionPersonnes)
            {
                db.InsertAll()
            }*/


            AddPersonneCommand = new RelayCommand(AddPersonne);
            RemovePersonneCommand = new RelayCommand(RemovePersonne);
            SendMessageCommand = new RelayCommand(SendMsg);

            this.frame = frame;
        }


        #region Mes_Commandes
        public ICommand AddPersonneCommand { get; set; }

        public void AddPersonne()
        {
            AddPersonne viewPageAdd = new AddPersonne(frame);
            frame.Navigate(viewPageAdd);

            this.NotifyPropertyChanged("AddContact");
            /*WindowAddPersonne windowAdd = new WindowAddPersonne();
            windowAdd.Show();*/
        }

        public ICommand RemovePersonneCommand { get; set; }

        public void RemovePersonne()
        {
            if (selectedPersonne != null)
            {
                Console.WriteLine("[DELETE] user: {0}", SelectPersonne.Username);
                CollectionPers.Remove(SelectPersonne);
            }
        }

        public ICommand SendMessageCommand { get; set; }

        public void SendMsg()
        {
            if (selectedPersonne != null)
            {
                MessageClass newMessage;

                var rootQuery = (from i in db.Table<PersonneClass>() where i.Root != "nope" select i);

                foreach (var pers in rootQuery)
                {
                    if (pers.Root != "nope")
                    {
                        newMessage = new MessageClass(edtMsgText, pers.Username, selectedPersonne.Username);
                        //ROOT = new PersonneClass(pers.Username);
                        CollectionMsg.Add(newMessage);


                        #region add message
                        //db.Insert
                        try
                        {
                            db.Insert(newMessage);
                        }
                        catch
                        { }
                        #endregion
                    }
                }

                
                
                Console.WriteLine("[ADD MESSAGE] from: {0} to: {1} message: {2}", 1, SelectPersonne.Username, edtMsgText);
                InputMsg = "";
                this.NotifyPropertyChanged("CollectionMsgByPersonne");
            }
            else
            {
                MessageBox.Show("Veillez selectionner une personne");
                Console.WriteLine("[ERROR ADD MESSAGE] selectionner une personne");
            }
        }

        #endregion

    }
}
