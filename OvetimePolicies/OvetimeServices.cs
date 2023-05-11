namespace OvetimePolicies
{
    public interface IOvetimeServices
    {
        public decimal CalculatorA();
        public decimal CalculatorB();
        public decimal CalculatorC();
    }
    public  class OvetimeServices: IOvetimeServices
    {
        //حقوق پایه
        protected decimal BasicSalary { get; set; }
        //حق جذب
        protected decimal Allowance { get; set; }
        //ایاب و ذهاب
        protected decimal Transportation { get; set; }
        //مالیات
        protected decimal Tax { get; set; } 

        public OvetimeServices(decimal basicSalary, decimal allowance, decimal transportation,decimal tax)
        {
            BasicSalary = basicSalary;
            Allowance = allowance;
            Transportation = transportation;
            Tax = tax;
        }
        //محاسبه حقوق
        public decimal CalculatorA()
        {
            return this.BasicSalary + this.Allowance + this.Transportation + CalculatorB()- CalculatorC();
        }
        //محاسبه اضافه کاری
        public decimal CalculatorB()
        {
            return this.BasicSalary + this.Allowance;
        }
        //محاسبه مالیات
        public decimal CalculatorC()
        {
            return (this.BasicSalary + this.Allowance)*Tax;
        }
    }
}