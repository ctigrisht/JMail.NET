global using JMail.NET.Models;

namespace JMail.Relay.Hook
{
    /// <summary>
    /// This class is used to hook to whichever backend you are going to use, such as a program to save jmails to database
    /// </summary>
    public static class LetterHook
    {
        public static void IncomingJMailHandler(this JMailLetter letter)
        {
            //
            // Write your logic to handle reception of JMail
            //
            Console.WriteLine($"\n\nRECEIVED JMAIL" +
                $"\n [{letter.Origin}] --> [{letter.Target}] \n" +
                $"\n From: {letter.Sender}" +
                $"\n To: {letter.Recipient}" +
                $"\n ---" +
                $"\n Date: {letter.Date.ToString("g")}" +
                $"\n Header: {letter.Message.Content.Header}" +
                $"\n Body: {letter.Message.Content.Body}" +
                $"\n Footer: {letter.Message.Content.Footer}" +
                $"\n END OF MESSAGE");
            //
            // Write your logic to handle reception of JMail
            //
        }

    }
}
