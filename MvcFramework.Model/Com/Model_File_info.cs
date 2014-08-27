
using System;

namespace MvcFramework.Model
{
    public class Model_File_info
    {
        public int FILE_IDX { get; set; }
        public string FILE_ID { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_ORI_NAME { get; set; }
        public string FILE_TYPE { get; set; }
        public string FILE_URL { get; set; }
        public string FILE_FOLDER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int CREATE_USER { get; set; }
    }
}

