using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.MSMQ
{
   public class MsmqSender
    {
        public void SendToMsmq(string token, string email)
        {
            MessageQueue msmq = new MessageQueue();
            msmq.Path = @".\private$\ForgotPassword";
            if (!MessageQueue.Exists(msmq.Path))
            {
                MessageQueue.Create(msmq.Path);
            }
            Message message = new Message();
            //message.Formatter = new XmlMessageFormatter();
            message.Body = token;
            msmq.Send(message, email);
        }
    }
}
