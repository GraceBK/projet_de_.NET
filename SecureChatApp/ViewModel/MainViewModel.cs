using SecureChatApp.Model;
using SecureChatApp.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SecureChatApp.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string dbPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "database\\grace.db3");// "db/grace.db3";
        SQLiteConnection db;

        private static PersonneClass GRACE;

        private ObservableCollection<PersonneClass> collectionPersonnes;
        private PersonneClass selectedPersonne;
        private String edtMsgText;
        
        #region Proprietes
        public PersonneClass SelectPersonne
        {
            get => selectedPersonne;
            set
            {
                if (selectedPersonne != value)
                {
                    selectedPersonne = value;
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

        public String InputMsg
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
                    return GRACE.MessagesByUser(selectedPersonne);
                }
                return null;
            }
        }
        #endregion

        #region Constructeur
        public MainViewModel()
        {
            #region Instanciation_de_la_connexion
            Console.WriteLine(dbPath);
            db = new SQLiteConnection(dbPath);
            #endregion
            db.CreateTable<PersonneClass>();
            //db.CreateTable<MessageClass>();



            PersonneClass p = new PersonneClass("Test moi");
            GRACE = p;

            try
            {
                db.Insert(p);
            } catch (SQLite.SQLiteException e)
            {
                Console.WriteLine("EXIT");
            }
            
            

            collectionPersonnes = new ObservableCollection<PersonneClass>();

            PersonneClass pers1 = new PersonneClass("Grace 1");
            PersonneClass pers2 = new PersonneClass("Grace 2");

            collectionPersonnes.Add(pers1);
            collectionPersonnes.Add(pers2);

            CollectionMsg.Add(new MessageClass("Coucou toi", p, pers2));
            CollectionMsg.Add(new MessageClass("Coucou ca va?", pers2, p));

            
            try
            {
                db.InsertAll(collectionPersonnes);
            }
            catch (SQLite.SQLiteException e)
            {
                Console.WriteLine("[ERROR INSERT] {0}", e);
            }

            /*foreach(var per in collectionPersonnes)
            {
                db.InsertAll()
            }*/

            AddPersonneCommand = new RelayCommand(AddPersonne);
            RemovePersonneCommand = new RelayCommand(RemovePersonne);
            SendMessageCommand = new RelayCommand(SendMsg);
        }
        #endregion

        #region Mes_Commandes
        public ICommand AddPersonneCommand { get; set; }

        public void AddPersonne()
        {
            WindowAddPersonne windowAdd = new WindowAddPersonne();
            windowAdd.Show();
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
                CollectionMsg.Add(new MessageClass(edtMsgText, GRACE, selectedPersonne));
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


        #region Action_Click
        /*public void Personne_SelectionChanged(object sender, EventArgs e)
        {
            if (idlistpersonnes.selecteditem != null)
            {
                index = idlistpersonnes.selectedindex;

                grace.messagesbyuser(personnes[index]);

                console.writeline("id: {0} [{1}] {2}", index, personnes[index].username, grace.messagesbyuser(personnes[index]).count);
           }
        }*/
        #endregion


        #region Event_and_Notify
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string monPropriete)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(monPropriete));
            }
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T valeur, string monPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            NotifyPropertyChanged(monPropriete);
            return true;
        }
        #endregion
    }
}
