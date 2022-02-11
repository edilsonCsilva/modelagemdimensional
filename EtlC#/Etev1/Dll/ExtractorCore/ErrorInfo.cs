using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractorCore
{
  
    class ErrorInfo
    {
        public int typeErrro { get; set; }
        public String descriptionError { get; set; }

        public ErrorInfo(string exeption,int typeError=500)
        {

            this.typeErrro = typeError;
            this.descriptionError = exeption;



        }

    }
}
