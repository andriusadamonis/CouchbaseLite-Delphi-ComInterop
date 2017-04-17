using System;
using System.Runtime.InteropServices;

namespace CouchbaseLiteManager
{

    [ComVisible(true)]
    [Guid("02113b7a-4864-4520-94cd-1542a2b0ff05")]
    public interface ICouchbaseLiteFacade
    {
        string GetLocalDbName();

        void StartSyncGateway(string scheme = "https", string hostname = "localhost", int port = 4984, string dbname = "beer", string username = "david", string password = "123456");

        void StopSyncGateway();

        string Insert(string docId, string propertiesJson);

        string Get(string docId);

        string Update(string documentJson, string updatedPropertiesJsons);

        bool Delete(string documentId);
    }
}
