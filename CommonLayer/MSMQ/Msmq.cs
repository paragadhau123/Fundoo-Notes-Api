using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.MSMQ
{
   public class Msmq
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
            message.Formatter = new BinaryMessageFormatter();
            message.Body = token;
            msmq.Send(message, email);
        }

        public static string ReceiveMessage()
        {
            using (MessageQueue myQueue = new MessageQueue())
            {
                myQueue.Path = @".\private$\ForgotPassword";
                Message message = new Message();
                message = myQueue.Receive(new TimeSpan(0, 0, 5));
                message.Formatter = new BinaryMessageFormatter();
                string msg = message.Body.ToString();
                return msg;
            }
        }
    }
}
