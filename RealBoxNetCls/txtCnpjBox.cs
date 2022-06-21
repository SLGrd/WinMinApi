using System.Data;

namespace RealBoxNetCls
{
    public partial class txtCnpjBox : UserControl
    {
        public txtCnpjBox() => InitializeComponent();

        #region Variables    
        private string _Mask = "__.___.___/____-__";
        private string _UText = "";
        private int _FontSize = 14;
        public string _Title = "CNPJ";
        public string _Msg = "";

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
            }
        }
        #endregion
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                if (tBox.Text.Count(d => char.IsDigit(d)) < _Mask.Count(f => f == '_'))
                {
                    string Left = new(tBox.Text[..tBox.SelectionStart].Where(c => char.IsDigit(c)).ToArray());
                    string Right = new(tBox.Text[tBox.SelectionStart..].Where(c => char.IsDigit(c)).ToArray());

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

                tBox.SelectionStart = CurPosition(Left.Length);
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

            tBox.ForeColor = Color.Black;
            if (t.Length == _Mask.Count(f => f == '_')) tBox.ForeColor = IsValid(t) ? Color.Black : Color.Red;

            return new string(s);
        }
        private int CurPosition(int p)
        {
            for (int i = 0; i < _Mask.Length; i++)
                if (_Mask[i] == '_')
                { p--; if (p < 0) return i; }
            return _Mask.Length;
        }
        private void TxtRealBoxControl_Enter(object sender, EventArgs e) => pnl.BackColor = Color.Black;
        private void TxtRealBoxControl_Leave(object sender, EventArgs e) => pnl.BackColor = Color.LightGray;
        public bool IsValid(string value)
        {
            if (value == null) return false;

            string w = new(value.ToString()[..].Where(c => char.IsDigit(c)).ToArray()); //  Clear points and hifens

            if (w.Length != 14) return false;                               //  Numero de caracteres valido ?

            //--------------------- Primeiro digito de controle ------------------------------------------------------------
            string m = "543298765432";
            if (w[12] != CalculaDigito(w, m)) return false;                  //  Primeiro digito de controle valido ?

            //--------------------- Segundo digito de controle --------------------------------------------------------------
            m = "6543298765432";
            if (w[13] != CalculaDigito(w, m)) return false;                 //  Segundo digito de controle valido ?  

            return true;                                                    //  Verification digits matched                                  
        }
        public char CalculaDigito(string w, string m)
        {
            int Digit = 0;
            for (int i = 0; i < m.Length; i++)
                Digit += (w[i] - '0') * (m[i] - '0');                      //  w[i] - '0' =  Convert.ToInt32(w[i].ToString()) 

            Digit = (Digit %= 11) < 2 ? 0 : (11 - (Digit %= 11));           //  11 - Resto da divisão por 11. Se for maior que 9 ==> digito = 0

            return (char)(Digit + '0');                                     //  Transforma em Ascii : ex 0 em Ascii é 48 binario
        }
        public void SetColor(string w)
        {
            tBox.ForeColor = Color.Black;
            if (w.Length == 11)
                tBox.ForeColor = IsValid(w) ? Color.Black : Color.Red;
        }
    }
}