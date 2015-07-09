using System.Collections.Generic;
using MR_Reporting_System_Data_Context.Context;
using MR_Reporting_System_Model.DataTransferObjectModel;

namespace MR_Reporting_System_Interface.IDataService
{
    public interface IDrugsRepository:IGenericRepository<Drugs>
    {
        List<DtoDrugs> SelectAll( string lang);
        DtoDrugs SelectById(int id, string lang); 
    }
}

