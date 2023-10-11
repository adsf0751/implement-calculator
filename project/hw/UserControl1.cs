using System;
using System.Data;
using System.Windows.Forms;

namespace hw
{
    public partial class UserControl1 : UserControl
    {
        String? num1, num2, num3;
        String? op1, op2, op3;
        string str ;
        string prev ;
        double result ;
        string input;
        string last;
        string expression;
        public UserControl1()
        {
            InitializeComponent();
            init();
        }
        void init()
        {
            num1 = num2 = num3 = null;
            op1 = op2 = op3 = null;
            str = "";
            prev = "";
            result = 0;
            input = "";

            expression = "";
            button14.Enabled = true;
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void get_result(object sender, EventArgs e)
        {
            Print();
            //按數字完接著按等號  number= ，result = number
            if (op2 == null)
            {
                if (op1 == null)
                {
                    bool isInteger = int.TryParse(input, out int intValue);
                    if (isInteger)
                    {
                        label1.Text = str;
                    }
                    label1.Text = str;
                }
                else
                {
                    //case: 1+2 or 1+
                    if (str != "")
                    {
                        //1+2
                        num2 = str;
                        Count(num1, num2, op1);

                    }
                    if (num2 == null)
                    {
                        //1+
                        Count(num1, op1[0]);
                    }
                    label1.Text = result.ToString();

                }
            }
            else
            {
                if (str != "")
                {
                    //       1+2+3
                    if (op1 == "+" || op2 == "-")
                    {
                        if (op2 == "*" || op2 == "/")
                        {
                            Count(num2, str, op2);
                            Count(num1, result.ToString(), op1);
                        }
                        else
                        {
                            Count(num1, num2, op1);
                            Count(result.ToString(), str, op2);
                        }

                    }
                    else
                    {
                        Count(num1, num2, op1);
                        Count(result.ToString(), str, op2);
                    }

                }
                else
                {       //     1+2+
                    if (op1 == "+" || op2 == "-")
                    {
                        if (op2 == "*" || op2 == "/")
                        {
                            Count(num2, op2[0]);
                            Count(result.ToString(), num1, op1);
                        }
                        else
                        {
                            Count(num1, num2, op1);
                            Count(result.ToString(), op2[0]);
                        }

                    }
                    else
                    {
                        Count(num1, num2, op1);
                        Count(result.ToString(), op2[0]);
                    }
                }
                label1.Text = result.ToString();
            }

            init();
        }
        private void button_number(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string temp = btn.Text;
            input += temp;
            label2.Text = input;
            prev = null;
            Print();

            if (temp == ".")
            {
                button14.Enabled = false;
            }
            str += temp;
            label1.Text = str;
            Print();

        }

        private void reset(object sender, EventArgs e)
        {
            label1.Text = "";
            init();
        }


        private void button_ops(object sender, EventArgs e)
        {

            button14.Enabled = true;
            Print();
            Button btn = (Button)sender;
            string temp = btn.Text;
            input += temp;
            label2.Text = input;
            last = str;
            if (num1 == null)
            {
                num1 = str;
                str = "";
            }
            else if (num2 == null)
            {
                num2 = str;
                str = "";
            }
            else if (num3 == null)
            {
                num3 = str;
                str = "";
            }

            if (prev == null)
            {

                if (op1 == null)
                {
                    op1 = temp;
                    prev = "op1";
                }
                else if (op2 == null)
                {
                    op2 = temp;
                    prev = "op2";
                }
                else if (op3 == null)
                {
                    op3 = temp;
                    prev = "op3";
                }
            }
            else
            {
                if (prev == "op1")
                {
                    op1 = temp;
                }
                if (prev == "op2")
                {
                    op2 = temp;
                }
                if (prev == "op3")
                {
                    op3 = temp;
                }
            }
            Print();

            if (num1 != null && num2 != null && num3 != null && op1 != null && op2 != null && op3 != null)
            {
                if (op1 == "+" || op1 == "-")
                {
                    if (op2 == "*" || op2 == "/")
                    {

                        Count(num2, num3, op2);
                        num2 = result.ToString();
                        num3 = null;
                        op2 = op3;
                        op3 = null;
                        prev = "op2";
                    }
                    else if (op2 == "+" || op2 == "-")
                    {
                        Count(num1, num2, op1);
                        num1 = result.ToString();
                        num2 = num3;
                        num3 = null;
                        op1 = op2;
                        op2 = op3;
                        op3 = null;
                        prev = "op2";
                    }
                    Print();

                }
                else if (op1 == "*" || op1 == "/")
                {
                    Count(num1, num2, op1);
                    num1 = result.ToString();
                    num2 = num3;
                    num3 = null;
                    op1 = op2;
                    op2 = op3;
                    op3 = null;
                    prev = "op2";
                    Print();

                }
                label1.Text = result.ToString();
            }

            if (op1 != null && op2 != null)
            {
                if (op1 == "+" || op1 == "-")
                {
                    if (op2 == "+" || op2 == "-")
                    {
                        Count(num1, num2, op1);
                        label1.Text = result.ToString();
                    }
                    else
                    {

                        // 1+2*3+
                        if (op3 != null)
                        {
                            Count(num2, num3, op2);
                            label1.Text = result.ToString();
                        }
                        else
                        {
                            label1.Text = num2;
                        }
                    }
                }
                else
                {
                    Count(num1, num2, op1);
                    label1.Text = result.ToString();
                }
            }


        }
        void Print()
        {
            label3.Text = "num1 : " + num1 + "\n op1 : " + op1 + "\n num2 : " + num2 + "\n op2 : " + op2 + "\n num3 : " + num3 + "\n op3 :" + op3 + "\n prev: " + prev + "\n最後還在輸入的值:" + str + "\n 倒數的數值" + last;
            expression = num1 + op1 + num2 + op2 + num3 + op3;
        }
        void Count(string num1, string num2, string op)
        {
            switch (op)
            {

                case "+":
                    result = double.Parse(num1) + double.Parse(num2);
                    break;
                case "-":
                    result = double.Parse(num1) - double.Parse(num2);
                    break;
                case "*":
                    result = double.Parse(num1) * double.Parse(num2);
                    break;
                case "/":
                    try
                    {
                        result = double.Parse(num1) / double.Parse(num2);
                    }
                    catch (DivideByZeroException e)
                    {
                        label1.Text = "錯誤";
                    }
                    break;
            }
            if (double.TryParse(result.ToString(), out double doubleValue))
            {
                result = doubleValue;//將123.0改成123
            }
        }
        void Count(string num1, char op)
        {
            double num = double.Parse(num1);
            //case1 :one number and one operand  
            switch (op)
            {
                case '+':
                    result = num + num;
                    break;
                case '-':
                    result = num - num;
                    break;
                case '*':
                    result = num * num;
                    break;
                case '/':
                    result = num / num;
                    break;

            }
            if (double.TryParse(result.ToString(), out double doubleValue))
            {
                result = doubleValue;//將123.0改成123
            }
        }

    }
}





