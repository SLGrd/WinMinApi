using Common;

namespace MinimalApi.Interfaces;
public interface IControls
{
    string GetPing();
    Task<IEnumerable<ControlModel>> GetAllControls();
    Task<ControlModel> GetControlById(int rowId);
    Task<ControlModel> CreateControl( ControlModel cm);
    Task<ControlModel> UpdateControl(ControlModel cm);
    Task<IResult> DeleteControl( int rowId);
}