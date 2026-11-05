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
        public static Dictionary<string, ValueFormater> ValueFormaters = new Dictionary<string, ValueFormater>()
        {
            ["default"] = new ValueFormater(),
            ["minecraft"] = new ValueFormater(("chest", 27), ("stack", 64)),
            ["wynn"] = new ValueFormater(("stx", 64), ("le", 64), ("eb", 64)),
        };
        public MainWindow()
        {
            InitializeComponent();
            SetupCalculator();
            SetupResultFormatterBox();
        }

        private void SetupResultFormatterBox()
        {
            ResultFormatterBox.ItemsSource = ValueFormaters.Keys;
        }

        private static void SetupCalculator()
        {
            //MultipleOperation minus = new MultipleOperation(new Minus(), new CompactMinus());
            //Calculator.Operators.Add(new ValueStack());
            Calculator.Operators.AddRange(ValueFormaters.Values.AsEnumerable().Skip(1));
            //Calculator.Operators.Add(new ValueFormater(("stx", 64), ("le", 64), ("eb", 64)));
            //Calculator.Operators.Add(new ValueFormater(("fullChest", 27), ("stack", 64)));
            Calculator.Operators.AddRange(new Max(), new Minimum());
            Calculator.Operators.Add(new Brackets());
            Calculator.Operators.Add(new Pow());
            Calculator.Operators.Add(new MultipleOperation(new Multipy(), new Divide(), new Modulo()));
            Calculator.Operators.Add(new MultipleOperation(new Minus(), new Add()));
            Calculator.Operators.Add(new MultipleOperation(new LeftShift(), new RightShift(), new UnsignedRightShift()));
            Calculator.Operators.Add(new BitwiseAnd());
            Calculator.Operators.Add(new BitwiseXor());
            Calculator.Operators.Add(new BitwiseOr());
        }

        private void calculation_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalculation();

            //result.Text += "\n" + Helper.BracketMatch(calculation.Text);
        }

        private void UpdateCalculation()
        {
            result.Text = Calculator.Calculate(calculation.Text, ValueFormaters[ResultFormatterBox.SelectedItem.ToString()]);
        }

        private void OnResultFormatterChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCalculation();
        }
    }
}