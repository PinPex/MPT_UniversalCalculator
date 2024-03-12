using MPT_UniversalCalculator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TCtrl<TFrac, FracEditor> fracController;
        TCtrl<TPNumber, PNumberEditor> pNumberController;
        TCtrl<TComplex, ComplexEditor> complexController;
        const string TAG_FRAC = "FRAC_";
        const string TAG_COMPLEX = "COMPLEX_";
        const string TAG_PNUMBER = "PNUMBER_";
        const string OPERATIONS = "+-/*";
        bool PNumberMode = true;
        bool FracMode = true;
        bool ComplexMode = true;
        ObservableCollection<HistoryDataType> historyData;
        enum ComplexFunctions
        {
            Pwr, Root, Abs, Dgr, Rad
        }
        public MainWindow()
        {
            historyData = new ObservableCollection<HistoryDataType>();
            fracController = new TCtrl<TFrac, FracEditor>();
            pNumberController = new TCtrl<TPNumber, PNumberEditor>();
            complexController = new TCtrl<TComplex, ComplexEditor>();
            InitializeComponent();
            
        }

        private string NumberBeatifier(string Tag, string str)
        {
            if (str == "ERROR")
                return str;
            string ToReturn = str;
            switch (Tag)
            {
                case TAG_PNUMBER:
                    break;
                case TAG_FRAC:
                    if (FracMode == true)
                        ToReturn = str;
                    else if (new TFrac(str).Denominator == 1)
                        ToReturn = new TFrac(str).Numerator.ToString();
                    break;
                case TAG_COMPLEX:
                    if (ComplexMode == true)
                        ToReturn = str;
                    else if (new TComplex(str).Imaginary == 0)
                        ToReturn = new TComplex(str).Real.ToString();
                    break;
            }
            return ToReturn;
        }

        private static AEditor.Command CharToEditorCommand(char ch)
        {
            AEditor.Command command = AEditor.Command.cNone;
            switch (ch)
            {
                case '0':
                    command = AEditor.Command.cZero;
                    break;
                case '1':
                    command = AEditor.Command.cOne;
                    break;
                case '2':
                    command = AEditor.Command.cTwo;
                    break;
                case '3':
                    command = AEditor.Command.cThree;
                    break;
                case '4':
                    command = AEditor.Command.cFour;
                    break;
                case '5':
                    command = AEditor.Command.cFive;
                    break;
                case '6':
                    command = AEditor.Command.cSix;
                    break;
                case '7':
                    command = AEditor.Command.cSeven;
                    break;
                case '8':
                    command = AEditor.Command.cEight;
                    break;
                case '9':
                    command = AEditor.Command.cNine;
                    break;
                case 'A':
                    command = AEditor.Command.cA;
                    break;
                case 'B':
                    command = AEditor.Command.cB;
                    break;
                case 'C':
                    command = AEditor.Command.cC;
                    break;
                case 'D':
                    command = AEditor.Command.cD;
                    break;
                case 'E':
                    command = AEditor.Command.cE;
                    break;
                case 'F':
                    command = AEditor.Command.cF;
                    break;
                case '.':
                    command = AEditor.Command.cSeparator;
                    break;
                case '-':
                    command = AEditor.Command.cSign;
                    break;
            }
            return command;
        }

        private static TProc<T>.Oper CharToOperationsCommand<T>(char ch) where T : ANumber, new()
        {
            TProc<T>.Oper command = TProc<T>.Oper.None;
            switch (ch)
            {
                case '+':
                    command = TProc<T>.Oper.Add;
                    break;
                case '-':
                    command = TProc<T>.Oper.Sub;
                    break;
                case '*':
                    command = TProc<T>.Oper.Mul;
                    break;
                case '/':
                    command = TProc<T>.Oper.Div;
                    break;
            }
            return command;
        }
        private static AEditor.Command KeyCodeToEditorCommand(Keys ch)
        {
            AEditor.Command command = AEditor.Command.cNone;
            switch (ch)
            {
                case Keys.Back:
                    command = AEditor.Command.cBS;
                    break;
                case Keys.Delete:
                case Keys.Escape:
                    command = AEditor.Command.CE;
                    break;
            }
            return command;
        }

        private void Button_Number_Edit(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            string FullTag = button.Tag.ToString();
            if (FullTag.StartsWith(TAG_FRAC))
            {
                Enum.TryParse(FullTag.Replace(TAG_FRAC, string.Empty), out AEditor.Command ParsedEnum);
                tB_Frac.Text = fracController.ExecCommandEditor(ParsedEnum);
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                Enum.TryParse(FullTag.Replace(TAG_COMPLEX, string.Empty), out AEditor.Command ParsedEnum);
                tB_Complex.Text = complexController.ExecCommandEditor(ParsedEnum);
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                pNumberController.Edit.Notation = new TNumber(trackBar_PNumber.Value);
                Enum.TryParse(FullTag.Replace(TAG_PNUMBER, string.Empty), out AEditor.Command ParsedEnum);
                tB_PNumber.Text = pNumberController.ExecCommandEditor(ParsedEnum);
            }
        }

        private void Button_Number_Operation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string FullTag = button.Tag.ToString();


            if (FullTag.StartsWith(TAG_FRAC))
            {
                string Command = FullTag.Replace(TAG_FRAC, string.Empty);
                Enum.TryParse(Command, out TProc<TFrac>.Oper ParsedEnum);
                tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecOperation(ParsedEnum));
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);
                Enum.TryParse(Command, out TProc<TComplex>.Oper ParsedEnum);
                tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecOperation(ParsedEnum));
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);
                Enum.TryParse(Command, out TProc<TPNumber>.Oper ParsedEnum);
                tB_PNumber.Text = pNumberController.ExecOperation(ParsedEnum);
            }
        }


        private void GetHistory(TCtrl<TFrac, FracEditor> controller, String result, String oper = "")
        {
            HistoryDataType history_date = new HistoryDataType();
            if(oper == "")
            {
                history_date.Operation = controller.Proc.Operation.ToString();
            }
            else
            {
                history_date.Operation = oper;
            }
            history_date.Operand_1 = controller.Proc.Lop_Res.ToString();
            history_date.Operand_2 = controller.Proc.Rop.ToString();
            history_date.Result = result;
            history_date.Number_Type = "Simple Fraction";
            historyData.Add(history_date);
        }
        private void GetHistory(TCtrl<TPNumber, PNumberEditor> controller, String result, String oper = "")
        {
            HistoryDataType history_date = new HistoryDataType();
            if (oper == "")
            {
                history_date.Operation = controller.Proc.Operation.ToString();
            }
            else
            {
                history_date.Operation = oper;
            }
            history_date.Operand_1 = controller.Proc.Lop_Res.ToString();
            history_date.Operand_2 = controller.Proc.Rop.ToString();
            history_date.Result = result;
            history_date.Number_Type = "PNumber";
            historyData.Add(history_date);
        }

        private void GetHistory(TCtrl<TComplex, ComplexEditor> controller, String result, String oper = "")
        {
            HistoryDataType history_date = new HistoryDataType();
            if (oper == "")
            {
                history_date.Operation = controller.Proc.Operation.ToString();
            }
            else
            {
                history_date.Operation = oper;
            }
            history_date.Operand_1 = controller.Proc.Lop_Res.ToString();
            history_date.Operand_2 = controller.Proc.Rop.ToString();
            history_date.Result = result;
            history_date.Number_Type = "Complex";
            historyData.Add(history_date);
        }

        private void Button_Number_Function(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string FullTag = button.Tag.ToString();

            

            if (FullTag.StartsWith(TAG_FRAC))
            {

                string Command = FullTag.Replace(TAG_FRAC, string.Empty);
                
                Enum.TryParse(Command, out TProc<TFrac>.Func ParsedEnum);
                
                tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecFunction(ParsedEnum));
                GetHistory(fracController, tB_Frac.Text, Command);
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);
                Enum.TryParse(Command, out TProc<TComplex>.Func ParsedEnum);
                tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecFunction(ParsedEnum));
                GetHistory(complexController, tB_Complex.Text, Command);
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);
                Enum.TryParse(Command, out TProc<TPNumber>.Func ParsedEnum);
                tB_PNumber.Text = pNumberController.ExecFunction(ParsedEnum);
                GetHistory(pNumberController, tB_Complex.Text, Command);
            }
        }

        private void Button_Reset(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string FullTag = button.Tag.ToString();

            if (FullTag.StartsWith(TAG_FRAC))
            {
                GetHistory(fracController, "", "Reset");
                tB_Frac.Text = fracController.Reset();
                label_Frac_Memory.Text = string.Empty;
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                GetHistory(complexController, "", "Reset");
                tB_Complex.Text = complexController.Reset();
                tB_Complex_SpecialOut.Text = string.Empty;
                label_Complex_Memory.Text = string.Empty;
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                GetHistory(pNumberController, "", "Reset");
                tB_PNumber.Text = pNumberController.Reset();
                label_PNumber_Memory.Text = string.Empty;
            }
        }

        private void Button_FinishEval(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string FullTag = button.Tag.ToString();

            if (FullTag.StartsWith(TAG_FRAC))
            {
                
                tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.Calculate());
                GetHistory(fracController, tB_Frac.Text);
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.Calculate());
                GetHistory(complexController, tB_Complex.Text);
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                tB_PNumber.Text = pNumberController.Calculate();
                GetHistory(pNumberController, tB_PNumber.Text);
            }
        }

        private void Button_Memory(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string FullTag = button.Tag.ToString();



            if (FullTag.StartsWith(TAG_FRAC))
            {
                string Command = FullTag.Replace(TAG_FRAC, string.Empty);

                GetHistory(fracController, "", Command);

                Enum.TryParse(Command, out TMemory<TFrac>.Commands ParsedEnum);
                dynamic exec = fracController.ExecCommandMemory(ParsedEnum, tB_Frac.Text);
                if (ParsedEnum == TMemory<TFrac>.Commands.Copy)
                    tB_Frac.Text = exec.Item1.ToString();
                label_Frac_Memory.Text = exec.Item2 == TMemory<TFrac>.NumStates.ON ? "M" : string.Empty;
            }
            else if (FullTag.StartsWith(TAG_COMPLEX))
            {
                string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);

                GetHistory(complexController, "", Command);

                Enum.TryParse(Command, out TMemory<TComplex>.Commands ParsedEnum);
                dynamic exec = complexController.ExecCommandMemory(ParsedEnum, tB_Complex.Text);
                if (ParsedEnum == TMemory<TComplex>.Commands.Copy)
                    tB_Complex.Text = exec.Item1.ToString();
                label_Complex_Memory.Text = exec.Item2 == TMemory<TComplex>.NumStates.ON ? "M" : string.Empty;
            }
            else if (FullTag.StartsWith(TAG_PNUMBER))
            {
                string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);

                GetHistory(pNumberController, "", Command);

                Enum.TryParse(Command, out TMemory<TPNumber>.Commands ParsedEnum);
                dynamic exec = pNumberController.ExecCommandMemory(ParsedEnum, tB_PNumber.Text);
                if (ParsedEnum == TMemory<TPNumber>.Commands.Copy)
                    tB_PNumber.Text = exec.Item1.ToString();
                label_PNumber_Memory.Text = exec.Item2 == TMemory<TPNumber>.NumStates.ON ? "M" : string.Empty;
            }
        }

        private void СправкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Калькулятор чисел", "Калькулятор");
        }

        private void TrackBar_PNumber_ValueChanged(object sender, EventArgs e)
        {
            label_PNumber_P.Text = trackBar_PNumber.Value.ToString();
            pNumberController.Edit.Notation = new TNumber(trackBar_PNumber.Value);
            tB_PNumber.Text = pNumberController.Reset();
            if(label_PNumber_Memory != null)
            {
                label_PNumber_Memory.Text = string.Empty;
            }
            
            string AllowedEndings = "0123456789ABCDEF";
            List<Button> button_list = new List<Button>(){ b_PNumber_0, b_PNumber_1, b_PNumber_2, b_PNumber_3, b_PNumber_4,
                                                            b_PNumber_5, b_PNumber_6, b_PNumber_7, b_PNumber_8, b_PNumber_9,
                                                            b_PNumber_A, b_PNumber_B, b_PNumber_C, b_PNumber_D, b_PNumber_E, b_PNumber_F };
            foreach (Button i in button_list)
            {
                if (AllowedEndings.Contains(i.Name.ToString().Last()) && i.Name.ToString().Substring(i.Name.ToString().Length - 2, 1) == "_")
                {
                    int j = AllowedEndings.IndexOf(i.Name.ToString().Last());
                    if (j < trackBar_PNumber.Value)
                    {
                        i.IsEnabled = true;
                    }
                    if ((j >= trackBar_PNumber.Value) && (j <= 15))
                    {
                        i.IsEnabled = false;
                    }
                }
            }
            pNumberController.Proc.Lop_Res.Notation = new TNumber(trackBar_PNumber.Value);
            pNumberController.Proc.Rop.Notation = new TNumber(trackBar_PNumber.Value);
        }

        private void GetHistory(String operand1, String operation, String result, String number_type = "Complex")
        {
            historyData.Add(new HistoryDataType
            {
                Operand_1 = operand1,
                Operand_2 = "",
                Operation = operation,
                Number_Type = number_type,
                Result = result
            });
        }
        private void Button_Complex_Special(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string Tag = button.Tag.ToString().Replace(TAG_COMPLEX, string.Empty);
                Enum.TryParse(Tag, out ComplexFunctions ParsedEnum);
                TComplex number = new TComplex(tB_Complex.Text);
                switch (ParsedEnum)
                {
                    case ComplexFunctions.Pwr:
                        int PwrN = Convert.ToInt32(nUD_Complex_Pwr.Value);
                        tB_Complex_SpecialOut.Text = number.Pwr(PwrN).ToString();
                        GetHistory(tB_Complex.Text, "PWR", tB_Complex_SpecialOut.Text);
                        break;
                    case ComplexFunctions.Root:
                        int RootN = Convert.ToInt32(nUD_Complex_Root_N.Value);
                        int Rooti = Convert.ToInt32(nUD_Complex_Root_i.Value);
                        tB_Complex_SpecialOut.Text = number.Root(RootN, Rooti).ToString();
                        GetHistory(tB_Complex.Text, "Root", tB_Complex_SpecialOut.Text);
                        break;
                    case ComplexFunctions.Abs:
                        tB_Complex_SpecialOut.Text = number.Abs().ToString();
                        GetHistory(tB_Complex.Text, "Abs", tB_Complex_SpecialOut.Text);
                        break;
                    case ComplexFunctions.Dgr:
                        tB_Complex_SpecialOut.Text = number.GetDegree().ToString();
                        GetHistory(tB_Complex.Text, "Dgr", tB_Complex_SpecialOut.Text);
                        break;
                    case ComplexFunctions.Rad:
                        tB_Complex_SpecialOut.Text = number.GetRad().ToString();
                        GetHistory(tB_Complex.Text, "Rad", tB_Complex_SpecialOut.Text);
                        break;
                }
            }
            catch
            {
                tB_Complex_SpecialOut.Text = "ERROR";
            }
        }

        // Переделать
        private void Button_Complex_ShowAllRoots(object sender, EventArgs e)
        {
            

            ObservableCollection<Root> rootsData = new ObservableCollection<Root>();


            int RootN = Convert.ToInt32(nUD_Complex_Root_N.Value);
            TComplex number = new TComplex(tB_Complex.Text);
            for (int i = 0; i < RootN; ++i)
            {
                rootsData.Add(new Root { Number = i.ToString(), RootValue = number.Root(RootN, i).ToString() });
            }
            Roots rootsForm = new Roots(rootsData);
            rootsForm.Show();
        }

        private void nUD_Complex_Root_N_ValueChanged(object sender, EventArgs e)
        {
            if (nUD_Complex_Root_i != null)
            {
                nUD_Complex_Root_i.Maximum = nUD_Complex_Root_N.Value;
            }
        }


        // Переделать
        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            
            
            /*char key = KeyToChar(e.Key);
            switch (CalculatorWPFTabControl.SelectedIndex)
            {

                case 0:
                    {
                        if ((key >= '0' && key <= '9') || key == '.')
                            tB_Frac.Text = fracController.ExecCommandEditor(CharToEditorCommand(key));
                        else if (OPERATIONS.Contains(key))
                            tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecOperation(CharToOperationsCommand<TFrac>(key)));
                        break;
                    }
                case 1:
                    {
                        if ((key >= '0' && key <= '9') || key == '.')
                            tB_Complex.Text = complexController.ExecCommandEditor(CharToEditorCommand(key));
                        else if (OPERATIONS.Contains(key))
                            tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecOperation(CharToOperationsCommand<TComplex>(key)));
                        break;
                    }
                case 2:
                    {
                        if ((key >= '0' && key <= '9') || (key >= 'A' && key <= 'F') || (key == '.' && PNumberMode))
                            tB_PNumber.Text = pNumberController.ExecCommandEditor(CharToEditorCommand(key));
                        else if (OPERATIONS.Contains(key))
                            tB_PNumber.Text = NumberBeatifier(TAG_PNUMBER, pNumberController.ExecOperation(CharToOperationsCommand<TPNumber>(key)));
                        break;
                    }
                default:
                    break;
            }*/
        }

        // Переделать
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    {
                        if (e.KeyCode == Keys.Enter)
                            b_PNumber_Eval.PerformClick();
                        else
                        {
                            AEditor.Command comm = KeyCodeToEditorCommand(e.KeyCode);
                            if (comm != AEditor.Command.cNone)
                                tB_PNumber.Text = pNumberController.ExecCommandEditor(comm);
                        }
                        break;
                    }
                case 1:
                    {
                        if (e.KeyCode == Keys.Enter)
                            b_Frac_Eval.PerformClick();
                        else
                        {
                            AEditor.Command comm = KeyCodeToEditorCommand(e.KeyCode);
                            if (comm != AEditor.Command.cNone)
                                tB_Frac.Text = pNumberController.ExecCommandEditor(comm);
                        }
                        break;
                    }
                case 2:
                    {
                        if (e.KeyCode == Keys.Enter)
                            b_Complex_Eval.PerformClick();
                        else
                        {
                            AEditor.Command comm = KeyCodeToEditorCommand(e.KeyCode);
                            if (comm != AEditor.Command.cNone)
                                tB_Complex.Text = pNumberController.ExecCommandEditor(comm);
                        }
                        break;
                    }
                default:
                    break;
            }*/
        }

        private void tB_Frac_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*String fract_symbols = "01234567890/-";

            if (!(tB_Frac.Text.Contains("/")) && tB_Frac.Text.Contains("00"))
            {
                tB_Frac.Text = tB_Frac.Text.Replace("00", "0");
                return;
            }

            if ((tB_Frac.Text.Contains("/")) && tB_Frac.Text.Contains("/0"))
            {
                tB_Frac.Text = tB_Frac.Text.Replace("/0", "/");
                return;
            }


            int oldCaretIndex = tB_Frac.CaretIndex;


            if (tB_Frac.Text.Any(c => !fract_symbols.Contains(c)) ||
                tB_Frac.Text.Count(c => c == '/') > 1 ||
                tB_Frac.Text.Count(c => c == '-') > 1 ||
                tB_Frac.Text.IndexOf("-") > 0

                )
            {
                tB_Frac.Text = tB_Frac.Text.Remove(tB_Frac.CaretIndex - 1, 1);
                --oldCaretIndex;
            }

            tB_Frac.CaretIndex = Math.Min(oldCaretIndex, tB_Frac.Text.Length);*/
        }

        private void tB_Complex_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*String decimal_symbols = "01234567890-. +i*";

            int oldCaretIndex = tB_Complex.CaretIndex;

            if (tB_Complex.Text.Any(c => !decimal_symbols.Contains(c)) ||
                tB_Complex.Text.Count(c => c == '.') > 1 ||
                tB_Complex.Text.Count(c => c == '-') > 1 ||
                tB_Complex.Text.IndexOf("-") > 0

                )
            {
                tB_Complex.Text = tB_Complex.Text.Remove(tB_Complex.CaretIndex - 1, 1);
                --oldCaretIndex;
            }

            tB_Complex.CaretIndex = Math.Min(oldCaretIndex, tB_Complex.Text.Length);*/

        }

        private void History_click(object sender, RoutedEventArgs e)
        {
            History history = new History(historyData);
            history.Show();
        }
    }
}
