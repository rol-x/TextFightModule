using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TheAmuletsOfCamembert
{
    class EpicWriter
    {
        private string _message;
        private int _waitTime;
        private bool _messageFinished;

        private void ClearConsoleBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public void Write(string message)
        {
            _message = message;
            _waitTime = 32;
            _messageFinished = false;

            Thread skipThread = new Thread(ClickToSkip);
            skipThread.Start();

            foreach (char letter in _message)
            {
                Console.Write(letter);
                Thread.Sleep(_waitTime);
            }
            _messageFinished = true;

            ClearConsoleBuffer();
            skipThread.Join();
        }

        public void ClickToSkip()
        {
            while (_messageFinished == false)
            {
                if (Console.KeyAvailable)
                {
                    _waitTime = 0;
                    break;
                }
            }
        }
    }
}
