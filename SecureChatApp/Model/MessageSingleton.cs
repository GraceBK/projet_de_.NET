using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Model
{
    class MessageSingleton
    {
        private static MessageSingleton Instance { get; set; }

        private ObservableCollection<MessageClass> messageClasses;

        public static MessageSingleton NewInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new MessageSingleton();
                }
                return Instance;
            }
        }

        public ObservableCollection<MessageClass> GetSetMessages
        {
            get => messageClasses;
            set => messageClasses = value;
        }

        /*public ObservableCollection<MessageClass> Getmessages()
        {
            return messageClasses;
        }

        public void Setmessages(ObservableCollection<MessageClass> value)
        {
            messageClasses = value;
        }*/

        private MessageSingleton()
        {
            messageClasses = new ObservableCollection<MessageClass>();
        }
    }
}
