using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Model
{
    [Table("Message")]
    public class MessageClass
    {
        #region Constructeur
        public MessageClass() { }
        public MessageClass(String message, string from, string to)
        {
            string id = Guid.NewGuid().ToString("D");
            ID = id;
            Msg = message;
            From = from;
            To = to;
            CreateAt = DateTime.Now;
        }
        #endregion

        #region Proprietes
        [PrimaryKey, Column("_id")]
        public string ID { get; set; }
        [Column("Message")]
        public string Msg { get; set; }
        [Column("From")]
        public string From { get; set; }
        [Column("To")]
        public string To { get; set; }
        [Column("CreateAt")]
        public DateTime CreateAt { get; set; }
        #endregion

        #region Methodes
        public override string ToString()
        {
            string res;
            res = "Message: [{"+ Msg + "} | {" + From + "} --> {" + To + "} | {" + CreateAt + "}]";
            return res;
        }
        #endregion
    }
}
