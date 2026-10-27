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
        }

        private void calculation_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MultipleOperation minus = new MultipleOperation(new Minus(), new CompactMinus());
            Calculator.Operators.Add(new ValueStack());
            Calculator.Operators.Add(new MultipleOperation(new Multipy(), new Divide()));
            Calculator.Operators.Add(new MultipleOperation(new Minus(), new Add()));
            result.Text = Calculator.calculate(calculation.Text).ToString();

            //result.Text = Helper.BracketMatch(calculation.Text);
        }
    }
}