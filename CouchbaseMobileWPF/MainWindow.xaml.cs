using Couchbase.Lite;
using CouchbaseLiteManager;
using System;
using System.Windows;

namespace CouchbaseMobileWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CouchbaseLiteFacade _manager;
        string documentId;
        const string TAG = "BeerDemo";
        const string DB_NAME = "beer";
        const string SYNCHOST = "localhost";

        private Database _db;

        public MainWindow()
        {
            InitializeComponent();
            _manager = new CouchbaseLiteFacade();
            Title = _manager.GetLocalDbName();
            _manager.StartSyncGateway(hostname: SYNCHOST);
        }

        private void InsertDocumentClick(object sender, RoutedEventArgs e)
        {
            var properties =
            "{" +
                "\"Beer\": \"Švyturio Gintarinis\", " +
                "\"Age\": 12," +
                "\"Good\" : false" +
            "}";

            var docInsertedId = _manager.Insert(id.Text, properties);
            MessageBox.Show("Doc-id: " + docInsertedId, "Insert");

            documentId = docInsertedId;
            id.Text = docInsertedId;
        }

        private void UpdateDocumentClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(id.Text)) return;

            var newProperties =
                        "{" +
                            "\"Beer\": \"Švyturio Stiprusis\", " +
                            "\"Age\": " + DateTime.Now.Second + "," +
                            "\"Good\" : true" +
                        "}";

            var updated = _manager.Update(id.Text, newProperties);
            MessageBox.Show(updated, "Update");

        }

        private void GetDocumentClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(id.Text)) return;
           
            var docGet = _manager.Get(id.Text);
            
           MessageBox.Show(docGet);
        }

        private void DeleteDocumentClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(id.Text)) return;

            var isDeleted = _manager.Delete(id.Text);
            var msg = string.Format("Document deleted: {0}", isDeleted ? "Deleted" : "NotDeleted");
            Console.WriteLine(msg);
            MessageBox.Show(msg);
           
            documentId = null;
        }

        private void StartSyncClick(object sender, RoutedEventArgs e)
        {
            _manager.StartSyncGateway(hostname: SYNCHOST, username: ComboName.Text);
        }

        private void StopSyncClick(object sender, RoutedEventArgs e)
        {
            _manager.StopSyncGateway();
        }
    }
}
