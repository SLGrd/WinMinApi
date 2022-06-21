using CleanApi.Properties;
using Common;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RealBoxNetCls;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using static Common.Glb;
using Sj = System.Text.Json;

namespace CleanApi;
public partial class FrmCleanApi : Form
{
    private ControlModel? CurrentModel;
    private ControlModel? InitialModel;

    BindingSource ctrlList = new();

    IHttpClientFactory? httpFactory;
    static readonly Stopwatch timer = new();
    
    public FrmCleanApi() => InitializeComponent(); 
    private void TxrQtty__TextChanged(object sender, EventArgs e)
    {
        //Podemos utilizar este codigo ou...
        NumberStyles Ns = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign;
        txnTotalValue.UText = ( decimal.Parse(txnQtty.UText, Ns)
                                * decimal.Parse(txnUnitPrice.UText, Ns)).ToString($"N{txnTotalValue.DecimalPlaces}");
        // ou ..... 
        // Podemos utilizar as extensões da classe string e decimal 
        txnTotalValue.UText = (txnQtty.UText.ToDecimal() * txnUnitPrice.UText.ToDecimal()).ToString($"N{txnTotalValue.DecimalPlaces}");
    }
    private void FrmCleanApi_Load(object sender, EventArgs e)
    {
        InitHttpFactory();  //  Initialize httpFactory
        BuildToolStrip();   //  Build toolstrip 
        AddNewRecord();     //  Ready to accept a new record
    }
    private void InitHttpFactory()
    {
        //  Routine to initialize httpFactory as a service
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient();
        ServiceProvider servs = serviceCollection.BuildServiceProvider();
        httpFactory = servs.GetRequiredService<IHttpClientFactory>();
    }
    private async void TestPing()
    {
        try
        {
            txtMsgs.Text = await Ping();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void AddNewRecord()
    {
        InitialModel = new();
        MapModelToScreen(InitialModel);
        RestoreMsgs();
        txtMsgs.Text = "";
        txrNome.Focus();
    }
    private void RestoreRecord()
    {
        MapModelToScreen( InitialModel);
        RestoreMsgs();
        txtMsgs.Text = "";
        txrNome.Focus();
    }
    private void DeleteRecord()
    {
        try
        {
            if ( MessageBox.Show( this, "Confirmação exclusão do registro ?", "Controls", 
                 MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) == DialogResult.Yes)
            {
                if (InitialModel!.RowId != 0)
                {
                    var rowId = DeleteRecord(InitialModel.RowId);
                    ClearScreen();
                    txtMsgs.Text = $"Record number {rowId} deleted";
                }
                else MessageBox.Show("No record to delete !");
            }
        }
        catch (Exception ex) { MessageBox.Show($"Erro : {ex.Message}"); }
    }   
    private async void SaveRecord()
    {
        try
        {
            CurrentModel = new();
            MapScreenToModel( CurrentModel!);

            if (Valid( CurrentModel!))      //  Check business rules
            {
                if (CurrentModel.RowId == 0)
                {
                    CurrentModel = await SaveRecord(CurrentModel);
                    ClearScreen();
                    txtMsgs.Text = $"Incluido o registro { CurrentModel!.RowId} - {CurrentModel.Nome} ";
                }
                else
                {
                    CurrentModel = await UpdateRecord(CurrentModel);
                    ClearScreen();
                    txtMsgs.Text = $"Atualizado o registro { CurrentModel!.RowId} - {CurrentModel.Nome} ";
                }                
            }
        }
        catch (Exception ex) { MessageBox.Show($"Erro : {ex.Message}"); }
    }   
    private async void ComboSelected(ToolStripComboBox t)
    {
        try
        {
            int rowId = (int)t.ComboBox.SelectedValue;  //  Get record Id from combobox
            InitialModel = await GetRecordById(rowId);  //  Gets record from database
            MapModelToScreen(InitialModel);             //  Maps record to screen
            txrNome.Focus();                            //  Move Focus to form
        }
        catch (Exception ex) { MessageBox.Show($"Erro : {ex.Message}"); }
    }
    private async void FillComboAll(ToolStripComboBox t)
    {
        try
        {
            ctrlList.DataSource = await GetAllRecords();
        }
        catch (Exception ex) { MessageBox.Show($"Erro : {ex.Message}", caption: "CLEAN API"); }
    }
    protected async Task<string> Ping()
    {
        try
        {
            HttpClient h = httpFactory!.CreateClient();
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);

            //  Envia o request (getasync) com o Action endpoint
            using HttpResponseMessage m = await h.GetAsync( "/TestConnection");
            if (m.IsSuccessStatusCode)
                //  Recebe a resposta com os dados requisitados e converte 
                return m.Content.ReadAsStringAsync().Result.ToString();
            else
                //  Envia mensagem de erro
                throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");
        }
        catch (Exception) { throw; }
    }
    //  Declaring HttpClient static for testing purposes
    static HttpClient? hStatic;
    protected async Task StaticPingV2()
    {       
        timer.Reset();
        timer.Start();
        string w = "Inicio : " + DateTime.Now.ToString("t") + " Static Usage : ";

        for (int i = 0; i < 1001; i++)
        {
            try
            {
                hStatic = new() { BaseAddress = new Uri(ApiBaseAddress!) };
                //  Envia o request (getasync) com o Action name
                using HttpResponseMessage m = await hStatic.GetAsync("/TestConnection");
                if (m.IsSuccessStatusCode)
                    txtMsgs.Text = w + i.ToString("N0") + " Segundos : " + timer.Elapsed.TotalSeconds.ToString("N0");
                else
                    throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");                
            }
            catch (Exception) { throw; }
        }
        timer.Stop();
    }
    protected async Task StaticPingV1()
    {
        try
        {
            timer.Reset();
            timer.Start();
            string w = "Inicio : " + DateTime.Now.ToString("t") + " Static Usage : ";

            hStatic = new() { BaseAddress = new Uri(ApiBaseAddress!) };
            for (int i = 0; i < 50001; i++)
            {               
                //  Envia o request (getasync) com o Action name
                using HttpResponseMessage m = await hStatic.GetAsync("/TestConnecton");
                try
                {
                    m.EnsureSuccessStatusCode();
                    //  If no error detected execution follows its normal flow
                    txtMsgs.Text = w + i.ToString("N0") + " Segundos : " + timer.Elapsed.TotalSeconds.ToString("N0");
                }
                catch (HttpRequestException) 
                {
                    throw new Exception( $"Error : { m.StatusCode} - {m.ReasonPhrase}");
                }
            }
            timer.Stop();
        }        
        catch (Exception) { throw; }
    }
    protected async Task UsingV1()
    {
        try
        {
            timer.Reset();
            timer.Start();
            string w = "Inicio : " + DateTime.Now.ToString("t") + " Socket Usage : ";
                        
            for (int i = 0; i< 50001; i++)
            {
                using HttpClient h = new() { BaseAddress = new Uri(ApiBaseAddress!) };
                //  Envia o request (getasync) com o Action name
                using HttpResponseMessage m = await h.GetAsync("/TestConnection");
                if (m.IsSuccessStatusCode)
                    txtMsgs.Text = w + i.ToString("N0") + " Segundos : " + timer.Elapsed.TotalSeconds.ToString("N0");
                else                   
                    throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");
            }
            timer.Stop();
        }
        catch (Exception) { throw; }
    }
    protected async Task FactoryV1()
    {
        try
        {
            timer.Reset();
            timer.Start();
            string w = "Inicio : " + DateTime.Now.ToString("t") + " Factory Usage : ";

            for (int i = 0; i < 50001; i++)
            {
                HttpClient h = httpFactory!.CreateClient();
                h.BaseAddress = new Uri(ApiBaseAddress!);

                //  Envia o request (getasync) com o Action name
                using HttpResponseMessage m = await h.GetAsync("/TextConnection");
                if (m.IsSuccessStatusCode)
                    txtMsgs.Text = w + i.ToString("N0") + " Segundos : " + timer.Elapsed.TotalSeconds.ToString("N0");
                else
                    throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");
            }
            timer.Stop();   
        }
        catch (Exception) { throw; }
    }
    protected async Task<ControlModel?> GetRecordById( int rowId)
    {
        try
        {
            using HttpClient h = new();
            //  Define o modo de recebimento dos dados (JSON) . Poderia ser XML por ex;
            h.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);

            //  Envia o request (getasync) com o URI universal resource identifier
            using HttpResponseMessage m = await h.GetAsync($"/GetRecordById/{rowId}");
            if (m.IsSuccessStatusCode)
            {
                //  Recebe a resposta com os dados requisitados 
                var dados = await m.Content.ReadAsStringAsync();
                //  Dois jeitos de converter o Json numa lista
                return Sj.JsonSerializer.Deserialize<ControlModel>(dados, new Sj.JsonSerializerOptions
                                                                             { PropertyNameCaseInsensitive = true});               
            }
            else
                throw new Exception($"{m.StatusCode}" + m.ReasonPhrase);
        }
        catch (Exception) { throw; }
    }
    protected async Task<int?> DeleteRecord( int rowId)
    {
        try
        {
            HttpClient h = httpFactory!.CreateClient();            
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);

            //  Envia o request
            using HttpResponseMessage m = await h.DeleteAsync($"/DeleteRecord/{rowId}");
            if (m.IsSuccessStatusCode)
            {
                return int.Parse( await m.Content.ReadAsStringAsync());
            }
            else
                throw new Exception($"{m.StatusCode}" + m.ReasonPhrase);            
        }
        catch (Exception) { throw; };
    }
    protected async Task<IEnumerable<ControlModel>?> GetAllRecords()
    {
        try
        {
            using HttpClient h = httpFactory!.CreateClient();
            //  Define o modo de recebimento dos dados (JSON) . Poderia ser XML por ex;
            h.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);

            //  Envia o request (getasync) com o URI universal resource identifier
            using HttpResponseMessage m = await h.GetAsync("/GetAll");
            if (m.IsSuccessStatusCode)
            {
                //  Recebe a resposta com os dados requisitados 
                var dados = await m.Content.ReadAsStringAsync();

                //  Dois jeitos de converter o Json numa lista
                //return Sj.JsonSerializer.Deserialize<IEnumerable<ControlModel>>(dados, new JsonSerializerOptions
                //              {
                //                  PropertyNameCaseInsensitive = true
                //              }).ToList();

                //      OU ...                        
                return JsonConvert.DeserializeObject<List<ControlModel>>(dados);
            }
            else
                throw new Exception( $"{m.ReasonPhrase} \r\n\n {m.RequestMessage}");
        }
        catch (Exception) { throw; }
    }
    protected async Task<ControlModel?> SaveRecord(ControlModel cm)
    {
        try
        {
            using HttpClient h = new();            
            //  Define o modo de recebimento dos dados (JSON) . Poderia ser XML por ex;
            h.DefaultRequestHeaders.ConnectionClose = true;
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);
            var x = new StringContent(Sj.JsonSerializer.Serialize(cm, new Sj.JsonSerializerOptions
                    { PropertyNameCaseInsensitive = false }), System.Text.Encoding.UTF8, "application/json");

            //  Envia o request (getasync) com o URI universal resource identifier
            using HttpResponseMessage m = await h.PostAsync($"/CreateRecord", x);
            if (m.IsSuccessStatusCode)
            {
                var dados = await m.Content.ReadAsStringAsync();
                return Sj.JsonSerializer.Deserialize<ControlModel>(dados, new Sj.JsonSerializerOptions
                                                                { PropertyNameCaseInsensitive = true });
            }
            else
                throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");
        }
        catch (Exception) { throw; };
    }
    protected async Task<ControlModel?> UpdateRecord(ControlModel cm)
    {
        try
        {
            HttpClient h = httpFactory!.CreateClient();
            //  Informa o endereço base (parte fixa) da API 7205
            h.BaseAddress = new Uri( ApiBaseAddress!);
            //  Serialize record to be updated
            var content = new StringContent( Sj.JsonSerializer.Serialize(cm), System.Text.Encoding.UTF8, "application/json");

            //  Envia o request
            using HttpResponseMessage m = await h.PutAsync($"/UpdateRecord", content);
            if (m.IsSuccessStatusCode)
            {
                var dados = await m.Content.ReadAsStringAsync();
                return Sj.JsonSerializer
                         .Deserialize<ControlModel>( dados, new Sj.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
                throw new Exception($"{m.StatusCode} - {m.ReasonPhrase}");
        }
        catch (Exception) { throw; };
    }
    private void MapModelToScreen(ControlModel? c)
    {
        txrNome.UText = c!.Nome;
        txtCpf.UText = c.Cpf;
        txtCnpj.UText = c.Cnpj;
        txmCep.UText = c.Cep;
        txmPhone.UText = c.Phone;
        txnProductId.UText = c.ProductId.ToString($"N{txnProductId.DecimalPlaces}");
        txnQtty.UText = c.Qtty.ToString($"N{txnQtty.DecimalPlaces}");
        txnUnitPrice.UText = c.UnitPrice.ToString($"N{txnUnitPrice.DecimalPlaces}");
        txnTotalValue.UText = (c.Qtty * c.UnitPrice).ToString($"N{txnTotalValue.DecimalPlaces}");
        txtMsgs.Text = "";
    }
    private void MapScreenToModel(ControlModel c)
    {
        try
        {
            c.Nome = txrNome.UText;
            c.Cpf = txtCpf.UText.ToDigits();
            c.Cnpj = txtCnpj.UText.ToDigits();
            c.Phone = txmPhone.UText.ToDigits();
            c.Cep = txmCep.UText.ToDigits();
            c.ProductId = txnProductId.UText.ToDecimal();
            c.Qtty = txnQtty.UText.ToDecimal();
            c.UnitPrice = txnUnitPrice.UText.ToDecimal();
            c.TotalValue = txnTotalValue.UText.ToDecimal();
            c.RowId = InitialModel!.RowId;
            txtMsgs.Text = "";
        }
        catch (Exception ex) { MessageBox.Show("Erro : " + ex.Message); }
    }
    private bool Valid(ControlModel c)
    {
        // Instantiates Controls Model Validator
        var ctx = new ValidationContext(c!);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(c!, ctx, validationResults, true);

        if (!isValid)
        {
            ClearMsgs();
            // Get validation Results
            for (int i = 0; i < validationResults.Count; i++)
            {
                switch (((string[])validationResults[i].MemberNames)[0])
                {
                    case "Nome": txrNome.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "Cpf": txtCpf.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "Cnpj": txtCnpj.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "Phone": txmPhone.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "Cep": txmCep.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;

                    case "ProductId": txnProductId.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "Qtty": txnQtty.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;
                    case "PricePerUnit": txnUnitPrice.Msg = validationResults.ElementAt(i).ErrorMessage!.ToString(); break;

                    default: break;
                }
            }
            txtMsgs.Text = "Registro contém campos inválidos";
            return false;
        }
        else
            return true;
    }
    private void ClearMsgs()
    {
        txrNome.Msg     = "";
        txtCpf.Msg      = "";
        txtCnpj.Msg     = "";
        txmPhone.Msg    = "";
        txmCep.Msg      = "";
        txnProductId.Msg = "";
        txnQtty.Msg      = "";
        txnUnitPrice.Msg = "";
        txtMsgs.Text     = "";
    }
    private void ClearScreen()
    {
        InitialModel = new();               //  Get an empty model
        MapModelToScreen(InitialModel);     //  Clear screen
        RestoreMsgs();  
        txrNome.Focus();
    }
    private void RestoreMsgs()
    {
        //  Restore messages
        txrNome.Msg = "Nome completo do Comprador";
        txtCpf.Msg = "Informe o Cpf";
        txtCnpj.Msg = "Informe o CNPJ";
        txmPhone.Msg = "Phone Number (Br)";
        txmCep.Msg = "Informe o CEP (Br)";
        txnProductId.Msg = "Product code";
        txnQtty.Msg = "Quantity in grams (g)";
        txnUnitPrice.Msg = "Price per gram";
        txnTotalValue.Msg = "Total Purchase Value";        
    }
    private void BuildToolStrip()
    {
        // Create a new ToolStrip control.
        ToolStrip tlsAction = new ToolStrip
        {
            BackColor = Color.FromArgb( 240, 255, 240 ),
            Dock = DockStyle.Top,
            GripMargin = new Padding(5, 8, 8, 3),
            ImageScalingSize = new Size(42, 42),
        };
        Controls.Add(tlsAction);
        #region Buttons
        tlsAction.Items.Clear();

        ToolStripButton tstNewRecord = new()
        {
            Image = Properties.Resources.icons8_plus_32px,
            ToolTipText = "Add new record"
        };
        tstNewRecord.Click += (object? sender, EventArgs e) => AddNewRecord();
        tlsAction.Items.Add(tstNewRecord);
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Height = 32, Width = 16 });

        ToolStripButton tstRefresh = new ToolStripButton
        {
            Image = Properties.Resources.icons8_undo.ToBitmap(),
            ToolTipText = "Restore current record"
        };
        tstRefresh.Click += (object? sender, EventArgs e) => RestoreRecord();
        tlsAction.Items.Add(tstRefresh);
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Width = 12 });

        ToolStripButton tstDelete = new ToolStripButton
        {
            Image = Resources.icons8_trash_3.ToBitmap(),
            ToolTipText = "Delete current record"
        };
        tstDelete.Click += (object? sender, EventArgs e) => DeleteRecord();
        tlsAction.Items.Add(tstDelete);
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Height = 32, Width = 16 });

        ToolStripButton tstSave = new ToolStripButton
        {
            Image = Resources.icons8_save_1.ToBitmap(),
            ToolTipText = "Save current record"
        };
        tstSave.Click += (object? sender, EventArgs e) => SaveRecord();
        tlsAction.Items.Add(tstSave);
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Width = 12 });

        ToolStripButton tstSearch = new ToolStripButton
        {
            Image = Resources.icons8_search_property.ToBitmap(),
            ToolTipText = "Search/Refresh grid view"
        };
        tlsAction.Items.Add(tstSearch);
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Width = 12 });
        #endregion
        #region SpecialButtons
        //  Begin dropdown definition -----------------------------------------------------------
        ToolStripMenuItem tstDropMnuItemOptionB = new ToolStripMenuItem     //* opcao B
        {
            Image = Resources.icons8_ruler_combined_32px,
            ForeColor = Color.Navy,
            Text = "Get Geolocation Lat/Lng"
        };
        tstDropMnuItemOptionB.Click += (object? sender, EventArgs e) =>
        {
            //GeoCode = await GeoLocationAsync();
            //ShowGeoCode();
        };
        ToolStripMenuItem tstDropMnuItemOptionD = new ToolStripMenuItem     //* opcao D
        {
            Image = Resources.icons8_taxi_on_the_map_64px,
            ForeColor = Color.Navy,
            Text = "Get Places Details"
        };
        tstDropMnuItemOptionD.Click += (object? sender, EventArgs e) =>
        {
            //GeoPlaceDetails = await GeoDetailsAsync();
            //ShowGeoPlaceDetails();
            //pnlAddressDetails.BringToFront();
        };
        ToolStripMenuItem tstDropMnuItemOptionE = new ToolStripMenuItem     //* opcao E
        {
            Image = Resources.icons8_google_maps_old_100px,
            ForeColor = Color.Navy,
            Text = "Get Place Map"
        };
        tstDropMnuItemOptionE.Click += (object? sender, EventArgs e) =>
        {
            //await GeoStaticMapAsync();
            //picMapa.BringToFront();
        };
        ToolStripDropDownButton tstDropGeo = new()
        {
            Image = Resources.icons8_meeting_arrows_globe.ToBitmap(),
            ForeColor = Color.Navy,
            Text = "Geo Functions"
        };
        tstDropGeo.DropDownItems.Add(tstDropMnuItemOptionB);
        tstDropGeo.DropDownItems.Add(tstDropMnuItemOptionD);
        tstDropGeo.DropDownItems.Add(tstDropMnuItemOptionE);
        tlsAction.Items.Add(tstDropGeo);
        //  End dropdown definition -----------------------------------------------------------
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Height = 32, Width = 12 });

        //  Begin dropdown definition ----------------------------------------------------------
        ToolStripMenuItem tstDropViewMap = new ToolStripMenuItem     //* opcao E
        {
            Image = Resources.icons8_meeting_arrows_globe.ToBitmap(),
            ForeColor = Color.Navy,
            Text = "Teste de conexão"
        };
        tstDropViewMap.Click += (object? sender, EventArgs e) => TestPing();        
        ToolStripMenuItem tstDropStaticPing = new ToolStripMenuItem     //* opcao A 
        {
            Image = Resources.icons8_meeting_arrows_globe.ToBitmap(),
            ForeColor = Color.Navy,
            Text = "Teste de conexão - Static"
        };
        tstDropStaticPing.Click += async (object? sender, EventArgs e) =>
        {
            try
            {
                await StaticPingV1();
            }
            catch (Exception ex) { MessageBox.Show("Erro :" + ex.Message); }
        };
        ToolStripMenuItem tstDropViewGrid = new()               //* opcao E
        {
            Image = Resources.secured_letter_32px,
            ForeColor = Color.Navy,
            Text = "Gera erro - Using "
        };
        tstDropViewGrid.Click += async (object? sender, EventArgs e) => {
            try
            {
                await UsingV1();
            }
            catch (Exception ex) { MessageBox.Show("Erro :" + ex.Message); }
        };
        ToolStripMenuItem tstDropViewDetails = new()            //* opcao C
        {
            Image = Resources.secured_letter_32px,
            ForeColor = Color.Navy,
            Text = "Não Gera erro - Factory"
        };
        tstDropViewDetails.Click += async (object? sender, EventArgs e) => {
            try
            {
                await FactoryV1();
            }
            catch (Exception ex) { MessageBox.Show("Erro :" + ex.Message); }
        };
        ToolStripSplitButton tstSplitView = new()
        {
            Image = Resources.secured_letter_32px,
            ForeColor = Color.Navy,
            Text = "     View      ",
            DropDownButtonWidth = 42
        };
        tstSplitView.Click += (object? sender, EventArgs e) =>
        {
            if (tstSplitView.ButtonPressed)
                MessageBox.Show("Split View Click");
        };
        tstSplitView.DropDownItems.Add(tstDropViewMap);
        tstSplitView.DropDownItems.Add(tstDropStaticPing);        
        tstSplitView.DropDownItems.Add(tstDropViewGrid);
        tstSplitView.DropDownItems.Add(tstDropViewDetails);
        tlsAction.Items.Add(tstSplitView);
        //  End dropdown definition -----------------------------------------------------------
        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Height = 32, Width = 32 });

        tlsAction.Items.Add(new ToolStripSeparator { AutoSize = false, Height = 32, Width = 32, Alignment = ToolStripItemAlignment.Right });
        ToolStripComboBox tscFields = new()
        {
            AutoSize = false,
            Alignment = ToolStripItemAlignment.Right,
            FlatStyle = FlatStyle.Standard,           //  Estilo da combo. Vc pode tentar outros
            MaxDropDownItems = 10,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 320,
            Height = 60,
            Font = new Font("", 12, FontStyle.Italic)
        };
        tscFields.ComboBox.ValueMember = "RowId";
        tscFields.ComboBox.DisplayMember = "Nome";
        tscFields.ComboBox.SelectedIndex = -1;
        tscFields.ComboBox.DataSource = ctrlList;
        tscFields.SelectedIndexChanged += (object? sender, EventArgs e) => ComboSelected(tscFields);
        tlsAction.Items.Add(tscFields);

        //  Associa button to call fillcombo routine
        tstSearch.Click += (object? sender, EventArgs e) => FillComboAll(tscFields);
        #endregion
    }
}