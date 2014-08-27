
namespace MvcFramework.Model
{
    public class Model_Dept_Info
    {
        public int DEPT_REF_ID { get; set; }
        public int UP_DEPT_ID { get; set; }
        public int DEPT_LEVEL { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public string UP_DEPT_NAME { get; set; }
        public string USE_YN { get; set; }
    }
}
