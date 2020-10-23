using Experimental.System.Messaging;
using System;

namespace MsmqReceiver
{
   public class Program
    {
        public string ReceiveMessage()
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
       public static void main(string[] args)
        {

        }
       }
    }

