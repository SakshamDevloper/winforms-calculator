using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public class CalculatorForm : Form
    {
        private TextBox txtDisplay;
        private double _value = 0;
        private string _operation = "";
        private bool _isOperationPressed = false;

        public CalculatorForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Calculator";
            this.Width = 300;
            this.Height = 400;
            this.Padding = new Padding(10);

            // Display
            txtDisplay = new TextBox();
            txtDisplay.Text = "0";
            txtDisplay.Dock = DockStyle.Top;
            txtDisplay.Height = 60;
            txtDisplay.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            txtDisplay.ReadOnly = true;

            this.Controls.Add(txtDisplay);

            // Table Layout
            var panel = new TableLayoutPanel();
            panel.RowCount = 5;
            panel.ColumnCount = 4;
            panel.Dock = DockStyle.Fill;

            // Equal rows
            for (int i = 0; i < 5; i++)
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            // Equal columns
            for (int i = 0; i < 4; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            string[] buttons = {
                "7","8","9","/",
                "4","5","6","*",
                "1","2","3","-",
                "0","C","=","+"
            };

            foreach (var text in buttons)
            {
                Button btn = new Button();
                btn.Text = text;
                btn.Dock = DockStyle.Fill;
                btn.Margin = new Padding(3);
                btn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

                // Optional styling (modern look)
                btn.BackColor = Color.FromArgb(45, 45, 48);
                btn.ForeColor = Color.White;

                if (char.IsDigit(text[0]))
                    btn.Click += OnNumberClick;
                else if (text == "=")
                    btn.Click += OnEqualsClick;
                else if (text == "C")
                    btn.Click += OnClearClick;
                else
                    btn.Click += OnOperatorClick;

                panel.Controls.Add(btn);
            }

            this.Controls.Add(panel);
        }

        private void OnNumberClick(object sender, EventArgs e)
        {
            if (txtDisplay.Text == "0" || _isOperationPressed)
                txtDisplay.Clear();

            _isOperationPressed = false;
            txtDisplay.Text += ((Button)sender).Text;
        }

        private void OnOperatorClick(object sender, EventArgs e)
        {
            if (_isOperationPressed) return;

            _operation = ((Button)sender).Text;
            _value = Convert.ToDouble(txtDisplay.Text);
            _isOperationPressed = true;
        }

        private void OnEqualsClick(object sender, EventArgs e)
        {
            double currentValue = Convert.ToDouble(txtDisplay.Text);

            switch (_operation)
            {
                case "+":
                    txtDisplay.Text = (_value + currentValue).ToString();
                    break;

                case "-":
                    txtDisplay.Text = (_value - currentValue).ToString();
                    break;

                case "*":
                    txtDisplay.Text = (_value * currentValue).ToString();
                    break;

                case "/":
                    txtDisplay.Text = currentValue == 0
                        ? "Error"
                        : (_value / currentValue).ToString();
                    break;
            }

            _isOperationPressed = false;
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _value = 0;
            _operation = "";
        }
    }
}