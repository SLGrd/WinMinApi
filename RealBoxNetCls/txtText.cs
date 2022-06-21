namespace RealBoxNetCls;
public partial class txtText : UserControl
{
    public txtText() => InitializeComponent();

    #region Variables
    private string _Title = "";
    private string _Msg = "Msg";
    public int _TextCase;
    private string _Mask = new string('_', 24);
    private string _UText = "";
    private int _FontSize = 16;
    private HorizontalAlignment _TextAlign = HorizontalAlignment.Right;
    public enum TextControl { Regular = 0, LowerCase = 1, UpperCase = 2 };
    #endregion
    #region Events
    #endregion
    #region Properties
    public string Title
    { get { return _Title; } set { _Title = value; Caption.Text = value; } }
    public string Msg
    { get { return _Msg; } set { _Msg = value; Footer.Text = value; } }
    public TextControl TextCase
    {
        get { return (TextControl)_TextCase; }
        set { _TextCase = (int)value; }
    }
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
        set {   _UText = value is null ? "" : value;
                tBox.Text = _UText;
                tBox.Select(0, 0);
        }
    }
    #endregion

    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar))
        {
            string Left = tBox.Text[..tBox.SelectionStart];
            string Right = tBox.Text[tBox.SelectionStart..];

            if ((Left.Length + Right.Length) < _Mask.Count(f => f == '_'))
            {
                switch (_TextCase) {
                    case 0: tBox.Text = (Left + e.KeyChar + Right); break;
                    case 1: tBox.Text = (Left + e.KeyChar + Right).ToLower(); break;
                    case 2: tBox.Text = (Left + e.KeyChar + Right).ToUpper(); break;
                }
                tBox.SelectionStart = CurPosition(Left.Length + 1);
            }
        }
        e.Handled = true;
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
        {
            string Left = tBox.Text[..tBox.SelectionStart];
            string Right = tBox.Text[tBox.SelectionStart..];

            if (e.KeyCode == Keys.Back && Left.Length > 0) Left = Left.Remove(Left.Length - 1, 1);
            if (e.KeyCode == Keys.Delete && Right.Length > 0) Right = Right.Remove(0, 1);

            tBox.Text = (Left + Right);

            tBox.SelectionStart = CurPosition(Left.Length);
            SetCount();
            e.Handled = true;
        }

        if (e.KeyCode == Keys.Home)
        {
            tBox.Text = ""; tBox.SelectionStart = 0; tBox.ForeColor = Color.Black;
            SetCount();
            e.Handled = true;
        }

        if (e.KeyCode == Keys.End)
        {
            tBox.SelectionStart = CurPosition(tBox.Text.Length); e.Handled = true;
        }
    }
    private int CurPosition(int l)
    {
        // Recalcula a posição do cursor
        int p = l, j = l;
        for (int i = 0; i < _Mask.Length; i++)
            if (p > 0)
                if (_Mask[i] == '_') p--; else j++;
            else break;
        SetCount();
        return j;        
    }
    private void TxtRealBoxControl_Enter(object sender, EventArgs e) => pnl.BackColor = Color.Black;
    private void TxtRealBoxControl_Leave(object sender, EventArgs e) => pnl.BackColor = Color.LightGray;
    private void SetCount() => Msg = tBox.Text.Length.ToString() ;
}