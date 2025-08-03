var ClsItems = {
    GetAll: function () {
        var self = this;
        Helper.AjaxCallGet("/Home/getallite", {}, "json",
            function (data) {

                var htmlData = "";
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    console.log(self.DrawItem(data[i])); // Use 'self'
                    htmlData += self.DrawItem(data[i]); // Use 'self'
                }
                console.log(htmlData);
                var d1 = document.getElementById('ItemArea');
                d1.innerHTML = htmlData;


            }, function () { });
    },
    DrawItem: function (item) {
        var data = `  <tr>
                        <tb>${item.CategoryId} </tb>
                        <tb>${item.CategoryName} </tb>
                      </tr>`;

        return data;
    }
};