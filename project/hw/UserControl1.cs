using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace hw
{
    public partial class UserControl1 : UserControl
    {
        string filepath = "log.txt";
        string string_label1_init_value = "0";
        string string_result = "";

        //string string_label1_init_value = "預設值為 0";
        string string_label2_init_value = "Input: ";
        string string_error = "錯誤";
        //string string_result = "最終結果為:";



        String num1, num2, num3;
        String op1, op2, op3;
        string str;
        string prev;
        double result;
        string input;
        int flag;
        bool point;
        bool error;


        public UserControl1()
        {
            
            InitializeComponent();
            label1.Text = string_label1_init_value ;
            label4.Text = null;
            init();
        }
        public void Append_String_To_File( string Write_String)//網路上的code
        {
            Write_String += "\n";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.Write(Write_String);
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.Write(Write_String);
                }

            }
        }
        void init()
        {
            num1 = num2 = num3 = null;
            op1 = op2 = op3 = null;
            str = null;
            prev = null;
            result = 0;
            error = false;

            //flag = 0;
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
        void GoActionResult()
        {
            label1.Text = string_result + string_error;
            label2.Text = string_label2_init_value;
            flag = 0;
            input = null;
            init();
        }
        private void reset(object sender, EventArgs e)
        {
            label1.Text = string_label1_init_value;
            label2.Text = string_label2_init_value;
            label3.Text = "紀錄: ";
            label4.Text = null;
            input = null;
            flag = 0;
            init();
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        bool IsEmpty(string s)
        {
            return string.IsNullOrEmpty(s);
        }
        private void Get_result(object sender, EventArgs e)
        {
            bool only_click_equal = false;
            input += "=";
            SetString();
            SetValue();
            flag = 0;
            // =  、 number= 、 (+/-)= 、  op=    
            // (+/-)op= 、 number op= 、 1+
            // (+/-)op(+/-)=、number op number= 1+2 
            // (+/-)op(+/-)op(+/-) 1+2+
            //1+2+3
            string getResult = null;
            if (IsEmpty(op2))
            {
                if(IsEmpty(op1))
                {
                    if(IsEmpty(num1))
                    {

                        only_click_equal = true;
                        //  //only click = 
                        getResult = "0";
                        //label4.Text = "0";
                    }
                    else
                    {
                        //click number or (+/-)

                        getResult = num1;
                        //label4.Text = num1;
                    }
                }
                else
                {

                    if(IsEmpty(num2))
                    {
                        //click (+/-) or not ， click number or not ， only click op
                        //因為op1有值，不管有無輸入數值或正負號都會帶一個0
                        Count(num1, num1, op1);
                        if (error)
                        {
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                        //label4.Text = getResult;
                        //label4.Text = "num1:" + num1 + "\n op1 :" + op1;
                    }
                    else
                    {
                        //1+2
                        Count(num1, num2, op1);
                        if (error)
                        {
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                    }
                }
            }
            else
            {
                if(IsEmpty(num3))
                {
                    if( op1 =="*" || op1 == "/")
                    {
                     // 1*2 (+-*/)   
                        Count(num1, num2, op1);  
                        if(error)
                        {
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                        Count(getResult, getResult, op2);//op2 的加減乘除都考慮進去了
                        if (error)
                        {
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                        //label4.Text = "num1:" + num1 + "\n op1 :" + op1 + "\n num2: " + num2 + "\n op2 :" + op2 + "\n getResult:" + getResult;
                    
                    }
                    else
                    {   //1 (+-) 2  (*/) 
                        if (op2 =="*" || op2 =="/")
                        {
                            Count(num2, num2, op2);
                            if(error)
                            {
                                //label4.Text = "錯誤";
                                GoActionResult();
                                return;
                            }
                            getResult = result.ToString();
                            Count(num1, getResult, op1);
                            getResult = result.ToString();
                            //label4.Text = "num1:" + num1 + "\n op1 :" + op1 + "\n num2: " + num2 + "\n op2 :" + op2 + "\n getResult:" + getResult;
                        }
                        else
                        {
                            Count(num1, num2, op1);
                            Count(result.ToString(), result.ToString(), op2);
                            getResult = result.ToString();
                        }
                    }
                }
                else
                {
                    if(op1 == "*" || op1 =="/")
                    {   
                        //1 * 2 (+-*/) 3
                        Count(num1, num2, op1);
                        if (error)
                        {
                            //label4.Text = "錯誤";
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                        Count(getResult, num3, op2);
                        if (error)
                        {
                            //label4.Text = "錯誤";
                            GoActionResult();
                            return;
                        }
                        getResult = result.ToString();
                        /*
                        label4.Text = "num1:" + num1 + "\n op1 :" + op1 + "\n num2: " + num2 + "\n op2 :" + op2 +
                            "\n num3: " + num3 +
                            "\n getResult:" + getResult;
                        */

                    }
                    else
                    {
                        if(op2 =="*" || op2 =="/")
                        {
                            Count(num2, num3, op2);//op2 maybe '/' check divide by zero
                            if (error)
                            {
                                //label4.Text = "錯誤";
                                GoActionResult();
                                return;
                            }
                            Count(num1, result.ToString(), op1);
                        }
                        //1 (+-) 2 (+-*/) 3
                        //confirm op1 == (+-)
                        else
                        {
                            Count(num1, num2, op1);
                            Count(result.ToString(),num3, op2);
                            
                        }
                        getResult = result.ToString();

                        /*
                        label4.Text = "num1:" + num1 + "\n op1 :" + op1 + "\n num2: " + num2 + "\n op2 :" + op2 +
                            "\n num3: " + num3 +
                            "\n getResult:" + getResult;
                        */
                    }
                    //label4.Text = "num1:" + num1 + "\n op1 :" + op1 + "\n num2: " + num2 + "\n op2 :" + op2 + "\n num3: " + num3;
                }
            }

            //label4.Text = getResult;
            label1.Text = string_result + getResult;
            //label2.Text = string_label2_init_value;
            
            input += getResult;
            input += only_click_equal ? "=" : "";
            Append_String_To_File(input);
            init();
            double double_result = double.Parse(getResult);
            if(double_result == 0.0 || double_result == -0.0)
            {            
                return;
            }

            str = str = getResult.Trim('-'); //正確寫法
            flag = (double_result > 0.0) ? flag : flag+1;
            //num1 = getResult;
            label2.Text = input;
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
                    //label4.Text = flag.ToString();
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
            //str is not null and str= "0"，當輸入除號後 數字0123 轉成123
            if(!IsEmpty(str) && str.Length==1 && str =="0")
            {
                str = null;
            }
            str += temp;
            if (!(flag % 2 == 0))
            {
                label1.Text = "-" + str;
            }
            else { label1.Text = str; }

            Print();
        }

        string   SetString()
        {

            //存數值的可能，input : (+/-) 、 null 、 (+/-)number 、 number
            if (flag == 0)//case : 沒有點選(+/-)
            {
                if (IsEmpty(str))
                {
                    //  
                    //do nothing ，str = null 
                    //return str;  
                }
                else
                {
                    //return str;
                    // only number
                }
            }
            else //clicked (+/-)
            {
                if(IsEmpty(str))
                {
                    // (+/-)
                    str = (flag % 2 == 0) ? "0" : "-0";
                }
                else
                {
                    //(+/-)number
                    str = (flag % 2 == 0) ? str : "-"+ str;
                }
            }
            //label4.Text = "Print string value: " + str;
            return str;
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
            SetString();
            SetValue();
            flag = 0;
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
                        if(error)
                        {
                            GoActionResult();
                            return;
                        }
                        num2 = result.ToString();
                        num3 = null;
                        op2 = op3;
                        op3 = null;
                        prev = "op2";
                    }
                    else if (op2 == "+" || op2 == "-")
                    {
                        Count(num1, num2, op1);
                        if (error)
                        {
                            GoActionResult();
                            return;
                        }
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
                    if (error)
                    {
                        GoActionResult();
                        return;
                    }
                    num1 = result.ToString();
                    num2 = num3;
                    num3 = null;
                    op1 = op2;
                    op2 = op3;
                    op3 = null;
                    prev = "op2";
                    Print();

                }
                label1.Text =  result.ToString();
            }
            if( !(string.IsNullOrEmpty(op1)) && !(string.IsNullOrEmpty(op2)) )
            {
                if (op1 == "+" || op1 == "-")
                {
                    if (op2 == "+" || op2 == "-")
                    {
                        Count(num1, num2, op1);
                        if (error)
                        {
                            GoActionResult();
                            return;
                        }
                        label1.Text =  result.ToString();
                    }
                    else
                    {

                        // 1+2*3+
                        if (!(string.IsNullOrEmpty(op3)))
                        {
                            Count(num2, num3, op2);
                            if (error)
                            {
                                GoActionResult();
                                return;
                            }
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
                    if (error)
                    {
                        GoActionResult();
                        return;
                    }
                    label1.Text = result.ToString();
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
            if (double.Parse(num2) == 0.0 && op == "/")
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

        /*        void Count(string num1, char op)
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
        }*/
    }
}
