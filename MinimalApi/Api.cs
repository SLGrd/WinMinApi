using Common;
using MinimalApi.Interfaces;

namespace MinimalApi;
public static  class ApiExtension
{
    public static void ApiMapping( this WebApplication app)
    {
        app.MapGet("/TestConnection",         GetPing );  //  Test (Ping) connection        
        app.MapGet("/GetAll",                 GetAll  );  //  Get a list of all records                     
        app.MapGet("/GetRecordById/{rowId}",  GetRecordById);
        app.MapPost("/CreateRecord",          CreateRecord);
        app.MapPut("/UpdateRecord",           UpdateRecord);
        app.MapDelete("DeleteRecord/{rowId}", DeleteRecord);
    }
    private static IResult GetPing(IControls ic)
    {
        try
        { return  Results.Ok( ic.GetPing()); } // Results.Ok("Pingou : " + DateTime.Now.ToString("hh:mm:ss")); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
    private static async Task<IResult> GetAll(IControls ic)
    {
        try
        { return Results.Ok(await ic.GetAllControls()); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
    private static async Task<IResult> GetRecordById(IControls ic, int rowId)
    {
        try
        { return Results.Ok(await ic.GetControlById(rowId)); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
    private static async Task<IResult> CreateRecord(IControls ic, ControlModel cm)
    {
        try
        { return Results.Ok(await ic.CreateControl(cm)); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
    private static async Task<IResult> UpdateRecord(IControls ic, ControlModel cm)
    {
        try
        { return Results.Ok(await ic.UpdateControl(cm)); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
    private static async Task<IResult> DeleteRecord(IControls ic, int rowId)
    {
        try
        { return Results.Ok(await ic.DeleteControl(rowId)); }
        catch (Exception ex)
        { return Results.Problem($"Erro : {ex.Message} {DateTime.Now:hh:mm:ss} "); }
    }
}