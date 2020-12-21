using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    class test
    {
        /*-----------------机器人串口通信命令生成--------------------*/

        byte checkcode;
        
        //传入十进制main函数序号，生成串口校验序列（10进制转16进制）
        public string ten2hex(int FunctionNum)
        {
            string concat;
            string convert = string.Format("{0:X}", Convert.ToInt32(FunctionNum));
            if (FunctionNum < 16) concat = "02 47 0" + convert + " 03";
            else concat = "02 47 " + convert + " 03";
            return (concat);

        }

        //异或校验用：16进制转10进制
        public int[] hex2ten(int function)
        {
            string convert_hex = ten2hex(function);
            string[] split = convert_hex.Split(' ');
            int[] check_ten = new int[split.Length];
            for (int i = 0; i < split.Length; i++) check_ten[i] = System.Int32.Parse(split[i], System.Globalization.NumberStyles.HexNumber);
            return (check_ten);
        }

        //进行XOR校验，校验码checkcode奇加偶减
        public string command(int function)
        {
            int[] check = hex2ten(function);
            byte[] data = new byte[check.Length];
            for (int i = 0; i < check.Length; i++) data[i] = Convert.ToByte(check[i]);
            for (int j = 0; j < data.Length; j++) checkcode ^= data[j];
            if (checkcode % 2 != 0)
            {
                checkcode += 2;
                string convert = string.Format("{0:X}", Convert.ToInt32(checkcode));
                return (ten2hex(function) + " " + convert);
            }
            else
            {
                checkcode -= 2;
                string convert = string.Format("{0:X}", Convert.ToInt32(checkcode));
                return (ten2hex(function) + " " + convert);
            }
        }
    }
}





namespace Robot_XORcheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入main函数序号：\n");
            int number = Convert.ToInt32(Console.ReadLine());
            demo.test command = new demo.test();
            //string Forcheck = command.ten2hex(number);
            Console.WriteLine(command.command(number));
            Console.ReadLine();
        }
    }
}
