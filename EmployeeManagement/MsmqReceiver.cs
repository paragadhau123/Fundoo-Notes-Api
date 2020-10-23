using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class MsmqReceiver
    {
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
