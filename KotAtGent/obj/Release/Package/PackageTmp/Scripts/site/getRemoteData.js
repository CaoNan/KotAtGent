var ajaxData;
var integratedData = new Array();;

//get data from remote.
$.ajax({
    url: 'http://data.appsforghent.be/kotatgent/data.json',
    type: 'get',
    dataType: 'json',
    success: function (data) {
        ajaxData = data.data;
        integrateData(data.data,1);//method defined in filter.js
    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log("error" + errorThrown);
    }
});

var integrateData = function (data,dataLocationIndex) {
    var i = 0;
    var dataItem = data[i];
    while (dataItem != undefined) {
        if (dataItem['Zone'] == "Sterre") {
            //make sure the remote data get a different id.
            dataItem['id'] = 1000000 * dataLocationIndex + i;
            integratedData.push(dataItem);
        }
        i++;
        dataItem = data[i];
    }
    //send data to controller
    var JSONstring = JSON.stringify(integratedData);
    $.post("/Home/GetRemoteData", { jsonData: JSONstring });
}