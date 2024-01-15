using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace windowsform
{
    public partial class Form1 : Form
    {
        int[,] BracketPairs;
        Color defaultcolour = Color.WhiteSmoke;
        enum ValueType { Regular, Constant };
        private string value = string.Empty;
        private List<string> expression = new List<string>(), brac = new List<string>();
        private bool shouldReset = false;
        private string ans = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void AssignSign(string sign)
        {
            if (shouldReset)
                ResetCalc();

            if (sign == "-" && ExpressionBox.Text.Length == 1)
            {
                value += sign;
            }
            else
            {

                if (value != string.Empty)
                {
                    expression.Add(value);
                    value = string.Empty;

                }
                expression.Add(sign);
            }

            ExpressionBox.Text += sign;

        }
        private void AssignValue(string value, ValueType vt)
        {
            if (shouldReset)
                ResetCalc();

            switch (vt)
            {
                case ValueType.Regular:
                    this.value += value;
                    break;

                case ValueType.Constant:
                    if (this.value != string.Empty)
                        expression.Add(this.value);
                    ExpressionBox.Text += value;
                    expression.Add(value);
                    value = string.Empty;
                    break;
            }
            ExpressionBox.Text += value;
        }

        private void WorkingWithBrackets(int index, int bracIndex)
        {
            for (int i = index + 1; i < expression.Count; i++)
            {
                if (expression[i] == "(")
                    WorkingWithBrackets(i, ++bracIndex);

                if (expression[i] == ")")
                {
                    BracketPairs[bracIndex, 0] = index;
                    BracketPairs[bracIndex, 1] = i;
                    CalculatingInBrackets(index, i);
                    brac.Clear();
                    break;
                }

            }
        }

        private void CalculatingInBrackets(int first, int second)
        {

            for (int i = first + 1; i < second; i++)
            {
                brac.Add(expression[i]);
            }

            brac = CalcList(brac);
            if (first - 1 > -1)
            {
                double x = 0;
                if (ModeButton.Text == "RAD")
                    x = double.Parse(brac[0]);
                else if (ModeButton.Text == "DEG")
                    x = ((double.Parse(brac[0]) * Math.PI) / 180);
                else if (ModeButton.Text == "GRAD")
                    x = (10 / 9) * (double.Parse(brac[0]) * Math.PI) / 180;

                if (expression[first - 1] == "sin")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Sin(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "cos")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Cos(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "tan")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Tan(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "sin-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Asin(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "cos-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Acos(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "tan-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Atan(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "cosec")
                {
                    brac[0] = Convert.ToString(Math.Round(1 / Math.Sin(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "sec")
                {
                    brac[0] = Convert.ToString(Math.Round(1 / Math.Cos(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "cot")
                {
                    brac[0] = Convert.ToString(Math.Round(1 / Math.Tan(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "cosec-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Asin(1 / x), 15));
                    first--;
                }

                else if (expression[first - 1] == "sec-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Acos(1 / x), 15));
                    first--;
                }

                else if (expression[first - 1] == "cot-1")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Atan(1 / x), 15));
                    first--;
                }
                else if (expression[first - 1] == "sinh")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Sinh(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "cosh")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Cosh(x), 15));
                    first--;
                }

                else if (expression[first - 1] == "tanh")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Tanh(x), 15));
                    first--;
                }
                else if (expression[first - 1] == "cosech")
                {
                    brac[0] = Convert.ToString(Math.Round(2 / (Math.Exp(x) - Math.Exp(-x)), 15));
                    first--;
                }

                else if (expression[first - 1] == "sech")
                {
                    brac[0] = Convert.ToString(Math.Round(2 / (Math.Exp(x) + Math.Exp(-x)), 15));
                    first--;
                }

                else if (expression[first - 1] == "coth")
                {
                    brac[0] = Convert.ToString(Math.Round((Math.Exp(x) + Math.Exp(-x)) / (Math.Exp(x) - Math.Exp(-x)), 15));
                    first--;
                }


                else if (expression[first - 1] == "ln")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Log(double.Parse(brac[0])), 15));
                    first--;
                }
                else if (expression[first - 1] == "log")
                {
                    brac[0] = Convert.ToString(Math.Round(Math.Log(double.Parse(brac[0])) / Math.Log(10), 15));
                    first--;
                }
            }
            expression.RemoveRange(first, second - first + 1);

            if (expression.Count > 0)
                expression.Insert(first, brac[0]);
            else
                expression.Add(brac[0]);
        }

        private void WorkingWithSigns(int i)
        {

            if (expression[i + 1] == "+" && expression[i] == "+")
            {
                expression.RemoveAt(i);
            }
            else if (expression[i + 1] == "+" && expression[i] == "-")
            {
                expression.RemoveAt(++i);
            }
            else if (expression[i + 1] == "-" && expression[i] == "+")
            {
                expression.RemoveAt(i);
            }
            else if (expression[i + 1] == "-" && expression[i] == "-")
            {
                expression[i] = "+";
                expression.RemoveAt(++i);
            }
            else if (double.TryParse(expression[i + 1], out double num) && expression[i] == "-")
            {
                expression[i + 1] = "-" + expression[i + 1];
                if (expression[i - 1] == "*" || expression[i - 1] == "/")
                    expression.RemoveAt(i);
                else
                    expression[i] = "+";
            }
            else if (expression[i + 1] == "+" && (expression[i] == "*" || expression[i] == "/"))
            {
                expression.RemoveAt(++i);
            }
            else if (expression[i + 1] == "-" && (expression[i] == "*" || expression[i] == "/"))
            {
                while (!(double.TryParse(expression[i + 2], out _)))
                {
                    WorkingWithSigns(i + 1);
                }
            }
        }

        public List<string> CalcList(List<string> ls)
        {
            while (ls.Contains("%"))
            {

                ls.Insert(ls.IndexOf("%"), "/");
                ls.Insert(ls.IndexOf("%"), "100");
                ls.Remove("%");
            }

            while (ls.Contains("Ans"))
                ls = ConstHandler("Ans", double.Parse(ans), ls);

            while (ls.Contains("e"))
                ls = ConstHandler("e", Math.E, ls);

            while (ls.Contains("π"))
                ls = ConstHandler("π", Math.PI, ls);

            while (ls.Contains("^"))
            {
                int index = ls.IndexOf("^");
                if (ls[ls.IndexOf("^") + 1] == "√")
                {
                    string x = Math.Pow(double.Parse(ls[ls.IndexOf("^") + 2]), 1 / double.Parse(ls[ls.IndexOf("^") - 1])).ToString();
                    ls[ls.IndexOf("^") - 1] = x;

                }
                ls[index - 1] = Math.Pow(double.Parse(ls[index - 1]), double.Parse(ls[index + 1])).ToString();
                ls.RemoveAt(index);
                ls.RemoveAt(index);
            }

            while (ls.Contains("√"))
            {
                int index = ls.IndexOf("√");
                ls[index] = Math.Sqrt(double.Parse(ls[index + 1])).ToString();
                ls.RemoveAt(index + 1);
            }

            while (ls.Contains("!"))
            {
                double result = 1;
                for (int i = int.Parse(ls[ls.IndexOf("!") - 1]); i > 0; i--)
                    result *= i;
                ls[ls.IndexOf("!") - 1] = result.ToString();
                ls.Remove("!");
            }

            while (ls.Contains("/"))
                ls = BODMAS("/", ls);

            while (ls.Contains("*"))
                ls = BODMAS("*", ls);

            while (ls.Contains("+"))
                ls = BODMAS("+", ls);

            while (ls.Contains("-"))
                ls = BODMAS("-", ls);
            return ls;
        }

        private void ResetCalc()
        {
            value = string.Empty;
            expression.Clear();
            ExpressionBox.Text = string.Empty;
            AnswerLabel.Text = string.Empty;
            shouldReset = false;
            ExpressionBox.ReadOnly = false;
        }
        private List<string> ConstHandler(string s, double d, List<string> ls)
        {
            if (ls.IndexOf(s) > 0 && ls[ls.IndexOf(s) - 1] != "+" && ls[ls.IndexOf(s) - 1] != "-" && ls[ls.IndexOf(s) - 1] != "*" && ls[ls.IndexOf(s) - 1] != "/")
                ls.Insert(ls.IndexOf(s), "*");

            ls[ls.IndexOf(s)] = d.ToString("N0");
            return ls;
        }

        private List<string> BODMAS(string sign, List<string> ls)
        {

            double i1 = 0, i2 = 0, res = 0;
            i1 = Convert.ToDouble(ls[ls.IndexOf(sign) - 1]);
            i2 = Convert.ToDouble(ls[ls.IndexOf(sign) + 1]);
            switch (sign)
            {
                case "/":
                    try
                    {
                        res = i1 / i2;
                    }
                    catch
                    {
                        MessageBox.Show("Undefined: Cannot divide by zero");
                        shouldReset = true;
                    }

                    break;

                case "*":
                    res = i1 * i2;
                    break;

                case "+":
                    res = i1 + i2;
                    break;

                case "-":
                    res = i1 - i2;
                    break;
            }
            int index = ls.IndexOf(sign) - 1;
            ls.RemoveAt(index);
            ls.RemoveAt(index);
            ls.RemoveAt(index);
            if (ls.Count > 0)
                if (res % 1 == 0)
                    ls.Insert(index, Math.Round(res, 15).ToString("F0"));
                else
                    ls.Insert(index, Math.Round(res, 15).ToString());
            else
                if (res % 1 == 0)
                ls.Add(Math.Round(res, 15).ToString("F0"));
            else
                ls.Add(Math.Round(res, 15).ToString());
            return ls;
        }
        void Calculate()
        {

            try
            {
                int size = 0;
                if (value != string.Empty)
                    expression.Add(value);

                for (int i = 0; i < expression.Count; i++)
                {
                    if (expression[i] == "(")
                        size++;

                    if (expression[i] == "+" || expression[i] == "-" || expression[i] == "*" || expression[i] == "/")
                        WorkingWithSigns(i);
                }
                if (size > 0)
                {
                    BracketPairs = new int[size, 2];
                    for (int i = 0; i < size; i++)
                    {
                        for (int k = 0; k < 2; k++)
                            BracketPairs[i, k] = -1;
                    }

                    for (int i = 0; i < size; i++)
                        WorkingWithBrackets(expression.IndexOf("("), 0);

                }

                expression = CalcList(expression);

                if (expression.Count != 1)
                    throw new Exception();

                AnswerLabel.Text = "= " + expression[0];
                prevtext = string.Empty;
                ans = expression[0];
                if (AnsButton.Enabled == false)
                    AnsButton.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Syntax Error");
            }
            finally
            {
                shouldReset = true;
                ExpressionBox.ReadOnly = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AssignValue("1", ValueType.Regular);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LayoutSelector.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AssignValue("2", ValueType.Regular);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            AssignValue("3", ValueType.Regular);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AssignValue("4", ValueType.Regular);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignValue("5", ValueType.Regular);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AssignValue("6", ValueType.Regular);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AssignValue("7", ValueType.Regular);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AssignValue("8", ValueType.Regular);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AssignValue("9", ValueType.Regular);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AssignValue("0", ValueType.Regular);
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            AssignSign("*");
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            AssignSign("/");
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            AssignSign("+");
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            AssignSign("-");
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            shouldReset = true;
            ResetCalc();
        }
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void CommaButton_Click(object sender, EventArgs e)
        {
            AssignValue(",", ValueType.Regular);
        }

        private void AnsButton_Click(object sender, EventArgs e)
        {
            AssignValue("Ans", ValueType.Constant);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (shouldReset)
                ResetCalc();
            if (ExpressionBox.Text.Length > 0)
            {
                if (value == string.Empty)
                {
                    expression.RemoveAt(expression.Count - 1);
                    ExpressionBox.Text = string.Empty;
                    for (int i = 0; i < expression.Count; i++)
                        ExpressionBox.Text = ExpressionBox.Text + expression[i];
                }
                else
                {
                    value = value.Remove(value.Length - 1);
                    ExpressionBox.Text = ExpressionBox.Text.Remove(ExpressionBox.Text.Length - 1);
                }
                shouldReset = false;
            }
            AnswerLabel.Text = string.Empty;
        }

        private void eButton_Click(object sender, EventArgs e)
        {
            AssignValue("e", ValueType.Constant);
        }

        private void piButton_Click(object sender, EventArgs e)
        {
            AssignValue("π", ValueType.Constant);
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
        }

        private void PercentButton_Click(object sender, EventArgs e)
        {
            AssignSign("%");
        }

        private void ExpressionBox_Click(object sender, EventArgs e)
        {
            if (shouldReset == true)
                ResetCalc();
        }

        private string prevtext = string.Empty;
        private void ExpressionBox_TextChanged(object sender, EventArgs e)
        {



        }

        private void LeftBracButton_Click(object sender, EventArgs e)
        {
            AssignSign("(");
            if (expression.IndexOf("(") > 2 && double.TryParse(expression[expression.Count - 2].ToString(), out _))
            {
                expression.Insert(expression.Count - 1, "*");
            }
        }

        private void RightBracButton_Click(object sender, EventArgs e)
        {
            AssignSign(")");
        }

        private void ExponentButton_Click(object sender, EventArgs e)
        {
            AssignSign("^");
        }

        private void SqrtButtton_Click(object sender, EventArgs e)
        {
            AssignSign("√");
        }

        private void lnButton_Click(object sender, EventArgs e)
        {
            AssignSign("ln");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            AssignSign("log");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            if (ModeButton.Text == "RAD")
                ModeButton.Text = "DEG";
            else if (ModeButton.Text == "DEG")
                ModeButton.Text = "GRAD";
            else if (ModeButton.Text == "GRAD")
                ModeButton.Text = "RAD";
        }

        private void FactButton_Click(object sender, EventArgs e)
        {
            AssignSign("!");
        }

        private void xSqrtButton_Click(object sender, EventArgs e)
        {
            AssignSign("^");
            AssignSign("√");
        }

        private void CosecButton_Click(object sender, EventArgs e)
        {
            AssignSign("cosec");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void SecButton_Click(object sender, EventArgs e)
        {
            AssignSign("sec");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void CotButton_Click(object sender, EventArgs e)
        {
            AssignSign("cot");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void SinhButton_Click(object sender, EventArgs e)
        {
            AssignSign("sinh");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void CoshButton_Click(object sender, EventArgs e)
        {
            AssignSign("cosh");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void TanhButton_Click(object sender, EventArgs e)
        {
            AssignSign("tanh");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void CosechButton_Click(object sender, EventArgs e)
        {
            AssignSign("cosech");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void SechButton_Click(object sender, EventArgs e)
        {
            AssignSign("sech");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void CothButton_Click(object sender, EventArgs e)
        {
            AssignSign("coth");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AsinButton_Click(object sender, EventArgs e)
        {
            AssignSign("sin-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AcosButton_Click(object sender, EventArgs e)
        {
            AssignSign("cos-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AtanButton_Click(object sender, EventArgs e)
        {
            AssignSign("tan-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AcosecButton_Click(object sender, EventArgs e)
        {
            AssignSign("cosec-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AsecButton_Click(object sender, EventArgs e)
        {
            AssignSign("sec-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void AcotButton_Click(object sender, EventArgs e)
        {
            AssignSign("cot-1");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void SinButton_Click(object sender, EventArgs e)
        {
            AssignSign("sin");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }

        private void CosButton_Click(object sender, EventArgs e)
        {
            AssignSign("cos");
            if (expression.Count > 1 && double.TryParse(expression[expression.Count - 2], out _))
                expression.Insert(expression.Count - 1, "*");
            AssignSign("(");
        }
        private void TanButton_Click(object sender, EventArgs e)
        {
            if (expression.Count > 0 && double.TryParse(expression[expression.Count - 1], out _))
                expression.Add("*");
            AssignSign("tan");
            AssignSign("(");
        }


        private void TrigInvButton_Click(object sender, EventArgs e)
        {

            if (TrigInvButton.BackColor == defaultcolour)
            {
                TrigInvButton.BackColor = Color.LightBlue;
                if (TrigHyp.Visible == true)
                    TrigHyp.Visible = false;
                TrigRegular.Visible = false;
                TrigInverse.Visible = true;
                TrigHypButton.BackColor = defaultcolour;
            }
            else
            {
                TrigInvButton.BackColor = defaultcolour;
                TrigInverse.Visible = false;
                TrigRegular.Visible = true;
            }
        }

        private void TrigHypButton_Click(object sender, EventArgs e)
        {
            if (TrigHypButton.BackColor == defaultcolour)
            {
                TrigHypButton.BackColor = Color.LightBlue;
                if (TrigInverse.Visible == true)
                    TrigInverse.Visible = false;
                TrigRegular.Visible = false;
                TrigHyp.Visible = true;
                TrigInvButton.BackColor = defaultcolour;

            }
            else
            {
                TrigHypButton.BackColor = defaultcolour;
                TrigHyp.Visible = false;
                TrigRegular.Visible = true;
            }
        }

        private void TrigonometryButton_Click(object sender, EventArgs e)
        {
            if (TrigonometryButton.BackColor == defaultcolour)
            {
                TrigonometryButton.BackColor = Color.LightBlue;
                TrigonometryPanel.Visible = true;
            }
            else
            {
                TrigonometryButton.BackColor = defaultcolour;
                TrigonometryPanel.Visible = false;
            }
        }

        private void ExpressionBox_KeyDown(object sender, KeyEventArgs e)
        {
            string signs = "*%/*-+!()^√", values = "0123456789,", input = e.KeyData.ToString().ToLower();
            if (input.Length > 1) input = input.Remove(0, 1);

            MessageBox.Show(input);
            if (e.KeyData == Keys.Enter)
                Calculate();

            if (e.KeyData == Keys.Back)
            {
                ExpressionBox.Text = prevtext;
            }
            else
            {
                if (input == "e" || input == "π") // thats pi
                    AssignValue(input, ValueType.Constant);
                else if (values.Contains(input))
                    AssignValue(input, ValueType.Regular);

                else if (signs.Contains(input))
                {
                    AssignSign(input);
                }
                else
                {
                    if (ExpressionBox.Text.Length > 0)
                        ExpressionBox.Text = ExpressionBox.Text.Remove(ExpressionBox.Text.Length - 1);

                }
                prevtext = ExpressionBox.Text;
                ExpressionBox.SelectionStart = ExpressionBox.Text.Length;
                ExpressionBox.SelectionLength = 0;
            }
        }
    }
}
