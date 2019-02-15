using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Model
{
    public class MessageClass
    {
        #region Constructeur
        public MessageClass(String message, PersonneClass from, PersonneClass to)
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
        public string ID { get; set; }

        public string Msg { get; set; }

        public PersonneClass From { get; set; }

        public PersonneClass To { get; set; }

        public DateTime CreateAt { get; set; }
        #endregion

        #region Methodes
        public override string ToString()
        {
            string res;
            res = "Message: [{"+ Msg + "} | {" + From.Username + "} --> {" + To.Username + "} | {" + CreateAt + "}]";
            return res;
        }
        #endregion
    }
}
