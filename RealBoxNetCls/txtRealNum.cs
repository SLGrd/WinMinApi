using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealBoxNetCls;
[ToolboxItem(true)]
[DefaultEvent("_TextChanged")]
public partial class txtRealNum : UserControl
{
    public txtRealNum() => InitializeComponent();

    #region Variables    
    const char Minus = '-';
    private string  _Title  = "CPF";
    private string  _Msg    = "Msg";
    private decimal dp      = 1.0m;
    private string  fmt     = "N0";
    private int _DecimalPlaces = 0;
    private int _MaxLength  = 12;
    private int _FontSize   = 14;
    private HorizontalAlignment _TextAlign = HorizontalAlignment.Right;
    #endregion
    public event EventHandler? _TextChanged;
    #region Properties
    public string Title
    { get { return _Title; } set { _Title = value; Caption.Text = value; } }
    public string Msg
    { get { return _Msg; } set { _Msg = value; Footer.Text = value; } }
    public HorizontalAlignment TextAlign
    { get { return _TextAlign; } set { _TextAlign = value; tBox.TextAlign = value; this.Invalidate(); } }
    public int FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            tBox.Font = new Font(Font.FontFamily, _FontSize, FontStyle.Regular);
        }
    }
    public int MaxLength
    { get { return _MaxLength; } set { _MaxLength = value; } }
    public int DecimalPlaces
    {
        get { return _DecimalPlaces; }
        set
        {
            _DecimalPlaces = value;
            dp = (decimal)Math.Pow(10, DecimalPlaces); fmt = $"N{value}";
        }
    }
    public string UText
    {
        get { return tBox.Text; }
        set
        {
            tBox.Text = UFormat(value);
            SetPlusMinusColor();
            tBox.Select(tBox.Text.Length, 0);
        }
    }
    #endregion
    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar) || e.KeyChar == Minus)
        {
            int cursorPosition = tBox.Text.Length - tBox.SelectionStart;

            if (e.KeyChar == Minus)
                tBox.Text = tBox.Text[0] == Minus ? tBox.Text.Remove(0, 1) : Minus + tBox.Text;
            else
            {
                if (tBox.Text[0] == Minus && tBox.SelectionStart == 0) tBox.SelectionStart = 1;
                if (tBox.Text.Length < _MaxLength)
                    tBox.Text = UFormat(tBox.Text.Insert(tBox.SelectionStart, e.KeyChar.ToString()));
            }
            tBox.SelectionStart = cursorPosition > tBox.Text.Length ? 0 : tBox.Text.Length - cursorPosition;
            SetPlusMinusColor();
        }
        e.Handled = true;
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
        {
            string Left = tBox.Text[..tBox.SelectionStart].Replace(".", "").Replace(",", "");
            string Right = tBox.Text[tBox.SelectionStart..].Replace(".", "").Replace(",", "");

            if (e.KeyCode == Keys.Back && Left.Length > 0) Left = Left.Remove(Left.Length - 1, 1);
            if (e.KeyCode == Keys.Delete && Right.Length > 0) Right = Right.Remove(0, 1);

            tBox.Text = UFormat(Left + Right);

            int cursorPosition = Right.Length > DecimalPlaces
                                 ? UFormat(new string('1', Right.Length)).Length : Right.Length;
            tBox.SelectionStart = cursorPosition > tBox.Text.Length ? 0 : tBox.Text.Length - cursorPosition;

            SetPlusMinusColor();
            e.Handled = true;
        }

        if (e.KeyCode == Keys.End || e.KeyCode == Keys.Home)
        {
            if (e.KeyCode == Keys.Home) tBox.Text = 0.ToString(fmt);
            tBox.SelectionStart = tBox.Text.Length;

            SetPlusMinusColor();
            e.Handled = true;
        }
    }
    private void SetPlusMinusColor() => tBox.ForeColor = (tBox.Text[0] == Minus) ? Color.Red : Color.Black;
    private void TxtRealBoxControl_Enter(object sender, EventArgs e) => pnl.BackColor = Color.Black;
    private void TxtRealBoxControl_Leave(object sender, EventArgs e) => pnl.BackColor = Color.LightGray;
    private string UFormat(string t)
    {
        if (t.Equals(string.Empty) || t.Equals(Minus.ToString())) t = "0";
        return (decimal.Parse(t.Replace(",", "").Replace(".", "")) / dp).ToString(fmt);
    }
    private void TxtBox_TextChanged(object sender, EventArgs e) { if (_TextChanged != null) _TextChanged.Invoke(sender, e); }
}