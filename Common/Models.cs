using System.ComponentModel.DataAnnotations;

namespace Common;
public class ControlModel
{
    [Required]
    [MinLength(4, ErrorMessage = "Nome must have at least 4 chars")]
    [MaxLength(36, ErrorMessage = "Nome must have max 36 chars")]
    public string Nome { get; set; }

    [Required]
    [ValidChars(ValidChars: "-.0123456789", ErrorMessage = "Only .- and numbers are allowed")]
    [ValidCpf(ErrorMessage = "Invalid CPF value")]
    public string Cpf { get; set; }

    [Required]
    [ValidChars(ValidChars: "-.0123456789", ErrorMessage = "Only .- and numbers are allowed")]
    [ValidCnpj(ErrorMessage = "Invalid Cnpj value")]
    public string Cnpj { get; set; }

    [Required]
    [MinLength(11, ErrorMessage = "Phone have 11 digits")]
    [MaxLength(11, ErrorMessage = "Phone have 11 digits")]
    [ValidChars(ValidChars: "()+- 0123456789", ErrorMessage = "Only ()- or numbers are allowed")]
    public string Phone { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Cep must have 8 digits")]
    [MaxLength(8, ErrorMessage = "Cep must have 8 digits")]
    [ValidChars(ValidChars: "()+- 0123456789", ErrorMessage = "Only ()+- or numbers are allowed")]
    public string Cep { get; set; }

    [Required]
    [Range(1, 99999, ErrorMessage = "Product code between 1 and 99999")]
    [ValidChars(ValidChars: "()+- 0123456789", ErrorMessage = "Only ()+- or numbers are allowed")]
    public decimal ProductId { get; set; }

    [Required]
    [Range(1, 99999, ErrorMessage = "Qtty(g) between 1 and 99999")]
    [ValidChars(ValidChars: "()+- 0123456789", ErrorMessage = "Only ()+- or numbers are allowed")]
    public decimal Qtty { get; set; }

    [Required]
    [Range(0.01, 10000, ErrorMessage = "Unit Price (g) between 1 and 10000")]
    public decimal UnitPrice { get; set; }

    [Editable(false)]
    public decimal TotalValue { get; set; }

    [Editable(false)]
    public int RowId { get; set; }

    public ControlModel()
    {          
        Nome = "";
        Cpf = "";
        Cnpj = "";
        Phone = "";
        Cep = "";
        ProductId = 0;
        Qtty = 0.0m;
        UnitPrice = 0.0m;
        TotalValue = 0.0m;
        RowId = 0;
    }
    public ControlModel(string nome, string cpf, string cnpj, string phone,
                         string cep, int productId, decimal qtty, decimal unitPrice, int rowId)
    {
        Nome = nome;
        Cpf = cpf;
        Cnpj = cnpj;
        Phone = phone;
        Cep = cep;
        ProductId = productId;
        Qtty = qtty;
        UnitPrice = unitPrice;
        TotalValue = qtty * UnitPrice;
        RowId = 0;
    }
}
public class ValidChars : ValidationAttribute
{
    private readonly string valChars;
    public ValidChars(string ValidChars) => valChars = ValidChars;

    public override bool IsValid(object? value)
    {
        if (value == null) { return false; };

        string? w = value.ToString();

        for (int i = 0; i < w.Length; i++)                      //  Checks for chars not contained in the permitted chars list
        { if (!valChars.Contains(w[i])) { return false; } }

        return true;                                            //  No invalid chars have been found
    }
}
public class ValidCpf : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        string w = new(value.ToString()?[..].Where(c => char.IsDigit(c)).ToArray());  //  Clear points and hifens

        if (w.Length != 11) return false;                       //  Numero de caracteres valido ?

        //--------------------- Primeiro digito de controle ------------------------------------------------------------
        if (w[9] != CalculaDigito(w, 9)) return false;          //  Primeiro digito de controle valido ?

        //--------------------- Segundo digito de controle --------------------------------------------------------------
        if (w[10] != CalculaDigito(w, 10)) return false;        //  Segundo digito de controle valido ?  

        return true;                                            //  Verification digits matched                                  
    }
    public char CalculaDigito(string w, int DigitNumber)
    {
        int Digit = 0;
        for (int i = 0; i < DigitNumber; i++)                       //  Transforma char em binario                                                                         
        { Digit += (w[i] - '0') * (DigitNumber + 1 - i); }          //  w[i] - '0' =  Convert.ToInt32(w[i].ToString()) 

        Digit = 11 - (Digit %= 11) > 9 ? 0 : 11 - (Digit %= 11);    //  11 - Resto da divisão por 11. Se for maior que 9 ==> digito = 0

        return (char)(Digit + '0');                                 //  Transforma em Ascii : ex 0 em Ascii é 48 binario
    }
}
public class ValidCnpj : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        string w = new(value.ToString()?[..].Where(c => char.IsDigit(c)).ToArray()); //  Clear points and hifens

        if (w.Length != 14) return false;                           //  Numero de caracteres valido ?

        //--------------------- Primeiro digito de controle ------------------------------------------------------------
        string m = "543298765432";
        if (w[12] != CalculaDigito(w, m)) return false;             //  Primeiro digito de controle valido ?

        //--------------------- Segundo digito de controle --------------------------------------------------------------
        m = "6543298765432";
        if (w[13] != CalculaDigito(w, m)) return false;             //  Segundo digito de controle valido ?  

        return true;                                                //  Verification digits matched                                  
    }
    public char CalculaDigito(string w, string m)
    {
        int Digit = 0;
        for (int i = 0; i < m.Length; i++)
            Digit += (w[i] - '0') * (m[i] - '0');                   //  w[i] - '0' =  Convert.ToInt32(w[i].ToString()) 

        Digit = (Digit %= 11) < 2 ? 0 : 11 - (Digit %= 11);         //  11 - Resto da divisão por 11. Se for maior que 9 ==> digito = 0

        return (char)(Digit + '0');                                 //  Transforma em Ascii : ex 0 em Ascii é 48 binario
    }
}