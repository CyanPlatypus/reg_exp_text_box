using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextBoxTest
{
    class RegExpTextBox : System.Windows.Controls.TextBox
    {
        //[DefaultValue(typeof(SolidColorBrush), "Blue")]
        [Category("Brushes")]//Appearance
        [Description("BG color when Text doesn't match the RegExp pattern")]
        public Brush WrongBackgroundColor { get; set; }

        [DefaultValue(".*")]
        [Description("Redular Expression that the Text should match")]
        [Category("Other")]//Common Properties
        public string RegEx { get; set; }

        protected Brush TmpBG { get; set; }

        protected static readonly Color wrongBG = Colors.Purple;

        public RegExpTextBox()
        {
            TmpBG = Background;
            WrongBackgroundColor = new SolidColorBrush(wrongBG);
            RegEx = ".*";
            //OnTextChanged
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (Background != WrongBackgroundColor)
            {
                TmpBG = Background;
            }

            if (Match())
            {
                Background = TmpBG;
            }
            else
            {
                Background = WrongBackgroundColor;
                System.Windows.Input.FocusManager.SetFocusedElement(this.Parent, this);
            }

        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (!Match())
            {
                //e.Handled = true;

                Dispatcher.BeginInvoke(new Action(() => { this.Focus(); }));
                
                //this.Focus();
                //System.Windows.Input.Keyboard.Focus(this);
                //System.Windows.Input.FocusManager.SetFocusedElement(this.Parent, this);

            }

            base.OnLostFocus(e);
        }

        protected bool Match()
        {
            Regex rEx = new Regex(RegEx);
            return rEx.IsMatch(Text);
        }

    }
}
