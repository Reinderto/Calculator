using calculator.Operators;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupCalculator();
        }

        private static void SetupCalculator()
        {
            //MultipleOperation minus = new MultipleOperation(new Minus(), new CompactMinus());
            Calculator.Operators.Add(new Brackets());
            Calculator.Operators.Add(new ValueStack());
            Calculator.Operators.Add(new Pow());
            Calculator.Operators.Add(new MultipleOperation(new Multipy(), new Divide(), new Modulo()));
            Calculator.Operators.Add(new MultipleOperation(new Minus(), new Add()));
        }

        private void calculation_TextChanged(object sender, TextChangedEventArgs e)
        {
            result.Text = Calculator.Calculate(calculation.Text).ToString();

            //result.Text += "\n" + Helper.BracketMatch(calculation.Text);
        }
    }
}