using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Model
{
    [Table("User")]
    public class PersonneClass
    {
        #region Proprietes
        [PrimaryKey, Column("_id")]
        public string ID { get; set; }
        [Column("Name"), Unique]
        public string Username { get; set; }
        [Column("Admin")]
        public string Root { get; set; }

        // Proprietes messages by user
        public ObservableCollection<MessageClass> MessagesByUser(string personneId)
        {
            IEnumerable<MessageClass> query =
                from msg in MessageSingleton.NewInstance.GetSetMessages
                where msg.From == this.ID && msg.To == personneId || msg.To == this.ID && msg.From == personneId
                select msg;
            foreach (MessageClass m in query)
            {
                Console.WriteLine("{0} ------- ", m);
            }
            return new ObservableCollection<MessageClass>(query.ToList());
        }
        #endregion




        #region Constructeur
        public PersonneClass() { }

        public PersonneClass(string username)
        {
            string id = Guid.NewGuid().ToString("D");
            string root = "nope";
            ID = id;
            Username = username;
            Root = root;
        }

        public PersonneClass(string username, string root)
        {
            string id = Guid.NewGuid().ToString("D");
            ID = id;
            Username = username;
            Root = root;
        }
        #endregion

        // Methodes
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
