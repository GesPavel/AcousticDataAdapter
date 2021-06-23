using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcousticDataAdapter
{
    public class MissingDataException : Exception
    {
        public MissingDataException()
        {
        }

        public MissingDataException(string message)
            : base(message)
        {
        }

        public MissingDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
