using System.Collections.Generic;

namespace MvcFramework.Model
{
    public class Model_Category_Info
    {
        public string CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string CATEGORY_DESC { get; set; }

        public List<Model_Code_Info> CodeInfoList { get; set; }
 
    }
}
