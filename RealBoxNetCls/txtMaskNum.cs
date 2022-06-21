using System.Data;

namespace RealBoxNetCls;
public partial class txtMaskNum : UserControl
{
    public txtMaskNum() => InitializeComponent();

    #region Variables
    private string _Title = "";
    private string _Msg = "Msg";
    private string _Mask = "";
    private string _UText = "";
    private int _FontSize = 16;
    private HorizontalAlignment _TextAlign = HorizontalAlignment.Right;
    #endregion
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
    public string Mask
    { get { return _Mask; } set { _Mask = value; } }
    public string UText
    {
        get { return tBox.Text; }
        set
        {
            _UText = value is null ? "" : value;
            tBox.Text = FormatText(_UText);
            tBox.Select(0, 0);
            tBox.SelectionStart = CurPosition(0);
        }
    }
    #endregion
    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar))
        {
            string Left = new(tBox.Text[..tBox.SelectionStart].Where(c => char.IsDigit(c)).ToArray());
            string Right = new(tBox.Text[tBox.SelectionStart..].Where(c => char.IsDigit(c)).ToArray());

            if ((Left.Length + Right.Length) < _Mask.Count(f => f == '_'))
            {
                tBox.Text = FormatText(Left + e.KeyChar + Right);
                tBox.SelectionStart = CurPosition(Left.Length + 1);
            }
        }
        e.Handled = true;
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
        {
            string Left = new(tBox.Text[..tBox.SelectionStart].Where(c => char.IsDigit(c)).ToArray());
            string Right = new(tBox.Text[tBox.SelectionStart..].Where(c => char.IsDigit(c)).ToArray());

            if (e.KeyCode == Keys.Back && Left.Length > 0) Left = Left.Remove(Left.Length - 1, 1);
            if (e.KeyCode == Keys.Delete && Right.Length > 0) Right = Right.Remove(0, 1);

            tBox.Text = FormatText(Left + Right);

            tBox.SelectionStart = CurPositionBack(Left.Length);
            e.Handled = true;
        }

        if (e.KeyCode == Keys.Home)
        {
            tBox.Text = _Mask; tBox.SelectionStart = 0; tBox.ForeColor = Color.Black; e.Handled = true;
        }

        if (e.KeyCode == Keys.End)
        {
            string Left = new(tBox.Text.Where(c => char.IsDigit(c)).ToArray());
            tBox.SelectionStart = CurPosition(Left.Length); e.Handled = true;
        }
    }
    private string FormatText(string t)
    {
        int n = 0;
        char[] s = _Mask.ToCharArray();     // Char array para evitar new strings in a loop

        for (int i = 0; i < _Mask.Length; i++)
            if (_Mask[i] == '_')
                if (n < t.Length) s[i] = (char)t[n++]; else break;
        return new string(s);
    }
    private int CurPosition(int p)
    {
        for (int i = 0; i < _Mask.Length; i++)
            if (_Mask[i] == '_')
            { p--; if (p < 0) return i; }
        return _Mask.Length;
    }
    private int CurPositionBack(int p)
    {
        if (p == 0) return CurPosition(p);
        for (int i = 0; i < _Mask.Length; i++)
        {
            if (p == 0) return i;
            if (_Mask[i] == '_') p--;
        }
        return _Mask.Length;
    }
    private void TxtRealBoxControl_Enter(object sender, EventArgs e) => pnl.BackColor = Color.Black;
    private void TxtRealBoxControl_Leave(object sender, EventArgs e) => pnl.BackColor = Color.LightGray;
}