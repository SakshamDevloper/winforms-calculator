using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private double _value = 0;
        private string _operation = "";
        private bool _isOperationPressed = false;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void OnNumberClick(object sender, EventArgs e)
        {
            if (txtDisplay.Text == "0" || _isOperationPressed)
                txtDisplay.Clear();

            _isOperationPressed = false;
            Button btn = (Button)sender;
            txtDisplay.Text += btn.Text;
        }

        private void OnOperatorClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            _operation = btn.Text;
            _value = Convert.ToDouble(txtDisplay.Text);
            _isOperationPressed = true;
        }

        private void OnEqualsClick(object sender, EventArgs e)
        {
            double currentValue = Convert.ToDouble(txtDisplay.Text);

            switch (_operation)
            {
                case "+": txtDisplay.Text = (_value + currentValue).ToString(); break;
                case "-": txtDisplay.Text = (_value - currentValue).ToString(); break;
                case "*": txtDisplay.Text = (_value * currentValue).ToString(); break;
                case "/": txtDisplay.Text = (_value / currentValue).ToString(); break;
            }
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _value = 0;
        }
    }
}