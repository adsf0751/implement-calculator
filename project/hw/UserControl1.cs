using System;
using System.Data;
using System.Windows.Forms;
//issue: 輸入+/- 
namespace hw
{
    public partial class UserControl1 : UserControl
    {
        String num1, num2, num3;
        String op1, op2, op3;
        string str;
        string prev;
        double result;
        string input;
        string expression;
        int flag;
        bool point;
        bool error;
        public UserControl1()
        {
            InitializeComponent();
            label1.Text = "預設值為0";
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
            error = false;
;
            flag = 0;
            point = false;

        }

        void SetValue()
        {
            if (string.IsNullOrEmpty(num1))
            {
                num1 = str;

            }
            else if (string.IsNullOrEmpty(num2))
            {
                num2 = str;
            }
            else if (string.IsNullOrEmpty(num3))
            {
                num3 = str;
            }

            str = null;
        }
        private void reset(object sender, EventArgs e)
        {
            label1.Text = "預設值為0";
            label2.Text = "Input: ";
            label3.Text = "紀錄: ";
            init();
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void Get_result(object sender, EventArgs e)
        {
            Print();
            //按數字完接著按等號  number= ，result = number
            if (string.IsNullOrEmpty(op2))
            {
                if (string.IsNullOrEmpty(op1))
                {
                    if (string.IsNullOrEmpty(num1))
                    {
                        if(string.IsNullOrEmpty(str))
                        {
                            //都沒有輸入值，判斷是否點過(+/-)，不用多一個flag==0判斷。
                            str = (flag % 2 == 0) ? "0" : "-0";
                        }
                        else
                        {
                            str = (flag % 2 == 0) ? str : "-"+str;
                        }
                    }

                    label1.Text = str;
                }
                else //op1有值
                {
                    if (string.IsNullOrEmpty(num2))
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            if (string.IsNullOrEmpty(num1))
                            {
                                //僅輸入+-*/
                                label1.Text = "0";
                            }
                            else
                            {   //num1 op1
                                if (num1 == "-0")
                                {
                                    label1.Text = "0";
                                }
                                else
                                {
                                    Count(num1, num1, op1);

                                }
                            }
                        }
                        else
                        { //num1 op1 str
                            str = (flag % 2 == 0) ? str : "-" + str;
                            Count(num1, str, op1);
                        }
                    }
                    label1.Text = (error) ? "錯誤" : result.ToString();
                }
            }
            else
            {
                if (!(string.IsNullOrEmpty(str)))
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
                {
                    //     1+2+
                    if (op1 == "+" || op1 == "-")
                    {
                        if (op2 == "*" || op2 == "/")
                        {
                            Count(num2, num2,op2);
                            Count(result.ToString(), num1, op1);
                        }
                        else
                        {
                            Count(num1, num2, op1);
                            Count(result.ToString(), result.ToString(), op2);
                        }

                    }
                    else
                    {
                        Count(num1, num2, op1);
                        Count(result.ToString(), result.ToString(), op2);
                    }
                }
                label1.Text = (error) ? "錯誤" : result.ToString();
            }

            label2.Text = "Input: ";
            //label3.Text = "紀錄: ";
            init();
        }
        private void button_number(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string temp = btn.Text;

            input += ((temp == "+/-") ? "(-)" : temp);
            label2.Text = input;






            prev = null;
            Print();
            if (temp == "+/-")
            {
                flag++;
                if (string.IsNullOrEmpty(str))
                {
                    label1.Text = (flag % 2 == 0) ? "0" : "-0";
                }
                else
                {
                    label1.Text = (flag % 2 == 0) ? str : ("-" + str);
                }
                return;
            }
            if (temp == ".")
            {
                if (!point)
                {
                    point = true;
                    str += temp;
                    label1.Text = str;
                    Print();
                   
                }
                return;
            }

            str += temp;
            if (!(flag % 2 == 0))
            {
                label1.Text = "-" + str;
            }
            else { label1.Text = str; }

            Print();
        }

        private void button_ops(object sender, EventArgs e)
        {
            point = false;
;
            Print();
            Button btn = (Button)sender;
            string temp = btn.Text;
            input += temp;
            label2.Text = input;


            if (!string.IsNullOrEmpty(str))
            {
                if (!(flag % 2 == 0))
                {
                    string s = str;
                    str = "-";
                    str += s;
                }
            }
            else
            {
                if (!(flag == 0))
                {
                    str = (flag % 2 == 0) ? "0" : "-0";
                }
            }
            flag = 0;
            SetValue();

            if (string.IsNullOrEmpty(prev))
            {
                if (string.IsNullOrEmpty(op1))
                {
                    op1 = temp;
                    prev = "op1";

                }
                else if (string.IsNullOrEmpty(op2))
                {
                    op2 = temp;
                    prev = "op2";
                }
                else if (string.IsNullOrEmpty(op3))
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

                
            if (!string.IsNullOrEmpty(op1) && string.IsNullOrEmpty(num1))
            {
                num1 = "0";
            }

            Print();


            if (!(string.IsNullOrEmpty(num1) ) &&
                !(string.IsNullOrEmpty(num2) ) &&
                !(string.IsNullOrEmpty(num3) ) &&
                !(string.IsNullOrEmpty(op1)  ) &&
                !(string.IsNullOrEmpty(op2)  ) &&
                !(string.IsNullOrEmpty(op3)  ) 
               )
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
                label1.Text = (error) ? "錯誤" : result.ToString();
            }
            if( !(string.IsNullOrEmpty(op1)) && !(string.IsNullOrEmpty(op2)) )
            {
                if (op1 == "+" || op1 == "-")
                {
                    if (op2 == "+" || op2 == "-")
                    {
                        Count(num1, num2, op1);
                        label1.Text = (error) ? "錯誤" : result.ToString();
                    }
                    else
                    {

                        // 1+2*3+
                        if (!(string.IsNullOrEmpty(op3)))
                        {
                            Count(num2, num3, op2);
                            label1.Text = (error) ? "錯誤" : result.ToString();
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
                    label1.Text = (error) ? "錯誤" : result.ToString();
                }
            }


        }
        void Print()
        {
            label3.Text =      "num1 : " + num1 +
                            "\n op1 : "  + op1  +
                            "\n num2 : " + num2 +
                            "\n op2 : "  + op2  +
                            "\n num3 : " + num3 + 
                            "\n op3 :"   + op3  + 
                            "\n prev: "  + prev + 
                            "\n 最後還在輸入的值:" + str;
        }
        void Count(string num1, string num2, string op)
        {
            //因為/0會執行並顯示結果為無限大符號，故不能用try catch (divide by zero)
            if (num2 == "0" && op == "/")
            {
                error = true;
            }
            else
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
                        result = double.Parse(num1) / double.Parse(num2);
                        break;
                }
                if (double.TryParse(result.ToString(), out double doubleValue))
                {
                    result = doubleValue;//將123.0改成123
                }
            }
        }
        void Count(string num1, char op)
        {
            if (num1 == "0" && op == '/')
            {
                error = true;
            }
            else
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
}
