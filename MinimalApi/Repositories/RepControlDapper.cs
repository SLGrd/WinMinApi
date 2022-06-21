using Common;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Cg = System.Configuration.ConfigurationManager;
using MinimalApi.Interfaces;

namespace MinimalApi.Repositories;
public class RepControlDapper : IControls
{
    public string GetPing()
    {
        try
        {   return $"Pingou (Dapper) : { DateTime.Now:hh:mm:ss}"; }
        catch (Exception ex)
        {   return $"Pingou fail (Dapper) : { ex.Message}"; }
    }
    public async Task<IEnumerable<ControlModel>> GetAllControls()
    {
        try
        {
            string sql = "Select * from Controls order by Nome";
            IDbConnection cn = new SqlConnection( Cg.ConnectionStrings["DBSource"].ConnectionString);                
            cn.Open();
            return await Task.FromResult( cn.Query<ControlModel>(sql).ToList());               
        }
        catch (Exception) { throw; } 
    }
    public async Task<ControlModel> GetControlById( int rowId)
    {
        try
        {
            string sqlHeader = "Select * from Controls where RowId = @RowId";
            using IDbConnection cn = new SqlConnection( Cg.ConnectionStrings["DBSource"].ConnectionString);               
            cn.Open();
            return await Task.FromResult( cn.Query<ControlModel>(sqlHeader, new { RowId = rowId }).First() );                   
        }
        catch (Exception) { throw; }
    }
    public async Task<ControlModel> CreateControl( ControlModel cm)
    {
        try
        {                
            string sqlHeader = "Insert into Controls "
                            + "(  Nome,  Cpf,  Cnpj,  Phone,  Cep,  ProductId,  Qtty,  UnitPrice) output Inserted.RowId"
                            + " values "
                            + "( @Nome, @Cpf, @Cnpj, @Phone, @Cep, @ProductId, @Qtty, @UnitPrice)";
            IDbConnection cn = new SqlConnection( Cg.ConnectionStrings["DBSource"].ConnectionString);
            cn.Open();
            var rowId = await cn.ExecuteScalarAsync<int>(sqlHeader, new
            {
                cm.Nome,
                cm.Cpf,
                cm.Cnpj,
                cm.Phone,
                cm.Cep,
                cm.ProductId,
                cm.Qtty,
                cm.UnitPrice,
                cm.RowId
            });
            cm.RowId = rowId;
            return cm;
        }
        catch (Exception) { throw; }
    }
    public async Task<ControlModel> UpdateControl( ControlModel cm)
    {
        try
        {
            string sqlHeader = "Update Controls "
                             + "set Nome = @Nome, Cpf = @Cpf, Cnpj = @Cnpj, Phone = @Phone, Cep = @Cep, "
                             + "ProductId = @ProductId, Qtty = @Qtty, UnitPrice = @UnitPrice "
                             + "where RowId = @Id";
            IDbConnection cn = new SqlConnection(Cg.ConnectionStrings["DBSource"].ConnectionString);
            cn.Open();
            var affectedRows = await  cn.ExecuteAsync(sqlHeader, new
            {
                cm.Nome,
                cm.Cpf,
                cm.Cnpj,
                cm.Phone,
                cm.Cep,
                cm.ProductId,
                cm.Qtty,
                cm.UnitPrice,
                Id = cm.RowId
            });
            return cm;
        }
        catch (Exception) { throw; }
    }
    public async Task<IResult> DeleteControl( int rowId)
    {
        try
        {
            string sqlHeader = "Delete from Controls where RowId = @RowId";
            IDbConnection cn = new SqlConnection(Cg.ConnectionStrings["DBSource"].ConnectionString);
            cn.Open();
            var affectedRows = await cn.ExecuteAsync(sqlHeader, new { RowId = rowId });
            return Results.Ok(rowId);
        }
        catch (Exception) { throw; }
    }
}

public class RepControlAdoNet : IControls
{
    public string GetPing()
    {
        try
        { return $"Pingou (ADO NET) : {DateTime.Now:hh:mm:ss}"; }
        catch (Exception ex)
        { return $"Pingou fail (ADO NET) : {ex.Message}"; }
    }
    public async Task<IEnumerable<ControlModel>> GetAllControls()
    {
        throw new NotImplementedException();    // Implementar todas as actions da interface é obrigatorio
    }
    public async Task<ControlModel> GetControlById(int rowId)
    {
        throw new NotImplementedException();    // Implementar todas as actions da interface é obrigatorio
    }
    public async Task<ControlModel> CreateControl(ControlModel cm)
    {
        throw new NotImplementedException();    // Implementar todas as actions da interface é obrigatorio
    }
    public async Task<ControlModel> UpdateControl(ControlModel cm)
    {
        throw new NotImplementedException();    // Implementar todas as actions da interface é obrigatorio
    }
    public async Task<IResult> DeleteControl(int rowId)
    {
        throw new NotImplementedException();    // Implementar todas as actions da interface é obrigatorio
    }
}