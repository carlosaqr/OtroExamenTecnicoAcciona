using ExamenTecnicoAcciona.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace ExamenTecnicoAcciona.Helper
{
    public class LogHelper<T> : ILogHelper<T>
    {
        public ILog Logger { get; set; }

        public LogHelper()
        {
            this.Logger = log4net.LogManager.GetLogger(typeof(T));
        }
    }
}