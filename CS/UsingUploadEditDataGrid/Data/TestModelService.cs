namespace UsingUploadEditDataGrid.Data {
    public class TestModelService {
        private List<TestModel> CreateDataSource() {
            List<TestModel> temp = new List<TestModel>();
            temp.Add(new TestModel() { ID = 0, Name = "John", ImageUrl = @"Images\SmallGreen.jpg" });
            temp.Add(new TestModel() { ID = 1, Name = "Peter", ImageUrl = @"Images\SmallRed.jpg" });
            temp.Add(new TestModel() { ID = 2, Name = "James", ImageUrl = @"Images\SmallYellow.jpg" });
            return temp;
        }

        private List<TestModel> DataSource { get; set; }
        public TestModelService() {
            DataSource = CreateDataSource();
        }
        public Task<IEnumerable<TestModel>> GetDataSourceAsync(CancellationToken ct = default) {
            return Task.FromResult(DataSource.AsEnumerable());
        }
        List<TestModel> InsertInternal(IDictionary<string, object> newValue) {
            var dataItem = new TestModel();
            Update(dataItem, newValue);
            dataItem.ID = DataSource.OrderBy(m => m.ID).LastOrDefault().ID + 1;
            DataSource.Add(dataItem);
            return DataSource;
        }
        public Task<List<TestModel>> Insert(IDictionary<string, object> newValue) {
            return Task.FromResult(InsertInternal(newValue));
        }
        List<TestModel> RemoveInternal(TestModel dataItem) {
            DataSource.Remove(dataItem);
            return DataSource;
        }
        public Task<List<TestModel>> Remove(TestModel dataItem) {
            return Task.FromResult(RemoveInternal(dataItem));
        }
        List<TestModel> UpdateInternal(TestModel dataItem, IDictionary<string, object> newValue) {
            foreach (var field in newValue.Keys) {
                switch (field) {
                    case "Name":
                        dataItem.Name = (string)newValue[field];
                        break;
                    case "ImageUrl":
                        dataItem.ImageUrl = (string)newValue[field];
                        break;
                }
            }
            return DataSource;
        }
        public Task<List<TestModel>> Update(TestModel dataItem, IDictionary<string, object> newValue) {
            return Task.FromResult(UpdateInternal(dataItem, newValue));
        }
    }
}
